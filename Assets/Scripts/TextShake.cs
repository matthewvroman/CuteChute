using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextShake : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 eulerAngles = this.transform.localEulerAngles;
		eulerAngles.z = Random.Range(-2.0f, 2.0f);
		this.transform.localEulerAngles = eulerAngles;
	}
}
