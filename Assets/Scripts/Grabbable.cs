using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grabbable : Sortable {

	[SerializeField] private bool m_randomizeStartDirection = true;

	private float m_prevSortableOffset;

	private bool m_beingThrown = false;

	private Vector2 m_throwStartVelocity;
	private Vector2 m_throwVelocity;
	private float m_horizontalDecay = 0.95f;
	private float m_groundPos;

	private bool m_inPit = false;

	public int m_score = 100;
	public int Score
	{
		get
		{
			return m_score;
		}
	}

	public static System.Action<Grabbable> OnEnterPit;
	public static System.Action<Grabbable> OnGrabbed;
	public static System.Action OnTrueColorsRevealed;

	override protected void OnEnable()
	{
		base.OnEnable();
		GrabbableManager.Instance.AddGrabbable(this);

		if(m_randomizeStartDirection)
		{
			if(Random.Range(0,2)==1)
			{
				Vector3 scale = this.transform.localScale;
				scale.x = -scale.x;
				this.transform.localScale = scale;
			}
		}
	}

	override protected void OnDisable()
	{
		base.OnDisable();
		GrabbableManager.Instance.RemoveGrabbable(this);
	}

	public void SetSortDistance(float value)
	{
		m_sortOffset = value;
		m_prevSortableOffset = m_sortOffset;
	}

	public float GetPrevSortDistance()
	{
		return m_prevSortableOffset;
	}

	public bool CanBeGrabbed()
	{
		return m_beingThrown == false || m_throwVelocity.y < 0.0f;
	}

	public void Grab()
	{
		m_beingThrown = false;

		if(this.gameObject.tag == "Sign" && GameManager.EvilActivated == false)
		{
			GameManager.EvilActivated = true;
			if(OnTrueColorsRevealed != null)
			{
				OnTrueColorsRevealed();
			}
		}

		if(OnGrabbed != null)
		{
			OnGrabbed(this);
		}
	}

	public void Throw(Vector2 velocity, float groundPos)
	{
		m_beingThrown = true;
		m_throwVelocity = velocity;
		m_throwStartVelocity = m_throwVelocity;
		m_groundPos = groundPos;
	}

	public void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.tag == "Pit" && m_beingThrown)
		{
			SortManager.Instance.RemoveSortable(this);
			GrabbableManager.Instance.RemoveGrabbable(this);
			m_beingThrown = true;
			m_inPit = true;
			m_groundPos = this.transform.position.y - 10.0f;
			SetOrder(-2);

			GameObject textPrefab = ResourceManager.Instance.GetResource(GameManager.EvilActivated?"EvilText":"CuteText");
			Vector3 textPosition = this.transform.position;
			textPosition.z = -5.0f;
			GameObject textObject = GameObject.Instantiate(textPrefab, textPosition, Quaternion.identity);
			Text text = textObject.GetComponentInChildren<Text>();
			text.text = CuteTextManager.GetTextForGrabbable(this);

			if(gameObject.tag == "Untagged")
			{
				End.CrittersKilled++;
			}

			if(OnEnterPit != null)
			{
				OnEnterPit(this);
			}
		}
	}

	protected void Update()
	{
		if(m_beingThrown)
		{
			Vector3 position = this.transform.position;
			position.x += m_throwVelocity.x * Time.deltaTime;
			position.y += m_throwVelocity.y * Time.deltaTime;

			m_throwVelocity.x *= m_horizontalDecay;
			m_throwVelocity.y += Physics2D.gravity.y * Time.deltaTime;

			if(position.y < m_groundPos)
			{
				position.y = m_groundPos;
				if(m_inPit)
				{
					m_beingThrown = false;
					GameObject.Destroy(this.gameObject);
				}
			}

			if(Mathf.Abs(m_throwVelocity.x) < 0.2f && !m_inPit)
			{
				m_beingThrown = false;
			}

			this.transform.position = position;

			float finalAngle = 360.0f;
			if(m_throwVelocity.x > 0.0f)
			{
				finalAngle = -finalAngle;
			}
			this.transform.eulerAngles = Vector3.Lerp(Vector3.zero, new Vector3(0, 0, finalAngle), Mathf.Min(1.0f, 1.0f - ((m_throwVelocity.x / m_throwStartVelocity.x) - 0.15f)));

		}
	}
}
