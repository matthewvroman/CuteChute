using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFade : MonoBehaviour {

	private float m_fadeOutDelay = 1.0f;

	private float m_fadeInTime = 0.5f;
	private float m_fadeInTimeLeft;

	private Text m_text;
	private float m_fadeTime = 1.0f;
	private float m_fadeTimeLeft;

	private float m_velocityY = 1.0f;
	
	private void Awake()
	{
		m_text = this.GetComponentInChildren<Text>();
		m_fadeTimeLeft = m_fadeTime;
		m_fadeInTimeLeft = m_fadeInTime;
	}

	// Update is called once per frame
	void Update () 
	{
		Color color = m_text.color;

		if(m_fadeInTimeLeft > 0.0f)
		{
			m_fadeInTimeLeft = Mathf.Max(0.0f, m_fadeInTimeLeft - Time.deltaTime);
			color.a = 1.0f - m_fadeInTimeLeft / m_fadeInTime;
			m_text.color = color;
			return;
		}

		if(m_fadeOutDelay > 0.0f)
		{
			m_fadeOutDelay -= Time.deltaTime;
			return;
		}

		m_fadeTimeLeft = Mathf.Max(0.0f, m_fadeTimeLeft - Time.deltaTime);
		color.a = 1.0f * m_fadeTimeLeft / m_fadeTime;
		m_text.color = color;
		if(m_fadeTimeLeft == 0.0f)
		{
			GameObject.Destroy(this.gameObject);
		}

		Vector3 position = this.transform.position;
		position.y += m_velocityY * Time.deltaTime;
		this.transform.position = position;
	}
}
