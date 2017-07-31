using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : Sortable {

	[SerializeField]
	private Rigidbody2D m_rigidbody;
	[SerializeField]
	private Animator m_animator;
	[SerializeField]
	private float m_horizontalSpeed;
	[SerializeField]
	private float m_verticalSpeed;
	[SerializeField]
	private ParticleSystemRenderer m_smokeParticleSystemRenderer;

	[SerializeField]
	private GameObject m_tutorialGameObject;

	private Grabbable m_currentGrabbable;

	private bool m_inputEnabled = true;

	public static System.Action OnFall;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_rigidbody.velocity.y < 0.0f)
		{
			m_smokeParticleSystemRenderer.sortingOrder = m_spriteRenderer.sortingOrder - 1;
		}
		else
		{
			m_smokeParticleSystemRenderer.sortingOrder = m_spriteRenderer.sortingOrder + 1;
		}
	}

	public void DesireMoveDirection(float horizontal, float vertical)
	{
		if(!m_inputEnabled)
		{
			return;
		}

		Vector3 velocity = m_rigidbody.velocity;
		velocity.x = horizontal * m_horizontalSpeed;
		velocity.y = vertical * m_verticalSpeed;
		m_rigidbody.velocity = velocity;

		m_animator.SetBool("Moving", horizontal != 0.0f || vertical != 0.0f);
		Vector3 scale = this.transform.localScale;
		if(m_rigidbody.velocity.x < 0.0f)
		{
			scale.x = Mathf.Abs(scale.x);
		}
		else if(m_rigidbody.velocity.x > 0.0f)
		{
			scale.x = -Mathf.Abs(scale.x);
		}
		this.transform.localScale = scale;
	}

	public void DesireAction()
	{
		if(!m_inputEnabled)
		{
			return;
		}

		Grabbable grabbable = GrabbableManager.Instance.GetClosestGrabbableInRadius(this.transform.position, 1.0f);
		if(grabbable != null && grabbable != m_currentGrabbable && grabbable.CanBeGrabbed())
		{
			Grab(grabbable);

			if(m_tutorialGameObject != null)
			{
				m_tutorialGameObject.GetComponent<TutorialText>().enabled = false;
				GameObject.Destroy(m_tutorialGameObject);
				m_tutorialGameObject = null;
				
			}
		}
		else if(m_currentGrabbable != null)
		{
			Throw(m_currentGrabbable);
			m_currentGrabbable = null;
		}
	}

	public void Grab(Grabbable grabbable)
	{
		if(m_currentGrabbable != null)
		{
			Drop(m_currentGrabbable);
			m_currentGrabbable = null;
		}

		m_currentGrabbable = grabbable;
		m_currentGrabbable.Grab();
		grabbable.transform.SetParent(this.transform);
		grabbable.transform.eulerAngles = new Vector3(0, 0, 180);
		grabbable.transform.localPosition = new Vector3(0, 0.3f, 0.0f);

		float grabbableSortPosition = (grabbable.transform.position.y + grabbable.GetSortDistance());

		grabbable.SetSortDistance(this.transform.position.y - grabbable.transform.position.y + m_sortOffset - 0.01f);
	}

	public void Throw(Grabbable grabbable)
	{
		grabbable.transform.SetParent(null);
		grabbable.transform.eulerAngles = new Vector3(0, 0, 0);
		grabbable.SetSortDistance(grabbable.GetPrevSortDistance());

		float throwVelocityX = 10.0f;
		if(this.transform.localScale.x > 0.0f)
		{
			throwVelocityX = -throwVelocityX;
		}
		grabbable.Throw(new Vector2(throwVelocityX, 3), this.transform.position.y);
	}

	public void Drop(Grabbable grabbable)
	{
		grabbable.transform.SetParent(null);
		grabbable.transform.eulerAngles = new Vector3(0, 0, 0);
		grabbable.SetSortDistance(grabbable.GetPrevSortDistance());
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.tag == "Pit")
		{
			m_inputEnabled = false;
			Fall();
		}
	}

	public void Fall()
	{
		m_rigidbody.gravityScale = 1.0f;
		m_rigidbody.constraints = 0;
		m_rigidbody.angularVelocity = this.transform.localScale.x>0?180.0f:-180.0f;

		m_spriteRenderer.sortingOrder = -2;
		SortManager.Instance.RemoveSortable(this);

		if(OnFall != null)
		{
			OnFall();
		}
	}

	public void ShowTutorial()
	{
		if(m_tutorialGameObject != null)
		{
			m_tutorialGameObject.SetActive(true);
		}
	}
}
