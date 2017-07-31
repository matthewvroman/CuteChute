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
	[SerializeField] private AudioSource m_sfxSource;
	[SerializeField] private AudioClip m_throwSfx;
	[SerializeField] private AudioClip m_pitSfx;
	[SerializeField] private AudioClip m_grabSfx;

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

	public void PlayThrow()
	{
		m_sfxSource.clip = m_throwSfx;
		m_sfxSource.Play();
	}

	public void PlayPit()
	{
		m_sfxSource.clip = m_pitSfx;
		m_sfxSource.Play();
	}

	public void PlayGrab()
	{
		m_sfxSource.clip = m_grabSfx;
		m_sfxSource.Play();
	}
}
