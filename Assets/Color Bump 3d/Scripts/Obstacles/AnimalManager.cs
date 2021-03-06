using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    public GameObject[] prefab_Animals;

    void Start()
    {
        if(PlayerPrefs.GetInt("CollectAll", 0) == 1)
        {
            Instantiate(prefab_Animals[Random.Range(0,12)], transform);
        }
        else
        {
            Instantiate(prefab_Animals[GetAnimalIdx_ToShow()], transform);
        }
    }

    public int GetAnimalIdx_ToShow()
    {
        for(int i = 0; i < prefab_Animals.Length; i++)
        {
            if (PlayerPrefs.GetInt("UniqueItem_" + i.ToString(), 0) == 1)
                continue;
            else
                return i;
        }

        return prefab_Animals.Length - 1;
    }
}
