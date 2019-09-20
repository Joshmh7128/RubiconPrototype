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

    public bool invis; // makes the player invisible
    public Material invisPlayer; // set this manually

    public bool large; // modify the player's scale
    public int newScale; // how large is the player now?

    public bool speedUp; // modify the player's speed
    public int speedAdd; // how much speed are we adding?

    public bool glowing; // makes the player glow
    public Material glowPlayer; // set this per player manually (different colored players = different emit materials)

    public bool trackingParticles; // spawn particle objects
    public GameObject trackingParticleBurst; // the actual particle burst

    public bool cinematicMode; // enable / disable an overlay attached to each of the player's uh maps

    public bool hpRegen; // set an HP regen overtime based on the HP being less than 100
    public float hpRegenAmount = 0.01f; // how much regen per frame?

    public bool vampirism; // if the player lands a shot, reference back to this script and give some hp back to them
    public int vampAmount = 5; // how much hp per hit?

    // all mods in need of weapon or bullet manipulation

    public bool ricochetAmmo; // setup ammo that flings around
    public bool explosiveAmmo; // boom ammo :D
    #endregion

    // which player are we working with?
    [Header ("Set Manually Per Player")]
    public GameObject targetPlayer; // set manually
    public Renderer targetPlayerRend; // set manually
    public PlayerController playerController; // set manually
    public InfoTracker playerInfoTracker; // set manually
    public GameObject targetPlayerCineRend; // set manually

    // start
    void Start()
    {
        if (cinematicMode == false)
        {
            targetPlayerCineRend.SetActive(false);
        }
    }

    // FixedUpdate is called once per frame
    void FixedUpdate()
    {
        // check every frame to see what we've applied

        // invisible
        if (invis)
        {
            // set the proper material to the player's body
            targetPlayerRend.material = invisPlayer;
        }

        // absolute unit
        if (large)
        {
            // set the player's transform to that of the newScale
            targetPlayer.transform.localScale = new Vector3(newScale, newScale, newScale); // double size
        }

        // speed up
        if (speedUp)
        {
            // set the player's movement speed here
            playerController.speed = 20; // base speed is 10 so we're doubling it here
        }

        // glowing
        if (glowing)
        {
            // change the player's material here
            targetPlayerRend.material = glowPlayer;
        }

        // tracking particles
        if (trackingParticles)
        {
            // spawn particle objects one unit behind the player to ensure it doesn't pollute their view
            Instantiate(trackingParticleBurst, targetPlayer.transform); // could parent to another object floating behind / out of view of the player, but this method works fine and looks fine. 
            // spawning at position makes it pretty awkward. 
        }

        // cinematic mode
        if (cinematicMode)
        {
            // change the UI
            targetPlayerCineRend.SetActive(true);
        }

        // HP regen over time
        if (hpRegen)
        {
            // maybe add a function within the player script itself to handle the HP regeneration overtime?
            if (playerInfoTracker.hp < playerInfoTracker.maxHP)
            {
                playerInfoTracker.AddHealth(hpRegenAmount);
            }
        }

        if (ricochetAmmo)
        {
            // change the ammo type
        }

        if (explosiveAmmo)
        {
            // change the ammo type
        }

    }
    
    // vampirism
    public void VampCheck()
    {
        // add the HP if vamp == true
        if (vampirism)
        {
            playerInfoTracker.AddHealth(vampAmount);
        }
    }
}
