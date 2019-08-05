using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqureObject : MonoBehaviour
{
    public int uniqureId = 0;
    private bool isFalling = false;
    public GameObject particle;
    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        if (isFalling == true)
            return;
        if (GameController.instance.isGameOver == true)
            return;

        if(gameObject.transform.position.y < -2.5f)
        {
            Debug.LogError("Unique Obtain");
            isFalling = true;
            PlayerPrefs.SetInt("UniqueItem_" + (uniqureId).ToString(), 1);
            GameController.instance.HasUniqueItem(uniqureId);
            ShowGetAnimalEffect();
        }
    }

    void ShowGetAnimalEffect()
    {
        particle.SetActive(true);
        SoundManager.Instance.PlayGiftSFX();
        if (transform.GetComponent<Rigidbody>() != null)
            transform.GetComponent<Rigidbody>().isKinematic = true;
        else
            transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;

        foreach (Collider col in transform.GetComponents<Collider>())
        {
            col.enabled = false;
        }
        foreach (Collider col in transform.GetComponentsInChildren<Collider>())
        {
            col.enabled = false;
        }
        //transform.GetComponent<Collider>().enabled = false;

        transform.position = new Vector3(0,0, Camera.main.transform.position.z + 23);
        transform.localEulerAngles = new Vector3(-90,0,40);

        Debug.Log(transform.position.y + "  " + transform.localEulerAngles);

        iTween.MoveTo(gameObject, iTween.Hash("y", 6, "time", 2, "onComplete", "HideObj", "easeType", iTween.EaseType.easeInCubic));
        //iTween.RotateTo(gameObject, iTween.Hash("y", 300, "looptype", iTween.LoopType.pingPong, "time", 2, "easeType", iTween.EaseType.linear));
    }

    void HideObj()
    {
        gameObject.SetActive(false);
    }
}
