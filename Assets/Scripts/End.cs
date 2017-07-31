using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour {

	public static int CrittersKilled = 0;

	[SerializeField]
	private ConversationBox m_conversationBox;

	// Use this for initialization
	void Start () 
	{
		if(CrittersKilled == 0)
		{
			m_conversationBox.SetTextChain(new string[]
			{
				"OH WOW. YOU SAVED ALL THE CRITTERS. WELL DON'T YOU FEEL SPECIAL!"
			});
		}
		else if(CrittersKilled == 1)
		{
			m_conversationBox.SetTextChain(new string[]
		{
			"YOU ONLY HURLED <color='#FFFFFF'>ONE</color> CUTE INNOCENT CRITTER TO THEIR DOOM BEFORE BANISHING ME!"
		});
		}
		else
		{
			m_conversationBox.SetTextChain(new string[]
			{
				"YOU ONLY HURLED <color='#FFFFFF'>"+CrittersKilled+"</color> CUTE INNOCENT CRITTERS TO THEIR DOOM BEFORE BANISHING ME!"
			});
		}

		AudioManager.Instance.PlayDefault();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
