using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CuteMeter : MonoBehaviour {

	private float m_startingValue = 1000;
	private float m_depletionRate = 30;
	private float m_currentValue;
	private float m_maxValue;

	[SerializeField]
	private Image m_fillImage;

	public static System.Action OnDepleted;

	private void Awake()
	{
		m_maxValue = m_currentValue = m_startingValue;
	}

	private void OnEnable()
	{
		Grabbable.OnEnterPit += OnGrabbableEnterPit;
		Grabbable.OnTrueColorsRevealed += OnTrueColorsRevealed;
	}

	private void OnDisable()
	{
		Grabbable.OnEnterPit -= OnGrabbableEnterPit;
		Grabbable.OnTrueColorsRevealed -= OnTrueColorsRevealed;
		
	}

	private void Update()
	{
		if(m_currentValue>0.0f)
		{
			m_currentValue -= m_depletionRate * Time.deltaTime;
			if(m_currentValue <= 0.0f)
			{
				if(OnDepleted != null)
				{
					OnDepleted();
				}
			}
		}

		UpdateFill();
	}
	
	public void UpdateFill()
	{
		m_fillImage.fillAmount = m_currentValue/m_maxValue;
	}

	private void OnGrabbableEnterPit(Grabbable grabbable)
	{
		if(m_currentValue <= 0.0f)
		{
			//ripped already
			return;
		}

		m_currentValue = Mathf.Min(m_currentValue + grabbable.Score, m_maxValue);
		UpdateFill();
	}

	private void OnTrueColorsRevealed()
	{
		m_fillImage.color = Color.red;
	}
}
