using UnityEngine;
using System;

public static class GameState
{
    public static int numPlayed;

    public static int Coins
    {
        get
        {
            return PlayerPrefs.GetInt("Coins");
        }
        set
        {
            PlayerPrefs.SetInt("Coins", value);
        }
    }
}
