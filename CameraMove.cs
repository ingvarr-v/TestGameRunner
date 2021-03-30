using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public GameObject followObj;


    void Update()
    {
        transform.position = new Vector3(0, 4.5f, followObj.transform.position.z - 5);
    }
}
