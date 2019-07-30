using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChild : MonoBehaviour
{
    public GameObject bgObj;
    public GameObject lockObj;
    public GameObject characterObj;

    void Start()
    {
        //ShowHide(false);
    }

    public void ShowHide(bool state)
    {
        if (state)
        {
            bgObj.GetComponent<Renderer>().enabled = false;
            lockObj.GetComponent<Renderer>().enabled = false;
            characterObj.SetActive(true);
        }
        else
        {
            bgObj.GetComponent<Renderer>().enabled = true;
            lockObj.GetComponent<Renderer>().enabled = true;
            characterObj.SetActive(false);
        }
    }
}
