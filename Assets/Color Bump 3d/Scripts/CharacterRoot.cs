using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRoot : MonoBehaviour
{
    public GameObject[] characters;

    private Vector3 offset;
    private void Awake()
    {
        //PlayerPrefs.SetInt("UniqueItem_" + 0, 1);
    }

    private void Start()
    {
        //Invoke("SetParent", 0.1f);
    }

    private void SetParent()
    {
        transform.parent = Camera.main.transform;
    }
    public void HideAll()
    {
        for(int i = 0; i < characters.Length; i++)
        {
            characters[i].SetActive(false);
        }
    }

    public void ShowPage(int num)
    {
        HideAll();
        int last = Mathf.Min(num * 4 + 3, characters.Length - 1);
        for(int i=num * 4; i <= last; i++)
        {
            characters[i].SetActive(true);

            if (PlayerPrefs.GetInt("UniqueItem_" + i.ToString(), 0) == 0)
                characters[i].GetComponent<CharacterChild>().ShowHide(false);
            else
                characters[i].GetComponent<CharacterChild>().ShowHide(true);
        }

    }
}
