using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public GameObject root;
    public Text txt_progress;
    public GameObject btn_retry;
    public Text txt_timer;

    private Vector3 initPos;
    private Vector3 initRetryPos; 
    public int countTime = 4;

    void Awake()
    {
        initPos = transform.localPosition;
        initRetryPos = btn_retry.transform.localPosition;
    }

    private void OnEnable()
    {
        transform.localPosition = initPos;
        btn_retry.transform.localPosition = initRetryPos;

        root.transform.localScale = Vector3.one;
        
        iTween.MoveTo(gameObject, iTween.Hash("x", 0, "islocal", true, "time", 1f));

        txt_timer.text = countTime.ToString();
        txt_progress.text = ((int)(GameController.instance.completeProgress * 100)).ToString() + "% COMPLETED";

        StartCoroutine(TimeCountAction());

        iTween.MoveTo(btn_retry, iTween.Hash("y", -330f, "islocal", true, "time", 1f, "delay", 1));
    }

    public void HidePanel()
    {
        iTween.MoveTo(gameObject, iTween.Hash("x", initPos.x, "islocal", true, "time", 1f));
    }

    IEnumerator TimeCountAction()
    {
        yield return new WaitForSeconds(1);
        int cnt = 0;
        while ((countTime - cnt) > 0)
        {
            yield return new WaitForSeconds(1);
            cnt++;
            iTween.ScaleTo(root, iTween.Hash( "x", 0.9f, "y",0.9f, "time", 0.1f ));
            iTween.ScaleTo(root, iTween.Hash("x", 1f, "y", 1f, "time", 0.1f, "delay", 0.1f));
            txt_timer.text = (countTime - cnt).ToString();
        }

    }

    private void OnDisable()
    {
        transform.localPosition = initPos;
        btn_retry.transform.localPosition = initRetryPos;

        StopAllCoroutines();
    }
}
