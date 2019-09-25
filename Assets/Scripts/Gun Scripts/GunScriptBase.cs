using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScriptBase : MonoBehaviour
{
	private PlayerController player;

    private InfoTracker myInfo;
    private ModApplication modApp;

    public string type = "blaster";
    public int mag;
	public int magSize = 16;
    public int dmg = 17;
    public float fireRate = 0.2f;
    public float weaponRange = 100f;
	private Camera fpsCam;                                                // Holds a reference to the first person camera
	public WaitForSeconds shotDuration;    // WaitForSeconds object used by our ShotEffect coroutine, determines time laser line will remain visible
    public float laserTime = 0.07f;
	private LineRenderer laserLine;                                        // Reference to the LineRenderer component which will display our laserline
	private float nextFire;                                                // Float to store the time the player will be allowed to fire again, after 

    public GunScriptBase(PlayerController player)
	{
		this.player = player;
        myInfo = GameObject.Find("Player" + player.playerID.ToString()).GetComponent<InfoTracker>(); // get info
        modApp = GameObject.Find("Player" + player.playerID.ToString()).GetComponent<ModApplication>(); // get mod application
        laserLine = player.GetComponent<LineRenderer>(); // get our lazer
		fpsCam = player.GetComponentInParent<Camera>(); // which cam are we
		mag = magSize; // mag size 
		player.flashLight.SetActive(false); // do we have our flashlight on?
        shotDuration = new WaitForSeconds(laserTime); // shot timing

	}


	public void Update()
	{
        // make sure we never exceed our mag size
        if (mag > magSize)
        {
            mag = magSize;
        }

        // set our weapons to active or inactive
        // blaster
        if (player.activeWeapon == PlayerController.Weapons.Blaster)
        {
            if (player.blaster.activeSelf == false)
            {
                player.blaster.SetActive(true);
                magSize = 10;
                fireRate = 0.2f;
                dmg = 7;
            }
        }
        else
        { player.blaster.SetActive(false); }

        // grenade launcher
        if (player.activeWeapon == PlayerController.Weapons.Grenade)
        {
            if (player.grenadeLauncher.activeSelf == false)
            {
                player.grenadeLauncher.SetActive(true);
                magSize = 6;
                fireRate = 0.5f;
                dmg = 20;
            }
        }
        else
        { player.grenadeLauncher.SetActive(false); }

        // machine
        if (player.activeWeapon == PlayerController.Weapons.Machine)
        {
            if (player.machineGun.activeSelf == false)
            {
                player.machineGun.SetActive(true);
                magSize = 20;
                fireRate = 0.1f;
                dmg = 10;
            }
        }
        else
        { player.machineGun.SetActive(false); }

        // missile
        if (player.activeWeapon == PlayerController.Weapons.Missile)
        {
            if (player.missileLauncher.activeSelf == false)
            {
                player.missileLauncher.SetActive(true);
                magSize = 5;
                fireRate = 1f;
                dmg = 30;
            }
        }
        else
        { player.missileLauncher.SetActive(false); }

        // shotgun
        if (player.activeWeapon == PlayerController.Weapons.Shotgun)
        {
            if (player.shotgunGun.activeSelf == false)
            {
                player.shotgunGun.SetActive(true);
                magSize = 5;
                fireRate = 1f;
                dmg = 30;
            }
        }
        else
        { player.shotgunGun.SetActive(false); }

        // sniper
        if (player.activeWeapon == PlayerController.Weapons.Sniper)
        {
            if (player.sniperRifle.activeSelf == false)
            {
                player.sniperRifle.SetActive(true);
                magSize = 1;
                fireRate = 1f;
                dmg = 40;
            }
        }
        else
        { player.sniperRifle.SetActive(false); }

        // Check if the player has pressed the fire button and if enough time has elapsed since they last fired
        if ((Input.GetAxis("Joy" + player.playerID + "Axis10") > 0.1f || Input.GetMouseButtonDown(0)) && Time.time > nextFire)
		{

        #region
        
			if (mag > 0)
			{
				mag--;
                myInfo.updateAmmo(mag);

				// Update the time when our player can fire next
				nextFire = Time.time + fireRate;

				player.weaponAnim.Play("fireR");

                // blaster
                if (player.activeWeapon == PlayerController.Weapons.Blaster)
                {
                    GameObject projectile = Instantiate(player.blasterProjectile, player.blasterEnd.position, player.transform.rotation) as GameObject; //Spawns the selected projectile
                    projectile.GetComponent<ProjectileScript>().dmg = dmg; // set our damage properly
                    projectile.GetComponent<ProjectileScript>().modApp = modApp; // set this to utilize vampirism
                    projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * player.blasterShotSpeed); //Set the speed of the projectile by applying force to the rigidbody
                }

                // grenade launcher
                if (player.activeWeapon == PlayerController.Weapons.Grenade)
                {
                    GameObject projectile = Instantiate(player.grenadeProjectile, player.grenadeLauncherEnd.position, player.transform.rotation) as GameObject; //Spawns the selected projectile
                    projectile.GetComponent<ProjectileScript>().dmg = dmg; // set our damage properly
                    projectile.GetComponent<ProjectileScript>().modApp = modApp; // set this to utilize vampirism
                    projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * player.grenadeShotSpeed); //Set the speed of the projectile by applying force to the rigidbody
                }

                // machine gun
                if (player.activeWeapon == PlayerController.Weapons.Machine)
                {
                    GameObject projectile = Instantiate(player.machineProjectile, player.machineGunEnd.position, player.transform.rotation) as GameObject; //Spawns the selected projectile
                    projectile.GetComponent<ProjectileScript>().dmg = dmg; // set our damage properly
                    projectile.GetComponent<ProjectileScript>().modApp = modApp; // set this to utilize vampirism
                    projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * player.machineShotSpeed); //Set the speed of the projectile by applying force to the rigidbody
                }

                // missile launcher
                if (player.activeWeapon == PlayerController.Weapons.Missile)
                {
                    GameObject projectile = Instantiate(player.missileProjectile, player.missileLauncherEnd.position, player.transform.rotation) as GameObject; //Spawns the selected projectile
                    projectile.GetComponent<ProjectileScript>().dmg = dmg; // set our damage properly
                    projectile.GetComponent<ProjectileScript>().modApp = modApp; // set this to utilize vampirism
                    projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * player.missileShotSpeed); //Set the speed of the projectile by applying force to the rigidbody
                }

                // shotgun 
                if (player.activeWeapon == (PlayerController.Weapons)4)
                {
                    GameObject projectile = Instantiate(player.shotgunProjectile, player.shotgunEnd.position, player.transform.rotation) as GameObject; //Spawns the selected projectile
                    projectile.GetComponent<ProjectileScript>().dmg = dmg; // set our damage properly
                    projectile.GetComponent<ProjectileScript>().modApp = modApp; // set this to utilize vampirism
                    projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * player.otherShotSpeed); //Set the speed of the projectile by applying force to the rigidbody
                }                
                // sniper
                if (player.activeWeapon == PlayerController.Weapons.Sniper)
                {
                    GameObject projectile = Instantiate(player.sniperProjectile, player.sniperRifleEnd.position, player.transform.rotation) as GameObject; //Spawns the selected projectile
                    projectile.GetComponent<ProjectileScript>().dmg = dmg; // set our damage properly
                    projectile.GetComponent<ProjectileScript>().modApp = modApp; // set this to utilize vampirism
                    projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * player.sniperShotSpeed); //Set the speed of the projectile by applying force to the rigidbody
                }
            }

            #region
            //player.muzzle.Play();

            // Start our ShotEffect coroutine to turn our laser line on and off
            //player.StartCoroutine(ShotEffect());

            // Create a vector at the center of our camera's viewport
            //Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            // Declare a raycast hit to store information about what our raycast has hit
            //RaycastHit hit;

            // Set the start position for our visual effect for our laser to the position of gunEnd
            //laserLine.SetPosition(0, player.gunEnd.position);

            // Check if our raycast has hit anything
            /*
			if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
			{
				// Set the end position for our laser line 
				laserLine.SetPosition(1, hit.point);

				if (hit.collider.gameObject.CompareTag("Player"))
				{
					player.InstantiateBlood(hit.point);
                    hit.collider.gameObject.GetComponent<InfoTracker>().TakeDamage(dmg);
                    modApp.VampCheck();
                }
                else if (hit.collider.gameObject.CompareTag("Breakable"))
                {
                    hit.collider.gameObject.GetComponent<BreakableObject>().TakeDamage();
                    player.InstantiateBurst(hit.point);
                }
                else
                {
                    player.InstantiateBurst(hit.point);
                }
			}
			else
			{
				// If we did not hit anything, set the end of the line to a position directly in front of the camera at the distance of weaponRange
				laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
			}
		}
		else
		{
			player.StartCoroutine(Reload());
		}*/

            #endregion
        }

        #endregion

        if (Input.GetButton("Player" + player.playerID + "Reload") && mag < magSize)
		{
			mag = 0;
			player.StartCoroutine(Reload());
		}
	}

	private IEnumerator Reload()
	{
		//mag = 0;
		player.weaponAnim.Play("loadR");
		player.SetState(PlayerState.reloadState);
		yield return new WaitForSeconds(1.01f);
		mag = magSize;
        myInfo.updateAmmo(mag);
        player.SetState(PlayerState.normalState);
	}
}
