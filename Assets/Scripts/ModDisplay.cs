using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModDisplay : MonoBehaviour
{
    public GameObject[] Player1Panels = new GameObject[4];
    public GameObject[] Player2Panels = new GameObject[4];

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
                modName.text = "Good Mod #1";
            }
            if (modKey == 2)
            {
                modName.text = "Good Mod #2";
            }
            if (modKey == 3)
            {
                modName.text = "Good Mod #3";
            }
            if (modKey == 4)
            {
                modName.text = "Good Mod #4";
            }
            if (modKey == 5)
            {
                modName.text = "Good Mod #5";
            }
            if (modKey == 6)
            {
                modName.text = "Good Mod #6";
            }
            if (modKey == 7)
            {
                modName.text = "Good Mod #7";
            }
            if (modKey == 8)
            {
                modName.text = "Bad Mod #8";
            }
            if (modKey == 9)
            {
                modName.text = "Bad Mod #9";
            }
            if (modKey == 10)
            {
                modName.text = "Bad Mod #10";
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
                modName.text = "Good Mod #1";
            }
            if (modKey == 2)
            {
                modName.text = "Good Mod #2";
            }
            if (modKey == 3)
            {
                modName.text = "Good Mod #3";
            }
            if (modKey == 4)
            {
                modName.text = "Good Mod #4";
            }
            if (modKey == 5)
            {
                modName.text = "Good Mod #5";
            }
            if (modKey == 6)
            {
                modName.text = "Good Mod #6";
            }
            if (modKey == 7)
            {
                modName.text = "Good Mod #7";
            }
            if (modKey == 8)
            {
                modName.text = "Bad Mod #8";
            }
            if (modKey == 9)
            {
                modName.text = "Bad Mod #9";
            }
            if (modKey == 10)
            {
                modName.text = "Bad Mod #10";
            }

            Player2Panels[modNum].SetActive(true);

        }
    }

}
