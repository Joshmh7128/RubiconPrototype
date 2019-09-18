using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public Renderer thisRend;
    public Material fullHP;
    public Material breakHP;
    public GameObject redBurst;
    public int hp;

    public void Start()
    {
        thisRend = gameObject.GetComponent<Renderer>();
    }

    // update
    public void Update() 
    {   
        if (hp == 1)
        {
            thisRend.material = fullHP;
        }

        if (hp == 0)
        {
            thisRend.material = breakHP;
        }
        
        // if the hp of the breakable is 0 then break it
        if (hp < 0)
        {
            Instantiate(redBurst, this.transform.position, Quaternion.identity);
            Instantiate(redBurst, this.transform.position, Quaternion.identity);
            Instantiate(redBurst, this.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }


    public void TakeDamage()
    {
        hp -= 1;
    }
}
