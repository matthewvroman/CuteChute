using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Sortable : MonoBehaviour {

	protected SpriteRenderer m_spriteRenderer;

	[SerializeField] private SpriteRenderer[] m_extraRenderers;

	[SerializeField] protected float m_sortOffset;

	private void Awake()
	{
		m_spriteRenderer = this.GetComponent<SpriteRenderer>();
	}

	protected virtual void OnEnable()
	{
		
	}

	protected virtual void OnDisable()
	{
		
	}

	private void OnBecameVisible()
	{
		SortManager.Instance.AddSortable(this);
	}

	private void OnBecameInvisible()
	{
		SortManager.Instance.RemoveSortable(this);
	}

	public float GetSortDistance()
	{
		return this.transform.position.y + m_sortOffset; //temp
	}

	public void SetOrder(int order)
	{
		m_spriteRenderer.sortingOrder = order;

		if(m_extraRenderers != null)
		{
			for(int i=0; i<m_extraRenderers.Length; i++)
			{
				m_extraRenderers[i].sortingOrder = order;
			}
		}
	}

	public void OnDrawGizmos()
	{
		SpriteRenderer spriteRenderer = this.GetComponent<SpriteRenderer>();

		Vector3 startPosition = this.transform.position;
		startPosition.y += m_sortOffset;
		startPosition.x -= spriteRenderer.sprite.bounds.size.x/2.0f;
		Vector3 endPosition = startPosition;
		endPosition.x += spriteRenderer.sprite.bounds.size.x;
		Gizmos.color = Color.red;
		Gizmos.DrawLine(startPosition, endPosition);
	}
}
