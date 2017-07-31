using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationBox : MonoBehaviour {

	[SerializeField] private Text m_text;

	private string m_writtenText;
	private string[] m_textChain;
	private int m_textChainIndex = 0;
	private Coroutine m_writeCoroutine;
	private Coroutine m_fadeOutCoroutine;

	private System.Action m_onComplete = null;

	private bool m_autoAdvance = false;

	public bool IsWriting()
	{
		return m_writeCoroutine != null;
	}

	public void SetTextChain(string[] textChain, System.Action onComplete=null, bool autoAdvance=false)
	{
		this.gameObject.SetActive(true);
		m_textChain = textChain;
		m_textChainIndex = 0;
		m_autoAdvance = autoAdvance;
		m_writeCoroutine = StartCoroutine(WriteText(m_textChain[m_textChainIndex]));

		StartCoroutine(FadeIn());

		m_onComplete = onComplete;
	}

	private IEnumerator FadeIn()
	{
		Vector3 startPosition = this.transform.position;
		startPosition.y += 15.0f;
		Vector3 endPosition = this.transform.position;

		CanvasGroup canvasGroup = this.GetComponent<CanvasGroup>();
		float progress = 0.0f;
		while(progress < 1.0f)
		{
			progress = Mathf.Min(progress + Time.deltaTime, 1.0f);
			canvasGroup.alpha = progress;

			this.transform.position = Vector3.Lerp(startPosition, endPosition, progress);

			yield return new WaitForEndOfFrame();
		}
	}

	private IEnumerator FadeOut()
	{
		Vector3 startPosition = this.transform.position;
		Vector3 endPosition = this.transform.position;
		endPosition.y -= 15.0f;

		CanvasGroup canvasGroup = this.GetComponent<CanvasGroup>();
		float progress = 0.0f;
		while(progress < 1.0f)
		{
			progress = Mathf.Min(progress + Time.deltaTime*2.0f, 1.0f);
			canvasGroup.alpha = 1.0f - progress;

			this.transform.position = Vector3.Lerp(startPosition, endPosition, progress);

			yield return new WaitForEndOfFrame();
		}

		this.transform.position = startPosition;
		this.gameObject.SetActive(false);

		m_fadeOutCoroutine = null;
	}

	public void SetText(string text)
	{
		SetTextChain(new string[]{text}, m_onComplete);
	}

	private IEnumerator WriteText(string text)
	{
		int currentIndex = 0;
		while(currentIndex <= text.Length)
		{
			if(currentIndex < text.Length)
			{
				bool needEndCarrot = text[currentIndex]=='<';
				while(needEndCarrot)
				{
					currentIndex++;
					needEndCarrot = text[currentIndex]!='>';
					if(!needEndCarrot)
					{
						currentIndex++;
					}
				}
			}
			string writeString = text.Substring(0, currentIndex);

			if(writeString.LastIndexOf("<color") > writeString.LastIndexOf("</color"))
			{
				writeString += "</color>";
			}

			currentIndex++;
			m_text.text = writeString;
			yield return new WaitForSeconds(0.04f);
		}

		if(m_autoAdvance)
		{
			yield return new WaitForSeconds(0.35f);
			Advance();
		}

		m_writeCoroutine = null;
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(IsWriting())
			{
				StopCoroutine(m_writeCoroutine);
				m_writeCoroutine = null;
				m_text.text = m_textChain[m_textChainIndex];
			}
			else
			{
				Advance();
			}
		}
	}

	private void Advance()
	{
		m_textChainIndex++;
		if(m_textChainIndex < m_textChain.Length)
		{
			m_writeCoroutine = StartCoroutine(WriteText(m_textChain[m_textChainIndex]));
		}
		else if(m_fadeOutCoroutine == null)
		{
			m_fadeOutCoroutine = StartCoroutine(FadeOut());

			if(m_onComplete != null)
			{
				m_onComplete();
				m_onComplete = null;
			}
		}
	}
}
