using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoTracker : MonoBehaviour
{
    public PlayerController myPlayer;
    private RoundManager rm;
    public int id;

	public float hp;
    public int maxHP = 100;
    public int shield = 0;
    public int maxShield = 0;
    private int tempMaxShield;
    private bool dead = false;

    public Image hpBar;
    public Text hpText;
    public Text shieldText;
    public Image shieldBar;
    public Image shieldAmount;
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
        shieldBar.gameObject.SetActive(false);
        shield = maxShield;
        tempMaxShield = maxShield;
        if (shield > 0)
        {
            shieldText.text = shield.ToString();
            shieldBar.rectTransform.sizeDelta = new Vector2(180f * (float)maxShield / 100, shieldBar.rectTransform.sizeDelta.y);
            shieldAmount.rectTransform.sizeDelta = new Vector2(180f * (float)maxShield / 100, shieldAmount.rectTransform.sizeDelta.y);
            shieldAmount.fillAmount = 1f;
            shieldBar.gameObject.SetActive(true);
        }
        StartCoroutine(startAmmo());
        id = myPlayer.playerID;
        rm = GameObject.FindGameObjectWithTag("Manager").GetComponent<RoundManager>();
    }

    private void Update()
    {
        int ammo = myPlayer._weaponSystems.mag;
        ammoText.text = ammo.ToString() + " / " + magSize;
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

    // changed HP from int to float to allow for frame-by-frame regen of HP 
    public void AddHealth(float added)
    {
        hp += added;
        if(hp > maxHP)
        {
            hp = maxHP;
        }
        int displayHP = (int)hp;
        hpText.text = displayHP.ToString() + " / " + maxHP.ToString();
        hpBar.fillAmount = hp / maxHP;
    }

    public void AddShield(int added)
    {
        int total = shield + added;
        if(total > tempMaxShield)
        {
            tempMaxShield = total;
        }
        shieldBar.rectTransform.sizeDelta = new Vector2(180f * (float)tempMaxShield / 100, shieldBar.rectTransform.sizeDelta.y);
        shieldAmount.rectTransform.sizeDelta = new Vector2(180f * (float)tempMaxShield / 100, shieldAmount.rectTransform.sizeDelta.y);
        shield += added;
        shieldBar.gameObject.SetActive(true);
        shieldAmount.fillAmount = (float)shield / tempMaxShield;
        shieldText.text = shield.ToString();
        shieldBar.GetComponent<Animator>().Play("shieldAppear");
    }

    public void AddXray(int activeTime)
    {
        StartCoroutine(Xray(activeTime));
    }

    private IEnumerator Xray(int uptime)
    {
        GameObject x = gameObject.transform.Find("Xray").gameObject;
        x.SetActive(true);
        yield return new WaitForSeconds(uptime);
        x.SetActive(false);
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
                    shieldText.text = shield.ToString();
                    shieldAmount.fillAmount = (float) shield / tempMaxShield;
                }
                else
                {
                    shield = 0;
                    shieldText.text = "";
                    shieldBar.fillAmount = 0f;
                    shieldBar.gameObject.SetActive(false);
                }
            }
        }

        if(taken > 0)
        {
            shield = 0;
            shieldText.text = "";
            shieldAmount.fillAmount = (float)shield / tempMaxShield;
            shieldBar.gameObject.SetActive(false);
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
                int displayHP = (int)hp;
                hpText.text = displayHP.ToString() + " / " + maxHP.ToString();
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
        tempMaxShield = maxShield;
        if(shield > 0)
        {
            shieldText.text = shield.ToString();
            shieldAmount.fillAmount = 1;
            shieldBar.gameObject.SetActive(true);
        }
        else
        {
            shieldText.text = "";
            shieldAmount.fillAmount = 0;
            shieldBar.gameObject.SetActive(false);
        }
        resetAmmo();
    }
}
