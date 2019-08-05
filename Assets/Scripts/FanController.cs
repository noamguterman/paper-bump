using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanController : MonoBehaviour
{
    //Sequence seq;
    //Vector3 initPos;
    //public Transform wingObj;
    //[SerializeField]
    //private float rotateSpeed = 3;
    //void Start()
    //{
    //    initPos = transform.position;

    //    StartMove();
    //}

    //public void StartMove()
    //{
    //    seq = DOTween.Sequence();

    //    seq.Append(transform.DOMoveX(7, 4))
    //        .AppendInterval(3)
    //        .Append(transform.DOMoveX(-7, 4))
    //        .AppendInterval(3)
    //        .SetLoops(-1, LoopType.Yoyo)
    //        .SetEase(Ease.Linear);

    //    seq.Play();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    wingObj.transform.Rotate(Vector3.forward, 360 * rotateSpeed * Time.deltaTime);
    //}

    public GameObject virtualPlayer;
    Vector3 distance;
    public GameObject fanObj;
    private void Start()
    {
        distance = transform.position - virtualPlayer.transform.position;
    }
    private void Update()
    {
        transform.position = virtualPlayer.transform.position + distance;
        //fanObj.transform.Rotate(Vector3.forward, Time.deltaTime * 36);
    }
}
