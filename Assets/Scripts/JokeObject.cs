using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokeObject : MonoBehaviour
{
    private int hp = 69;
    public SoundManager sm;

    public void TakeDamage(int dam)
    {
        hp -= dam;
        if(hp <= 0)
        {
            sm.PlaySound("joke");
            hp = 69;
        }
    }
}
