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
        Debug.Log(transform.GetComponent<Rigidbody>().centerOfMass);
        //transform.GetComponent<Rigidbody>().centerOfMass = transform.position;
    }

    // Update is called once per frame
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
            PlayerPrefs.SetInt("UniqueItem_" + (uniqureId + 1).ToString(), 1);
            ShowGetAnimalEffect();
        }
    }

    void ShowGetAnimalEffect()
    {
        particle.SetActive(true);
        transform.GetComponent<Rigidbody>().isKinematic = true;
        transform.GetComponent<Collider>().enabled = false;

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
