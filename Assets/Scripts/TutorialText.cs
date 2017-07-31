using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText : MonoBehaviour {

	[SerializeField]
	private Transform m_target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position = m_target.transform.position;
		position.y += 0.7f;
		this.transform.position = position;
	}
}
