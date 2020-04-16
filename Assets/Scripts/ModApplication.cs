using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModApplication : MonoBehaviour
{
    /*
     * this script will be used to: 
     * assign and activate all mods for the player(s) to be modified by
    */

    #region  all mods

    [Header("Active Mods")]

    public bool armor = false;
    public bool glowing = false;
    public bool large = false;
    public bool regen = false;
    public bool scatter = false;
    public bool shield = false;
    public bool speed = false;
    public bool stealth = false;
    public bool supercharge = false;
    public bool tracking = false;
    public bool tunnelVision = false;
    public bool vampirism = false;

    [Header("Mod Variables")]

    public Material invisPlayer; // set this manually
    private int newScale = 3; // how large is the player now?
    private int speedAdd = 5; // how much speed are we adding?
    public float hpRegenAmount = 1f; // how much regen per update?
    private float hpRegenDelay = 2f; // how long to wait between regen increments
    private float vampAmount = 0.25f; // how much hp per hit?
    private int armorAmount = 35;

    #endregion

    // which player are we working with?
    [Header ("Set Manually Per Player")]
    public GameObject targetPlayer; // set manually
    public Material defaultMat;
    public GameObject glowObj;
    public GameObject targetPlayerShield;
    public Renderer targetPlayerRend; // set manually
    public PlayerController playerController; // set manually
    public InfoTracker playerInfoTracker; // set manually
    public GameObject targetPlayerCineRend; // set manually
    public GameObject trackingLine; // the player's line renderer

    // start
    void Start()
    {

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            ActivateGlow();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ActivateScatter();
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            ActivateShield();
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            ActivateStealth();
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            ActivateSpeed();
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ResetMods();
        }
    }

    public void ResetMods()
    {
        armor = false;
        glowing = false;
        large = false;
        regen = false;
        scatter = false;
        shield = false;
        stealth = false;
        supercharge = false;
        tracking = false;
        tunnelVision = false;
        vampirism = false;

        trackingLine.SetActive(false);
        glowObj.SetActive(false);
        targetPlayerShield.SetActive(false);
        targetPlayer.transform.localScale = new Vector3(2, 2, 2);
        targetPlayer.GetComponent<InfoTracker>().maxArmor = 0;
        playerController.speed = 10;
        targetPlayerCineRend.SetActive(false);
        targetPlayerRend.material = defaultMat;
    }

    public void ActivateTracking()
    {
        tracking = true;
        trackingLine.SetActive(true);
        if(playerInfoTracker.rm.players == 2)
        {
            playerInfoTracker.rm.sm.PlaySound("trackingMod");
        }
        
    }

    public void ActivateLarge()
    {
        large = true;
        targetPlayer.transform.localScale = new Vector3(newScale, newScale, newScale); // double size
        if (playerInfoTracker.rm.players == 2)
        {
            playerInfoTracker.rm.sm.PlaySound("largeMod");
        }
    }

    public void ActivateSpeed()
    {
        speed = true;
        playerController.speed += speedAdd; // base speed is 10
        if (playerInfoTracker.rm.players == 2)
        {
            playerInfoTracker.rm.sm.PlaySound("speedMod");
        }
    }

    public void ActivateGlow()
    {
        glowing = true;
        glowObj.SetActive(true);
        if (playerInfoTracker.rm.players == 2)
        {
            playerInfoTracker.rm.sm.PlaySound("glowingMod");
        }
    }

    public void ActivateTunnelVision()
    {
        tunnelVision = true;
        targetPlayerCineRend.SetActive(true);
        if (playerInfoTracker.rm.players == 2)
        {
            playerInfoTracker.rm.sm.PlaySound("cineMod");
        }
    }

    public void ActivateSupercharge()
    {
        supercharge = true;
        if (playerInfoTracker.rm.players == 2)
        {
            playerInfoTracker.rm.sm.PlaySound("superchargeMod");
        }
    }

    public void ActivateScatter()
    {
        scatter = true;
        if (playerInfoTracker.rm.players == 2)
        {
            playerInfoTracker.rm.sm.PlaySound("scatterMod");
        }
    }

    public void ActivateShield()
    {
        armor = true;
        targetPlayerShield.SetActive(true);
        if (playerInfoTracker.rm.players == 2)
        {
            playerInfoTracker.rm.sm.PlaySound("shieldMod");
        }
    }

    public void ActivateArmor()
    {
        targetPlayer.GetComponent<InfoTracker>().maxArmor = armorAmount;
        targetPlayer.GetComponent<InfoTracker>().AddArmor(armorAmount);
        shield = true;
        if (playerInfoTracker.rm.players == 2)
        {
            playerInfoTracker.rm.sm.PlaySound("armorMod");
        }
    }

    public void ActivateVampirism()
    {
        vampirism = true;
        if (playerInfoTracker.rm.players == 2)
        {
            playerInfoTracker.rm.sm.PlaySound("vampMod");
        }
    }

    public void ActivateRegen()
    {
        regen = true;
        StartCoroutine(Regenerate());
        if (playerInfoTracker.rm.players == 2)
        {
            playerInfoTracker.rm.sm.PlaySound("hpRegenMod");
        }
    }

    public void ActivateStealth()
    {
        stealth = true;
        targetPlayerRend.material = invisPlayer;
        if (playerInfoTracker.rm.players == 2)
        {
            playerInfoTracker.rm.sm.PlaySound("invisMod");
        }
    }

    private IEnumerator Regenerate()
    {
        while (regen)
        {
            playerInfoTracker.AddHealth(hpRegenAmount);
            yield return new WaitForSeconds(hpRegenDelay);
        }
    }
    
    // vampirism
    public void VampCheck(int damage)
    {
        // add the HP if vamp == true
        if (vampirism)
        {
            playerInfoTracker.AddHealth((int)damage * vampAmount);
        }
    }

    public bool ChargeCheck()
    {
        if(supercharge)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int ScatterCheck()
    {
        if(scatter)
        {
            return 3;
        }
        else
        {
            return 1;
        }
    }
}
