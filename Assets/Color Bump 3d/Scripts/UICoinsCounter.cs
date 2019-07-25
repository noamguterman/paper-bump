using UnityEngine;
using UnityEngine.UI;

public class UICoinsCounter : MonoBehaviour
{
    private Text txt;
    private int Coins;


    void Start()
    {
        txt = this.gameObject.GetComponent<Text>();
        Coins = GameState.Coins;
    }


    void Update()
    {
        txt.text = "" + Coins;

        if (Coins < GameState.Coins)
        {
            //If 10 or less
            if ((Coins < GameState.Coins) && ((Coins + 10) > GameState.Coins))
            {
                Coins += 1;
            }
            //If more than 10
            else if (((Coins + 10) <= GameState.Coins) && (Coins + 100 > GameState.Coins))
            {
                Coins += 5;
            }
            //If more than 100
            else if (((Coins + 100) <= GameState.Coins) && (Coins + 1000 > GameState.Coins))
            {
                Coins += 20;
            }
            //If more than 1000
            else if (((Coins + 1000) <= GameState.Coins) && (Coins + 10000 > GameState.Coins))
            {
                Coins += 100;
            }
            //If more than 10000
            else if (((Coins + 10000) <= GameState.Coins) && (Coins + 100000 > GameState.Coins))
            {
                Coins += 1000;
            }
        }
        else if (Coins >= GameState.Coins)
        {
            Coins = GameState.Coins;
        }
    }
}