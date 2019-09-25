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

    public bool armor;
    public bool glowing;
    public bool large;
    public bool regen;
    public bool scatter;
    public bool shield;
    public bool speed;
    public bool stealth;
    public bool supercharge;
    public bool tracking;
    public bool tunnelVision;
    public bool vampirism;

    [Header("Mod Variables")]

    public Material invisPlayer; // set this manually
    public int newScale; // how large is the player now?
    public int speedAdd; // how much speed are we adding?
    public Material glowPlayer; // set this per player manually (different colored players = different emit materials)
    public float hpRegenAmount = 1f; // how much regen per update?
    public float hpRegenDelay = 1f; // how long to wait between regen increments
    public float vampAmount = 0.25f; // how much hp per hit?

    #endregion

    // which player are we working with?
    [Header ("Set Manually Per Player")]
    public GameObject targetPlayer; // set manually
    public Renderer targetPlayerRend; // set manually
    public PlayerController playerController; // set manually
    public InfoTracker playerInfoTracker; // set manually
    public GameObject targetPlayerCineRend; // set manually
    public GameObject trackingParticleBurst; // the player's particle burst

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

        trackingParticleBurst.SetActive(false);
        targetPlayer.transform.localScale = new Vector3(1, 1, 1);
        playerController.speed -= speedAdd;
        targetPlayerCineRend.SetActive(false);
    }

    public void ActivateTracking()
    {
        trackingParticleBurst.SetActive(true);
    }

    public void ActivateLarge()
    {
        targetPlayer.transform.localScale = new Vector3(newScale, newScale, newScale); // double size
    }

    public void ActivateSpeed()
    {
        playerController.speed += speedAdd; // base speed is 10
    }

    public void ActivateGlow()
    {

    }

    public void ActivateCinematic()
    {
        targetPlayerCineRend.SetActive(true);
    }

    public void ActivateSupercharge()
    {

    }

    public void ActivateScatter()
    {

    }

    // FixedUpdate is called once per frame
    void FixedUpdate()
    {
        // check every frame to see what we've applied

        // invisible
        if (stealth)
        {
            // set the proper material to the player's body
            targetPlayerRend.material = invisPlayer;
        }

        // HP regen over time
        if (regen)
        {
            StartCoroutine(Regenerate());
        }

    }

    private IEnumerator Regenerate()
    {
        while(regen)
        {
            yield return new WaitForSeconds(hpRegenDelay);
            playerInfoTracker.AddHealth(hpRegenAmount);
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
}
