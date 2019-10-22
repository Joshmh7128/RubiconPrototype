using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeObject : MonoBehaviour
{
    private Vector3 originPosition;
    public float shakeIntensity = .3f;

    private void Start()
    {
        originPosition = this.transform.localPosition;
    }

    void Update()
    {
        if (shakeIntensity > 0)
        {
            transform.localPosition = originPosition + Random.insideUnitSphere * shakeIntensity;
        }
    }
}
