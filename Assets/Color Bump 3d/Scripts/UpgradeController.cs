using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UpgradeController : MonoBehaviour
{
    public CharacterRoot characterRoot;
    public Text txt_progress;
    public GameObject btn_next;
    public Text txt_coin;
    public Text txt_power;

    int pageNum = 0;
    int maxPage;
    void Awake()
    {
        characterRoot = FindObjectOfType<CharacterRoot>();
    }

    private void Start()
    {
        maxPage = (characterRoot.characters.Length - 1) / 4 + 1;
        Debug.Log("MaxPage = " + maxPage);
    }

    private void OnEnable()
    {
        txt_progress.text = "";// ((int)(GameController.instance.completeProgress * 100)).ToString() + "% COMPLETED";

        iTween.MoveTo(gameObject, iTween.Hash("x", 0, "islocal", true, "time", 1f));
        iTween.MoveTo(btn_next, iTween.Hash("y", -550, "islocal", true, "time", 1f, "delay", 2));

        pageNum = 0;
        for (int i = 0; i < 12; i++)
        {
            if (PlayerPrefs.GetInt("UniqueItem_" + i.ToString(), 0) == 1)
                continue;
            else
            {
                if(i > 8)
                {
                    pageNum = 2;
                    Invoke("ShowPage", 2);
                    return;
                }
                else if (i > 4)
                {
                    pageNum = 1;
                    Invoke("ShowPage", 1);
                    return;
                }
                else
                {
                    pageNum = 0;
                    Invoke("ShowPage", 1);
                    return;
                }
            }
        }

        pageNum = 2;
        Invoke("ShowPage", 2);
    }

    private void ShowPage()
    {
        characterRoot.ShowPage(pageNum);
    }

    private void OnDisable()
    {
    }

    public void onclick_Down()
    {
        pageNum++;
        if (pageNum == maxPage)
            pageNum = 0;
        ShowPage();
    }
}
