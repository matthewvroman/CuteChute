using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuteTextManager : MonoBehaviour {

	private static List<string> CuteText = new List<string>()
	{
		"OMG!",
		"SOOOO CUTE!!",
		"EEEK!! CUTE!",
		"SO. CUTE.",
		"CUTENESS OVERLOAD!",
		"SO. FLUFFY!",
		"SO. CUDDLY!",
	};

	private static List<string>AvailableCuteText = new List<string>();

	private static List<string> EvilText = new List<string>()
	{
		"I HUNGER FOR SOULS!",
		"MWAHAHAHAHAHA!",
		"A WORTHY SACRIFICE!",
		"MORE! MORE!! MORE!!!",
		"MY HUNGER STRENGTHENS!"
	};

	private static List<string>AvailableEvilText = new List<string>();

	public static string GetTextForGrabbable(Grabbable grabbable)
	{
		/*if(grabbable.gameObject.tag == "Sign")
		{
			return "<color=#FF0000>BRING ME THE PARK RANGER!</color>";
		}*/

		if(AvailableCuteText.Count == 0)
		{
			for(int i=0; i<CuteText.Count; i++)
			{
				AvailableCuteText.Add(CuteText[i]);
			}
		}

		if(AvailableEvilText.Count == 0)
		{
			for(int i=0; i<EvilText.Count; i++)
			{
				AvailableEvilText.Add(EvilText[i]);
			}
		}

		string text = "";
		if(GameManager.EvilActivated)
		{
			text = AvailableEvilText[Random.Range(0, AvailableEvilText.Count)];
			AvailableEvilText.Remove(text);
		}
		else
		{
			text = AvailableCuteText[Random.Range(0, AvailableCuteText.Count)];
			AvailableCuteText.Remove(text);
		}
		
		return text + "\n+" + grabbable.Score;
	}
}
