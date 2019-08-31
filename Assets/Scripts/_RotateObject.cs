using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _RotateObject : MonoBehaviour
{
    //use this to make objects rotate
    public float speed;
    void Update()
    {
        gameObject.transform.Rotate(Vector3.up*speed*Time.deltaTime);
    }
}
