using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    public GameObject[] prefab_Animals;

    void Start()
    {
        Instantiate(prefab_Animals[GetAnimalIdx_ToShow()], transform);
    }

    public int GetAnimalIdx_ToShow()
    {
        for(int i = 0; i < prefab_Animals.Length; i++)
        {
            if (PlayerPrefs.GetInt("UniqueItem_" + i.ToString(), 0) == 1)
                continue;
            else
                return i-1;
        }

        return prefab_Animals.Length - 1;
    }
}
