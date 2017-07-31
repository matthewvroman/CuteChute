using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	[SerializeField]
	private Camera m_camera;

	[SerializeField]
	private Transform m_target;

	[SerializeField]
	private float m_lerpSpeed = 3.0f;
	
	private void OnEnable()
	{
		Kid.OnFall += OnKidFall;
	}

	private void OnDisable()
	{
		Kid.OnFall -= OnKidFall;
	}

	void LateUpdate () 
	{
		if(m_target == null)
		{
			return;
		}
		
		Vector3 desiredPosition = m_target.transform.position;
		desiredPosition.z = this.transform.position.z;

		this.transform.position = Vector3.Lerp(this.transform.position, desiredPosition, m_lerpSpeed * Time.deltaTime);
	}

	private void OnKidFall()
	{
		m_target = null;
	}
}
