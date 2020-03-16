using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModDisplay : MonoBehaviour
{
    public RoundManager rm;

    public GameObject[] Player1Panels = new GameObject[4];
    public GameObject[] Player2Panels = new GameObject[4];
    public GameObject[] Player3Panels = new GameObject[4];
    public GameObject[] Player4Panels = new GameObject[4];

    public ModApplication ma1;
    public ModApplication ma2;
    public ModApplication ma3;
    public ModApplication ma4;

    public Sprite upArrow;
    public Sprite downArrow;

    private bool fourPlayer = false;

    private void Start()
    {
        if(rm.players == 4)
        {
            fourPlayer = true;
        }

        for(int i = 1; i <= 4; i++)
        {
            Player1Panels[i - 1] = transform.Find("P1_Mod" + i.ToString()).gameObject;
            Player2Panels[i - 1] = transform.Find("P2_Mod" + i.ToString()).gameObject;
            if(fourPlayer)
            {
                Player3Panels[i - 1] = transform.Find("P3_Mod" + i.ToString()).gameObject;
                Player4Panels[i - 1] = transform.Find("P4_Mod" + i.ToString()).gameObject;
            }
        }
        ResetPanels();
    }

    public void ResetPanels()
    {
        for (int i = 0; i < Player1Panels.Length; i++)
        {
            Player1Panels[i].SetActive(false);
            Player2Panels[i].SetActive(false);
            if(fourPlayer)
            {
                Player3Panels[i].SetActive(false);
                Player4Panels[i].SetActive(false);
            }
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
                modName.text = "Armor";
                ma1.ActivateArmor();
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
                modName.text = "Shield";
                ma1.ActivateShield();
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
        if (targetPlayer == 3)
        {
            Image arrow = Player3Panels[modNum].transform.Find("Image").GetComponent<Image>();
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
            Text modName = Player3Panels[modNum].transform.Find("Text").GetComponent<Text>();
            if (modKey == 1)
            {
                modName.text = "Armor";
                ma3.ActivateArmor();
            }
            if (modKey == 2)
            {
                modName.text = "Regeneration";
                ma3.ActivateRegen();
            }
            if (modKey == 3)
            {
                modName.text = "Scatter";
                ma3.ActivateScatter();
            }
            if (modKey == 4)
            {
                modName.text = "Shield";
                ma3.ActivateShield();
            }
            if (modKey == 5)
            {
                modName.text = "Speed";
                ma3.ActivateSpeed();
            }
            if (modKey == 6)
            {
                modName.text = "Stealth";
                ma3.ActivateStealth();
            }
            if (modKey == 7)
            {
                modName.text = "Supercharge";
                ma3.ActivateSupercharge();
            }
            if (modKey == 8)
            {
                modName.text = "Vampirism";
                ma3.ActivateVampirism();
            }
            if (modKey == 9)
            {
                modName.text = "Glowing";
                ma3.ActivateGlow();
            }
            if (modKey == 10)
            {
                modName.text = "Large";
                ma3.ActivateLarge();
            }
            if (modKey == 11)
            {
                modName.text = "Tracking";
                ma3.ActivateTracking();
            }
            if (modKey == 12)
            {
                modName.text = "Tunnel Vision";
                ma3.ActivateTunnelVision();
            }

            Player3Panels[modNum].SetActive(true);

        }
        if (targetPlayer == 4)
        {
            Image arrow = Player4Panels[modNum].transform.Find("Image").GetComponent<Image>();
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
            Text modName = Player4Panels[modNum].transform.Find("Text").GetComponent<Text>();
            if (modKey == 1)
            {
                modName.text = "Armor";
                ma4.ActivateArmor();
            }
            if (modKey == 2)
            {
                modName.text = "Regeneration";
                ma4.ActivateRegen();
            }
            if (modKey == 3)
            {
                modName.text = "Scatter";
                ma4.ActivateScatter();
            }
            if (modKey == 4)
            {
                modName.text = "Shield";
                ma4.ActivateShield();
            }
            if (modKey == 5)
            {
                modName.text = "Speed";
                ma4.ActivateSpeed();
            }
            if (modKey == 6)
            {
                modName.text = "Stealth";
                ma4.ActivateStealth();
            }
            if (modKey == 7)
            {
                modName.text = "Supercharge";
                ma4.ActivateSupercharge();
            }
            if (modKey == 8)
            {
                modName.text = "Vampirism";
                ma4.ActivateVampirism();
            }
            if (modKey == 9)
            {
                modName.text = "Glowing";
                ma4.ActivateGlow();
            }
            if (modKey == 10)
            {
                modName.text = "Large";
                ma4.ActivateLarge();
            }
            if (modKey == 11)
            {
                modName.text = "Tracking";
                ma4.ActivateTracking();
            }
            if (modKey == 12)
            {
                modName.text = "Tunnel Vision";
                ma4.ActivateTunnelVision();
            }

            Player4Panels[modNum].SetActive(true);

        }
    }

}
