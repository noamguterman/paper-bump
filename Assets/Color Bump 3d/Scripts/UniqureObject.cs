using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqureObject : MonoBehaviour
{
    public int uniqureId = 0;
    private bool isFalling = false;
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
        }
    }
}
