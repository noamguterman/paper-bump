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

    public Image[] trophiesBG;
    public Image[] trophies;

    public Sprite[] img_BG;
    public ScrollRect scrollrect;
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
        iTween.MoveTo(btn_next, iTween.Hash("y", -450, "islocal", true, "time", 1f, "delay", 2));

        pageNum = 0;
        for (int i = 0; i < 12; i++)
        {
            if (PlayerPrefs.GetInt("UniqueItem_" + i.ToString(), 0) == 1)
            {
                pageNum = i;
                trophiesBG[i].sprite = img_BG[1];
                trophies[i].enabled = true;
            }
        }

        //Canvas.ForceUpdateCanvases();
        //scrollrect.verticalNormalizedPosition = 1 - (float)pageNum / 12;
        StartCoroutine(ResetPos());
    }

    IEnumerator ResetPos()
    {
        yield return new WaitForEndOfFrame();
        scrollrect.verticalNormalizedPosition = 1 - (float)pageNum / 12;

    }
}
