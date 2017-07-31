using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWaveScaler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 scale = this.transform.localScale;
		scale.y = 1.0f + 0.035f * Mathf.Sin(Time.realtimeSinceStartup * 1.5f * Mathf.PI);
		this.transform.localScale = scale;
		
	}
}
