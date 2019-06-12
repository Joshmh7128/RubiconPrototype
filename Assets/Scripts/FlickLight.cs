using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickLight : MonoBehaviour
{
    private Light lt;

    // Start is called before the first frame update
    void Start()
    {
        lt = this.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(lt, 0.2f);
    }
}
