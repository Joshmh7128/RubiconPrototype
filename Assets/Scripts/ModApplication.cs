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
    public int newScale; // how large is the player now?
    public int speedAdd; // how much speed are we adding?
    public float hpRegenAmount = 1f; // how much regen per update?
    public float hpRegenDelay = 0.5f; // how long to wait between regen increments
    public float vampAmount = 0.25f; // how much hp per hit?
    public int shieldAmount = 25;

    #endregion

    // which player are we working with?
    [Header ("Set Manually Per Player")]
    public GameObject targetPlayer; // set manually
    public Material defaultMat;
    public GameObject glowObj;
    public GameObject targetPlayerArmor;
    public Renderer targetPlayerRend; // set manually
    public PlayerController playerController; // set manually
    public InfoTracker playerInfoTracker; // set manually
    public GameObject targetPlayerCineRend; // set manually
    public GameObject trackingLine; // the player's line renderer

    // start
    void Start()
    {

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
        targetPlayerArmor.SetActive(false);
        targetPlayer.transform.localScale = new Vector3(2, 2, 2);
        targetPlayer.GetComponent<InfoTracker>().maxShield = 0;
        playerController.speed = 10;
        targetPlayerCineRend.SetActive(false);
        targetPlayerRend.material = defaultMat;
    }

    public void ActivateTracking()
    {
        Debug.Log("Activating Tracking");
        tracking = true;
        trackingLine.SetActive(true);
    }

    public void ActivateLarge()
    {
        Debug.Log("Activating Large");
        large = true;
        targetPlayer.transform.localScale = new Vector3(newScale, newScale, newScale); // double size
    }

    public void ActivateSpeed()
    {
        Debug.Log("Activating Speed");
        speed = true;
        playerController.speed += speedAdd; // base speed is 10
    }

    public void ActivateGlow()
    {
        Debug.Log("Activating Glow");
        glowing = true;
        glowObj.SetActive(true);
    }

    public void ActivateTunnelVision()
    {
        Debug.Log("Activating Tunnel Vision");
        tunnelVision = true;
        targetPlayerCineRend.SetActive(true);
    }

    public void ActivateSupercharge()
    {
        Debug.Log("Activating Supercharge");
        supercharge = true;
    }

    public void ActivateScatter()
    {
        Debug.Log("Activating Scattershot");
        scatter = true;
    }

    public void ActivateShield()
    {
        Debug.Log("Activating Shield");
        targetPlayer.GetComponent<InfoTracker>().AddShield(shieldAmount);
        shield = true;
    }

    public void ActivateArmor()
    {
        Debug.Log("Activating Armor");
        armor = true;
        targetPlayerArmor.SetActive(true);
    }

    public void ActivateVampirism()
    {
        Debug.Log("Activating Vampirism");
        vampirism = true;
    }

    public void ActivateRegen()
    {
        Debug.Log("Activating Regen");
        regen = true;
        StartCoroutine(Regenerate());
    }

    public void ActivateStealth()
    {
        Debug.Log("Activating Stealth");
        stealth = true;
        targetPlayerRend.material = invisPlayer;
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
