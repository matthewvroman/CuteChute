using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ColorFalloff : MonoBehaviour {

	[SerializeField]
	private Color m_startColor;
	[SerializeField]
	private Color m_endColor;
	[SerializeField]
	private float m_startY;
	[SerializeField]
	private float m_endY;

	private void Awake()
	{
		float percent = Mathf.Clamp((this.transform.position.y - m_startY) / (m_endY - m_startY), 0.0f, 1.0f); 
		//percent = Random.Range(-1.0f, 1.0f);
		SpriteRenderer m_renderer = this.GetComponent<SpriteRenderer>();
		m_renderer.color = Color.Lerp(m_startColor, m_endColor, percent);
	}
}
