using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromoScript : MonoBehaviour
{
    public PlayerController pc;
    private bool paused = false;

    private void Update()
    {
        pc.weaponLocked = true;
        if (Input.GetKey(KeyCode.P))
        {
            paused = !paused;
        }
        if(paused)
        {
            Time.timeScale = 0;
            pc.weaponLocked = true;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
