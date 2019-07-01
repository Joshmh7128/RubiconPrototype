using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTracker : MonoBehaviour
{
    public int hp;
    private int maxHP = 6;
    public GameObject bar;
    public Animator anim;
    public Animator flashAnim;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHP;
        bar.transform.localScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int taken)
    {
        hp -= taken;
        anim.Play("HPbump");
        if(hp < 0)
        {
            hp = 0;
        }
        flashAnim.Play("redFlash");
        bar.transform.localScale = new Vector3((float)hp / maxHP, 1, 1);
    }
}
