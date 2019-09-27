using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public Renderer thisRend;
    public Material fullHP;
    private int fullHPNum = 20;
    public Material breakHP;
    private int breakHPNum = 10;
    public GameObject redBurst;
    public int hp;

    public void Start()
    {
        thisRend = gameObject.GetComponent<Renderer>();
        hp = fullHPNum;
    }

    // update
    public void Update() 
    {   
        if (hp > breakHPNum)
        {
            thisRend.material = fullHP;
        }

        if (hp <= breakHPNum)
        {
            thisRend.material = breakHP;
        }
        
        // if the hp of the breakable is 0 then break it
        if (hp <= 0)
        {
            Instantiate(redBurst, this.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }


    public void TakeDamage(int dam)
    {
        hp -= dam;
    }
}
