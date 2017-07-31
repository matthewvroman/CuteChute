using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static bool EvilActivated = false;

	[SerializeField]
	private KidController m_kidController;

	[SerializeField]
	private CuteMeter m_meter;

	[SerializeField]
	private ConversationBox m_conversationBox;

	[SerializeField]
	private ConversationBox m_evilConversationBox;

	private void OnEnable()
	{
		End.CrittersKilled = 0;
		
		Grabbable.OnGrabbed += OnGrabbableGrabbed;
		Grabbable.OnTrueColorsRevealed += OnTrueColors;
		Grabbable.OnEnterPit += OnGrabbableEnterPit;
		CuteMeter.OnDepleted += OnCuteMeterDepleted;
		Kid.OnFall += OnKidFall;
	}

	private void OnDisable()
	{
		Grabbable.OnGrabbed -= OnGrabbableGrabbed;
		Grabbable.OnTrueColorsRevealed -= OnTrueColors;
		Grabbable.OnEnterPit -= OnGrabbableEnterPit;
		CuteMeter.OnDepleted -= OnCuteMeterDepleted;
		Kid.OnFall -= OnKidFall;
	}

	// Use this for initialization
	void Start () 
	{
		EvilActivated = false;
		m_kidController.enabled = false;
		m_meter.enabled = false;
		m_conversationBox.SetTextChain(new string[]
		{
			"Hey there <color='#9fd38a'>Billy</color>... can you spare a moment for your favorite park ranger?",
			"Great! Now <color='#9fd38a'>Bucky</color>, have you been paying attention in school?",
			"Do you know how your town keeps the lights on?",
			"...Electricity you say? Hah! Good one <color='#9fd38a'>Bobby</color>!",
			"Listen here <color='#9fd38a'>Baxter</color>. Your town runs on cuteness! *cough* Nothing strange about that, no sir.",
			"We have what we like to call a <color='#FF0000'>cute chute</color> in this park!", 
			"By putting cute objects into the <color='#FF0000'>chute</color>, we can power the town!",
			"The guy who normally operates the <color='#FF0000'>chute</color> fell i.. err.. called in sick today. *cough*",
			"Can you help out by doing his job? Everyone would really appreciate it!",
			"Great, thanks <color='#9fd38a'>Buster</color>! I always knew I could count on you!",
			"You can throw anything that's cute into that <color='#FF0000'>chute</color>, and I mean anything!", 
			"Keeping this town up and running is your top priority!",
			"Good luck now <color='#9fd38a'>Burton</color>! We're all counting on you!"
		}, delegate()
		{
			m_kidController.enabled = true;
			m_meter.enabled = true;
			m_kidController.GetComponent<Kid>().ShowTutorial();
		});
	}
	
	private void OnGrabbableGrabbed(Grabbable grabbable)
	{
		if(grabbable.gameObject.tag == "Ranger")
		{
			m_conversationBox.SetTextChain(new string[]
			{
				"Hey buddy! What the heck do you think you're doing!?!?",
			}, null, false);
		}
	}

	private void OnTrueColors()
	{
		m_kidController.enabled = false;
		m_meter.enabled = false;
		m_evilConversationBox.SetTextChain(new string[]
		{
			"DO YOU ENJOY FEEDING ME YOUR FURRY FRIENDS?",
			"NO?!??!? WELL THEN HOW ABOUT A DEAL!",
			"BRING ME THE SOUL OF YOUR BELOVED PARK RANGER AND I SHALL LEAVE THIS LAND!!",
		}, delegate()
		{
			m_kidController.enabled = true;
			m_meter.enabled = true;
			AudioManager.Instance.PlayDefault();
		});

		AudioManager.Instance.PlayFrantic();
	}

	private void OnGrabbableEnterPit(Grabbable grabbable)
	{
		if(grabbable.gameObject.tag == "Ranger")
		{
			m_kidController.enabled = false;
			m_meter.enabled = false;
			m_evilConversationBox.SetTextChain(new string[]
			{
				"YES. THE SOUL OF THE ONE WHO BOUND ME TO THIS WORLD!",
				"A MOST DELICIOUS MEAL. THANK YOU BILLY.. OR WAS IT BOBBY?",
				"AHH NEVERMIND! MY TIME HERE IS DONE! GOODBYE SMALL MORTAL!",
			}, delegate()
			{
				SceneManager.LoadScene("End");
				AudioManager.Instance.PlayDefault();
			});

			AudioManager.Instance.PlayFrantic();
		}
	}

	private void OnCuteMeterDepleted()
	{
		if(EvilActivated)
		{
			m_kidController.enabled = false;
			m_evilConversationBox.SetTextChain(new string[]
			{
				"IF YOU WILL NOT FEED ME YOUR PRECIOUS PARK RANGER. THEN I WILL DEVOUR YOUR WORLD",
			}, delegate()
			{
				SceneManager.LoadScene("Title");
				AudioManager.Instance.PlayDefault();
			});
		}
		else
		{
			m_kidController.enabled = false;
			m_conversationBox.SetTextChain(new string[]
			{
				"AWW GEEZE BRANDON! You didn't move fast enough!",
				"You say you're worried of falling in? What do you think the <color='#FF0000'>sign</color> is there for!",
				"Ahh jeeze.. maybe the next kid will get it.. darn youngins never read the <color='#FF0000'>sign</color>",
			}, delegate()
			{
				SceneManager.LoadScene("Title");
			});
			AudioManager.Instance.PlayDefault();
		}

		AudioManager.Instance.PlayFrantic();
	}

	private void OnKidFall()
	{
		m_meter.enabled = false;
		m_evilConversationBox.SetTextChain(new string[]
		{
			"THIS IS NOT THE HUMAN SOUL I SEEK. BUT DELICIOUS NONE THE LESS.."
		}, delegate()
		{
			m_conversationBox.SetTextChain(new string[]
			{
				"I wonder where <color='#9fd38a'>Brian</color> went? That's the 5th kid this week..",
				"I should make sure my <color='#FF0000'>sign</color> didn't fall down.."
			}, delegate()
			{
				SceneManager.LoadScene("Title");
			});

			AudioManager.Instance.PlayDefault();
		});

		AudioManager.Instance.PlayFrantic();
	}
}
