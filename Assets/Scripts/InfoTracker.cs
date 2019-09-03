using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoTracker : MonoBehaviour
{
    public PlayerController myPlayer;
    private RoundManager rm;
    public int id;

	public int hp;
    private int maxHP = 100;
    public int shield = 0;
    public int maxShield = 0; 
    private bool dead = false;

    public Image hpBar;
    public Text hpText;
    public Text shieldText;
    public Animator redAnim;

    public Text ammoText;
    public int magSize;

    public Canvas playerCanvas;

	// Start is called before the first frame update
	void Start()
    {
        playerCanvas.enabled = true;
        hp = maxHP;
        hpText.text = hp.ToString() + " / " + maxHP.ToString();
        hpBar.fillAmount = 1f;
        shield = maxShield;
        if(maxShield > 0)
        {
            shieldText.text = "Shield: " + shield.ToString();
        }
        else
        {
            shieldText.text = "";
        }
        StartCoroutine(startAmmo());
        id = myPlayer.playerID;
        rm = GameObject.FindGameObjectWithTag("Manager").GetComponent<RoundManager>();
    }

    public void updateAmmo(int ammo)
    {
        ammoText.text = ammo.ToString() +  " / " + magSize;
    }

    private IEnumerator startAmmo()
    {
        yield return new WaitForEndOfFrame();
        magSize = myPlayer._weaponSystems.magSize;
        resetAmmo();
    }

    public void resetAmmo()
    {
        ammoText.text = magSize + " / " + magSize;
    }

    public void TakeDamage(int taken)
    {
        if (shield > 0)
        {
            int temp = taken;
            taken -= shield;
            shield -= temp;
            if (taken <= 0)
            {
                taken = 0;
                if (shield > 0)
                {
                    shieldText.text = "Shield: " + shield.ToString();
                }
                else
                {
                    shield = 0;
                    shieldText.text = "";
                }
            }
        }

        if(taken > 0)
        {
            shield = 0;
            shieldText.text = "";
            hp -= taken;
            if (hp <= 0 && !dead)
            {
                hpBar.fillAmount = 0;
                dead = true;
                Die();
            }
            else if (hp > 0)
            {
                hpBar.fillAmount = (float)hp / maxHP;
                hpText.text = hp.ToString() + " / " + maxHP.ToString();
                redAnim.Play("redFlash");
            }
        }
           
    }

    public void Die()
    {
        Hide();
        redAnim.Play("redDead");
        rm.updateScore(id);
    }

    public void Hide()
    {
        playerCanvas.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void ResetStats()
    {
        playerCanvas.transform.GetChild(0).gameObject.SetActive(true);
        dead = false;
        redAnim.Play("redIdle");
        hp = maxHP;
        hpBar.fillAmount = 1;
        hpText.text = hp.ToString() + " / " + maxHP.ToString();
        shield = maxShield;
        if(shield > 0)
        {
            shieldText.text = "Shield: " + shield.ToString();
        }
        else
        {
            shieldText.text = "";
        }
        resetAmmo();
    }
}
