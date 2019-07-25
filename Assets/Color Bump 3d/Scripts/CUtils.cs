using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CUtils
{
	public static void OpenStore()
	{
		Application.OpenURL("https://play.google.com/store/apps/details?id=" + GameConfig.instance.androidPackageID);
	}

	public static void OpenStore(string id)
	{
		Application.OpenURL("https://play.google.com/store/apps/details?id=" + id);
	}

	public static void LikeFacebookPage(string faceID)
	{
		Application.OpenURL("fb://page/" + faceID);
		SetLikeFbPage(faceID);
	}

	public static void SetBuyItem()
	{
		SetBool("buy_item", value: true);
	}

	public static void SetRemoveAds(bool value)
	{
		SetBool("remove_ads", value);
	}

	public static bool IsAdsRemoved()
	{
		return GetBool("remove_ads");
	}

	public static bool IsBuyItem()
	{
		return GetBool("buy_item");
	}

	public static void SetRateGame()
	{
		SetBool("rate_game", value: true);
	}

	public static bool IsGameRated()
	{
		return GetBool("rate_game");
	}

	public static void SetLikeFbPage(string id)
	{
		SetBool("like_page_" + id, value: true);
	}

	public static bool IsLikedFbPage(string id)
	{
		return GetBool("like_page_" + id);
	}

	public static void SetDouble(string key, double value)
	{
		PlayerPrefs.SetString(key, DoubleToString(value));
	}

	public static double GetDouble(string key, double defaultValue)
	{
		string defaultValue2 = DoubleToString(defaultValue);
		return StringToDouble(PlayerPrefs.GetString(key, defaultValue2));
	}

	public static double GetDouble(string key)
	{
		return GetDouble(key, 0.0);
	}

	private static string DoubleToString(double target)
	{
		return target.ToString("R");
	}

	private static double StringToDouble(string target)
	{
		if (string.IsNullOrEmpty(target))
		{
			return 0.0;
		}
		return double.Parse(target);
	}

	public static void SetBool(string key, bool value)
	{
		PlayerPrefs.SetInt(key, value ? 1 : 0);
	}

	public static bool GetBool(string key, bool defaultValue = false)
	{
		int defaultValue2 = defaultValue ? 1 : 0;
		return PlayerPrefs.GetInt(key, defaultValue2) == 1;
	}

	public static bool EqualVector3(Vector3 v1, Vector3 v2)
	{
		return Vector3.SqrMagnitude(v1 - v2) <= 1E-07f;
	}

	public static double GetCurrentTime()
	{
		return DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
	}

	public static double GetCurrentTimeInDays()
	{
		return DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalDays;
	}

	public static double GetCurrentTimeInMills()
	{
		return DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
	}

	public static T GetRandom<T>(params T[] arr)
	{
		return arr[UnityEngine.Random.Range(0, arr.Length)];
	}

	public static bool IsActionAvailable(string action, int time, bool availableFirstTime = true)
	{
		if (!PlayerPrefs.HasKey(action + "_time"))
		{
			if (!availableFirstTime)
			{
				SetActionTime(action);
			}
			return availableFirstTime;
		}
		int num = (int)(GetCurrentTime() - GetActionTime(action));
		return num >= time;
	}

	public static double GetActionDeltaTime(string action)
	{
		if (GetActionTime(action) == 0.0)
		{
			return 0.0;
		}
		return GetCurrentTime() - GetActionTime(action);
	}

	public static void SetActionTime(string action)
	{
		SetDouble(action + "_time", GetCurrentTime());
	}

	public static void SetActionTime(string action, double time)
	{
		SetDouble(action + "_time", time);
	}

	public static double GetActionTime(string action)
	{
		return GetDouble(action + "_time");
	}

	public static void ShowInterstitialAd()
	{
		if (IsActionAvailable("show_ads", GameConfig.instance.adPeriod) && JobWorker.instance.onShowInterstitial != null)
		{
			JobWorker.instance.onShowInterstitial();
			SetActionTime("show_ads");
		}
	}

	public static void LoadScene(int sceneIndex, bool useScreenFader = false)
	{
		if (useScreenFader)
		{
			ScreenFader.instance.GotoScene(sceneIndex);
		}
		else
		{
			SceneManager.LoadScene(sceneIndex);
		}
	}

	public static void ReloadScene(bool useScreenFader = false)
	{
		int buildIndex = SceneManager.GetActiveScene().buildIndex;
		LoadScene(buildIndex, useScreenFader);
	}

	public static bool IsPointerOverUIObject()
	{
		PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
		PointerEventData pointerEventData2 = pointerEventData;
		Vector3 mousePosition = UnityEngine.Input.mousePosition;
		float x = mousePosition.x;
		Vector3 mousePosition2 = UnityEngine.Input.mousePosition;
		pointerEventData2.position = new Vector2(x, mousePosition2.y);
		List<RaycastResult> list = new List<RaycastResult>();
		EventSystem.current.RaycastAll(pointerEventData, list);
		return list.Count > 0;
	}
}
