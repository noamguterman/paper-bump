using UnityEngine;

public class Prefs
{
	public static int UnlockedLevel
	{
		get
		{
			return PlayerPrefs.GetInt("unlocked_level", 1);
		}
		set
		{
			PlayerPrefs.SetInt("unlocked_level", value);
		}
	}

	public static bool PlayerNeverDie
	{
		get
		{
			return (PlayerPrefs.GetInt("player_never_die") == 1) ? true : false;
		}
		set
		{
			PlayerPrefs.SetInt("player_never_die", value ? 1 : 0);
		}
	}
}
