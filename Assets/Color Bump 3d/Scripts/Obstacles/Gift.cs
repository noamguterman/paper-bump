using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : MonoBehaviour
{
    public GameObject effect;
    bool iscollided = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
            iTween.MoveTo(gameObject, iTween.Hash("y", 5, "islocal", true, "time", 6));
        //transform.Rotate(Vector3.up, 120 * Time.deltaTime);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player" && iscollided == false)
        {
            iscollided = true;
            effect.SetActive(true);
            GameState.Coins += 300;
            StartCoroutine(MoveUp());
            SoundManager.Instance.PlayGiftSFX();
        }
    }

    IEnumerator MoveUp()
    {
        yield return new WaitForSeconds(0.3f);
        transform.GetComponent<Collider>().enabled = false;
        transform.GetComponent<Rigidbody>().isKinematic = true;
        iTween.MoveTo(gameObject, iTween.Hash("y", 20, "islocal", true, "time", 5f));
    }
}
