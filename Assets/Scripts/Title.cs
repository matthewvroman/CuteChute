using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Title : MonoBehaviour {

	[SerializeField] private GameObject m_fullInfo;
	[SerializeField] private GameObject m_noInfo;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(this.GetComponent<End>() != null) //LOL
			{
				SceneManager.LoadScene("Title");
			}
			else
			{
				SceneManager.LoadScene("Level");
			}
			
		}
	}

	public void ToggleInfo()
	{
		m_fullInfo.gameObject.SetActive(!m_fullInfo.gameObject.activeInHierarchy);
		m_noInfo.gameObject.SetActive(!m_noInfo.gameObject.activeInHierarchy);
	}
}
