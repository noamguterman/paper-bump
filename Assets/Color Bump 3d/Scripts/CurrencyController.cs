using System;
using UnityEngine;

public class CurrencyController
{
	public const string CURRENCY = "ruby";

	public const int DEFAULT_CURRENCY = 10;

	public static Action onBalanceChanged;

	public static Action<int> onBallanceIncreased;

	public static int GetBalance()
	{
		return PlayerPrefs.GetInt("ruby", 10);
	}

	public static void SetBalance(int value)
	{
		PlayerPrefs.SetInt("ruby", value);
		PlayerPrefs.Save();
	}

	public static void CreditBalance(int value)
	{
		int balance = GetBalance();
		SetBalance(balance + value);
		if (onBalanceChanged != null)
		{
			onBalanceChanged();
		}
		if (onBallanceIncreased != null)
		{
			onBallanceIncreased(value);
		}
	}

	public static bool DebitBalance(int value)
	{
		int balance = GetBalance();
		if (balance < value)
		{
			return false;
		}
		SetBalance(balance - value);
		if (onBalanceChanged != null)
		{
			onBalanceChanged();
		}
		return true;
	}
}
