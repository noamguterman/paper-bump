using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObj : MonoBehaviour
{
    public float distanceToPlayer = -1f;

    private void Start()
    {
    }

    private void Update()
    {
        if (distanceToPlayer != -1f)
        {
            Vector3 position3 = base.transform.position;
            float z2 = position3.z;
            Vector3 position4 = PlayerController.instance.transform.position;
            if (!(z2 - position4.z < distanceToPlayer))
            {
                return;
            }
            goto IL_0091;
        }
        return;
    IL_0091:

        for(int i= 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        enabled = false;
    }
}
