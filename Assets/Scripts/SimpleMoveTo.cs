using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMoveTo : MonoBehaviour
{
    //public Transform addPos;
    public Vector3 difference;
    public GameObject targetObject;
    // Update is called once per frame
    void Update()
    {
        /*
        Vector3 newPos;
        newPos = addPos.position;
        newPos += difference;
        transform.position = newPos;
        */

        Vector3 newPos;
        newPos = targetObject.transform.position;
        newPos += difference;
        transform.position = newPos;
        transform.LookAt(targetObject.transform);

    }
}
