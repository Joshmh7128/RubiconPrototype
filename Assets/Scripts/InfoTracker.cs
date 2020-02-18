using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InfoTracker : MonoBehaviour
{
    public PlayerController myPlayer;
    private Rewired.Player rewiredPlayer;
    public RoundManager rm;
    public int id;

	public float hp;
    public int maxHP = 100;
    public int armor = 0;
    public int maxArmor = 0;
    private int tempMaxArmor;
    private bool dead = false;

    public Image hpBar;
    public Text hpText;
    public Text armorText;
    public Image armorBar;
    public Image armorAmount;
    public GameObject reloadPrompt;
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
        armorBar.gameObject.SetActive(false);
        armor = maxArmor;
        tempMaxArmor = maxArmor;
        if (armor > 0)
        {
            armorText.text = armor.ToString();
            armorBar.rectTransform.sizeDelta = new Vector2(180f * (float)maxArmor / 100, armorBar.rectTransform.sizeDelta.y);
            armorAmount.rectTransform.sizeDelta = new Vector2(180f * (float)maxArmor / 100, armorAmount.rectTransform.sizeDelta.y);
            armorAmount.fillAmount = 1f;
            armorBar.gameObject.SetActive(true);
        }
        //StartCoroutine(startAmmo());
        id = myPlayer.playerID;
        rm = GameObject.FindGameObjectWithTag("Manager").GetComponent<RoundManager>();
        rewiredPlayer = Rewired.ReInput.players.GetPlayer(myPlayer.playerID - 1);
    }

    private void Update()
    {
        int ammo = myPlayer._weaponSystems.mag;
        reloadPrompt.GetComponent<Animator>().SetInteger("ammo", ammo);
        /*
        if(ammo == 1)
        {
            reloadPrompt.SetActive(true);
            reloadPrompt.GetComponent<Animator>().Play("reloadPromptFaded");
        }
        else if (ammo < 1)
        {
            reloadPrompt.SetActive(true);
        }
        else
        {
            reloadPrompt.SetActive(false);
        }
        */
        int mag = myPlayer._weaponSystems.magSize;
        ammoText.text = ammo.ToString() + " / " + mag;

        if (rewiredPlayer.GetButton("Pause") && !rm.isPaused && !rm.isOver)
        {
            rm.isPaused = true;
            rm.PauseGame();
        }

        if(rewiredPlayer.GetButton("A") && rm.isPaused && !rm.isOver)
        {
            rm.isPaused = false;
            rm.ResumeGame();
        }

        if(rewiredPlayer.GetButton("B") && rm.isPaused)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("NewMainMenu");
        }

    }

    /*
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

    */

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

    public void AddArmor(int added)
    {
        int total = armor + added;
        if(total > 100)
        {
            total = 100;
            added = 100 - armor;
        }
        if(total > tempMaxArmor)
        {
            tempMaxArmor = total;
        }
        armorBar.rectTransform.sizeDelta = new Vector2(180f * (float)tempMaxArmor / 100, armorBar.rectTransform.sizeDelta.y);
        armorAmount.rectTransform.sizeDelta = new Vector2(180f * (float)tempMaxArmor / 100, armorAmount.rectTransform.sizeDelta.y);
        armor += added;
        armorBar.gameObject.SetActive(true);
        armorAmount.fillAmount = (float)armor / tempMaxArmor;
        armorText.text = armor.ToString();
        armorBar.GetComponent<Animator>().Play("shieldAppear");
    }

    public void AddXray(int activeTime)
    {
        if(!dead)
        {
            StartCoroutine(Xray(activeTime));
        }
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
        if (armor > 0)
        {
            int temp = taken;
            taken -= armor;
            armor -= temp;
            if (taken <= 0)
            {
                taken = 0;
                if (armor > 0)
                {
                    armorText.text = armor.ToString();
                    armorAmount.fillAmount = (float)armor / tempMaxArmor;
                }
                else
                {
                    armor = 0;
                    armorText.text = "";
                    armorBar.fillAmount = 0f;
                    armorBar.gameObject.SetActive(false);
                }
            }
        }

        if(taken > 0)
        {
            armor = 0;
            armorText.text = "";
            armorAmount.fillAmount = (float)armor / tempMaxArmor;
            armorBar.gameObject.SetActive(false);
            hp -= taken;
            if (hp <= 0 && !dead)
            {
                hpBar.fillAmount = 0;
                dead = true;
                Die();
            }
            else if (hp > 0)
            {
                int exKey = Random.Range(1, 101);
                if (exKey >= (103 - taken))
                {
                    rm.sm.PlaySound("exclamation");
                }
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
        playerCanvas.transform.Find("ToHide").gameObject.SetActive(false);
    }

    public void ResetStats()
    {
        playerCanvas.transform.Find("ToHide").gameObject.SetActive(true);
        dead = false;
        redAnim.Play("redIdle");
        hp = maxHP;
        hpBar.fillAmount = 1;
        hpText.text = hp.ToString() + " / " + maxHP.ToString();
        armor = maxArmor;
        tempMaxArmor = maxArmor;
        if(armor > 0)
        {
            armorText.text = armor.ToString();
            armorAmount.fillAmount = 1;
            armorBar.gameObject.SetActive(true);
        }
        else
        {
            armorText.text = "";
            armorAmount.fillAmount = 0;
            armorBar.gameObject.SetActive(false);
        }
        //resetAmmo();
    }
}
