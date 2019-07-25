using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackCube : MonoBehaviour
{
    int floorMask;
    void Start()
    {
        floorMask = LayerMask.GetMask("Plane");
    }

    void FixedUpdate()
    {
        Ray camRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, 0, 0));
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, 100, floorMask))
        {
            transform.position = new Vector3(floorHit.point.x, 0, floorHit.point.z) - new Vector3(0,0,0.8f);
        }
    }
}
