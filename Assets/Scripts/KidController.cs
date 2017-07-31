using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidController : MonoBehaviour {

	[SerializeField]
	private Kid m_kid;
	
	void Update () 
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		m_kid.DesireMoveDirection(horizontal, vertical);

		if(Input.GetKeyDown(KeyCode.Space))
		{
			m_kid.DesireAction();
		}
	}

	private void OnDisable()
	{
		m_kid.DesireMoveDirection(0,0);
	}
}
