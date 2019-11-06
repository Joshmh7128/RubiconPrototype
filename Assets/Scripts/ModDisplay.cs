using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModDisplay : MonoBehaviour
{
    public GameObject[] Player1Panels = new GameObject[4];
    public GameObject[] Player2Panels = new GameObject[4];

    public ModApplication ma1;
    public ModApplication ma2;

    public Sprite upArrow;
    public Sprite downArrow;

    private void Start()
    {
        for(int i = 1; i <= 4; i++)
        {
            Player1Panels[i - 1] = transform.Find("P1_Mod" + i.ToString()).gameObject;
            Player2Panels[i - 1] = transform.Find("P2_Mod" + i.ToString()).gameObject;
        }
        ResetPanels();
    }

    public void ResetPanels()
    {
        for (int i = 0; i < Player1Panels.Length; i++)
        {
            Player1Panels[i].SetActive(false);
            Player2Panels[i].SetActive(false);
        }
    }

    public void ActivateMod(int targetPlayer, bool isGood, int modNum, int modKey)
    {
        if(targetPlayer == 1)
        {
            Image arrow = Player1Panels[modNum].transform.Find("Image").GetComponent<Image>();
            if(isGood)
            {
                arrow.sprite = upArrow;
                arrow.color = Color.green;
            }
            else
            {
                arrow.sprite = downArrow;
                arrow.color = Color.red;
            }
            Text modName = Player1Panels[modNum].transform.Find("Text").GetComponent<Text>();
            if (modKey == 1)
            {
                modName.text = "Shield";
                ma1.ActivateShield();
            }
            if (modKey == 2)
            {
                modName.text = "Regeneration";
                ma1.ActivateRegen();
            }
            if (modKey == 3)
            {
                modName.text = "Scatter";
                ma1.ActivateScatter();
            }
            if (modKey == 4)
            {
                modName.text = "Armor";
                ma1.ActivateArmor();
            }
            if (modKey == 5)
            {
                modName.text = "Speed";
                ma1.ActivateSpeed();
            }
            if (modKey == 6)
            {
                modName.text = "Stealth";
                ma1.ActivateStealth();
            }
            if (modKey == 7)
            {
                modName.text = "Supercharge";
                ma1.ActivateSupercharge();
            }
            if (modKey == 8)
            {
                modName.text = "Vampirism";
                ma1.ActivateVampirism();
            }
            if (modKey == 9)
            {
                modName.text = "Glowing";
                ma1.ActivateGlow();
            }
            if (modKey == 10)
            {
                modName.text = "Large";
                ma1.ActivateLarge();
            }
            if (modKey == 11)
            {
                modName.text = "Tracking";
                ma1.ActivateTracking();
            }
            if (modKey == 12)
            {
                modName.text = "Tunnel Vision";
                ma1.ActivateTunnelVision();
            }

            Player1Panels[modNum].SetActive(true);

        }
        if (targetPlayer == 2)
        {
            Image arrow = Player2Panels[modNum].transform.Find("Image").GetComponent<Image>();
            if (isGood)
            {
                arrow.sprite = upArrow;
                arrow.color = Color.green;
            }
            else
            {
                arrow.sprite = downArrow;
                arrow.color = Color.red;
            }
            Text modName = Player2Panels[modNum].transform.Find("Text").GetComponent<Text>();
            if (modKey == 1)
            {
                modName.text = "Armor";
                ma2.ActivateArmor();
            }
            if (modKey == 2)
            {
                modName.text = "Regeneration";
                ma2.ActivateRegen();
            }
            if (modKey == 3)
            {
                modName.text = "Scatter";
                ma2.ActivateScatter();
            }
            if (modKey == 4)
            {
                modName.text = "Shield";
                ma2.ActivateShield();
            }
            if (modKey == 5)
            {
                modName.text = "Speed";
                ma2.ActivateSpeed();
            }
            if (modKey == 6)
            {
                modName.text = "Stealth";
                ma2.ActivateStealth();
            }
            if (modKey == 7)
            {
                modName.text = "Supercharge";
                ma2.ActivateSupercharge();
            }
            if (modKey == 8)
            {
                modName.text = "Vampirism";
                ma2.ActivateVampirism();
            }
            if (modKey == 9)
            {
                modName.text = "Glowing";
                ma2.ActivateGlow();
            }
            if (modKey == 10)
            {
                modName.text = "Large";
                ma2.ActivateLarge();
            }
            if (modKey == 11)
            {
                modName.text = "Tracking";
                ma2.ActivateTracking();
            }
            if (modKey == 12)
            {
                modName.text = "Tunnel Vision";
                ma2.ActivateTunnelVision();
            }

            Player2Panels[modNum].SetActive(true);

        }
    }

}
