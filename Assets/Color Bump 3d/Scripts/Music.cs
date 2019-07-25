using System;
using System.Collections;
using UnityEngine;

public class Music : MonoBehaviour
{
	public enum Type
	{
		None,
		Main_1,
		Main_2,
		Main_3
	}

	public AudioSource audioSource;

	public static Music instance;

	[HideInInspector]
	public AudioClip[] musicClips;

	private Type currentType;

	private float lastMusicTime = -2.14748365E+09f;

	private int lastMusicIndex = -1;

	private void Awake()
	{
		instance = this;
	}

	public bool IsMuted()
	{
		return !IsEnabled();
	}

	public bool IsEnabled()
	{
		return CUtils.GetBool("music_enabled", true);
	}

	public void SetEnabled(bool enabled, bool updateMusic = false)
	{
		CUtils.SetBool("music_enabled", enabled);
		if (updateMusic)
		{
			UpdateSetting();
		}
	}

	public void Play(Type type)
	{
		if (type != 0 && (currentType != type || !audioSource.isPlaying))
		{
			StartCoroutine(PlayNewMusic(type));
		}
	}

	public void Play()
	{
		Play(currentType);
	}

	public void Stop()
	{
		audioSource.Stop();
	}

	public void Pause()
	{
		audioSource.Pause();
	}

	private IEnumerator PlayNewMusic(Type type)
	{
		while (audioSource.volume >= 0.1f)
		{
			audioSource.volume -= 0.2f;
			yield return new WaitForSeconds(0.1f);
		}
		audioSource.Stop();
		currentType = type;
		audioSource.clip = musicClips[(int)type];
		if (IsEnabled())
		{
			audioSource.Play();
		}
		audioSource.volume = 1f;
	}

	private void UpdateSetting()
	{
		if (!(audioSource == null))
		{
			if (IsEnabled())
			{
				Play();
			}
			else
			{
				audioSource.Stop();
			}
		}
	}

	public void PlayAMusic()
	{
		if (Time.time - lastMusicTime < 60f)
		{
			audioSource.UnPause();
			return;
		}
		int num = Enum.GetNames(typeof(Type)).Length - 1;
		lastMusicIndex = (lastMusicIndex + 1) % num;
		lastMusicTime = Time.time;
		Type type = (Type)(lastMusicIndex + 1);
		Play(type);
	}
}
