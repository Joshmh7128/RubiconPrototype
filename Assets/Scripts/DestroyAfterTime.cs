using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public int key;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, key);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
