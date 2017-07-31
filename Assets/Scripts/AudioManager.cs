using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	private static AudioManager s_instance;
	public static AudioManager Instance
	{
		get
		{
			return s_instance;
		}
	}

	[SerializeField] private AudioSource m_audioSource;
	[SerializeField] private AudioClip m_defaultTrack;
	[SerializeField] private AudioClip m_franticTrack;

	void Start () 
	{
		if(s_instance==null)
		{
			s_instance = this;
		}
		if(s_instance != this)
		{
			GameObject.Destroy(this.gameObject);
			return;
		}
		GameObject.DontDestroyOnLoad(this.gameObject);

		PlayDefault();
	}
	
	public void PlayDefault()
	{
		if(m_audioSource.clip == m_defaultTrack) return;
		m_audioSource.clip = m_defaultTrack;
		m_audioSource.Play();
	}

	public void PlayFrantic()
	{
		if(m_audioSource.clip == m_franticTrack) return;
		m_audioSource.clip = m_franticTrack;
		m_audioSource.Play();
	}
}
