using System;
using UnityEngine;

[Serializable]
public class GameConfig : MonoBehaviour
{
	[Header("")]
	public int adPeriod;

	public string androidPackageID;

	public string iosAppID;

	[Header("")]
	public bool enableTesting;

	public static GameConfig instance;

	private void Awake()
	{
		instance = this;
	}
}
