using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashScript : MonoBehaviour
{
    public Light lt;
    private ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ps.time < 0.7)
        {
            lt.enabled = true;
        }
        else
        {
            lt.enabled = false;
        }
    }
}
