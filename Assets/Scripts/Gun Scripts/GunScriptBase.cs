using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GunScriptBase : MonoBehaviour
{
	private PlayerController player;

    private InfoTracker myInfo;
    
    public int mag;
	public int magSize = 100;
    public int dmg;
    public float fireRate;
	private Camera fpsCam;                                                // Holds a reference to the first person camera
	private float nextFire;                                                // Float to store the time the player will be allowed to fire again, after 
    private ModApplication modApp;
    public ParticleSystem bloodBurst;
    private GameObject overlapObj;
    private Rewired.Player rewiredPlayer;
    private float shake = 0.1f;
    private bool isLocked;

    public GunScriptBase(PlayerController player)
	{
        rewiredPlayer = Rewired.ReInput.players.GetPlayer(player.playerID - 1);
        this.player = player;
        myInfo = GameObject.Find("Player" + player.playerID.ToString()).GetComponent<InfoTracker>(); // get info
        modApp = player.modApp;
		fpsCam = player.GetComponentInParent<Camera>(); // which cam are we
		mag = magSize; // mag size 
        bloodBurst = player.blood;
        overlapObj = player.overlapCheckObj;
	}

	public void Update()
	{
        isLocked = player.weaponLocked;

        int x = modApp.ScatterCheck();

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
                magSize = 20;
                myInfo.magSize = magSize;
                fireRate = 0.25f;
                dmg = 7;
                if (x > 1)
                {
                    dmg = 3;
                }
                shake = 0.075f;
                //player.blasterCrosshairs.SetActive(true);
            }
        }
        else
        {
            player.blaster.SetActive(false);
            //player.blasterCrosshairs.SetActive(false);
        }

        // grenade launcher
        if (player.activeWeapon == PlayerController.Weapons.Grenade)
        {
            if (player.grenadeLauncher.activeSelf == false)
            {
                player.grenadeLauncher.SetActive(true);
                magSize = 10;
                myInfo.magSize = magSize;
                fireRate = 0.5f;
                dmg = 35;
                if (x > 1)
                {
                    dmg = 15;
                }
                shake = 0.1f;
                //player.grenadeCrosshairs.SetActive(true);
            }
        }
        else
        {
            player.grenadeLauncher.SetActive(false);
            //player.grenadeCrosshairs.SetActive(false);
        }

        // machine
        if (player.activeWeapon == PlayerController.Weapons.Machine)
        {
            if (player.machineGun.activeSelf == false)
            {
                player.machineGun.SetActive(true);
                magSize = 35;
                myInfo.magSize = magSize;
                fireRate = 0.15f;
                dmg = 8;
                if (x > 1)
                {
                    dmg = 3;
                }
                shake = 0.08f;
                //player.machineCrosshairs.SetActive(true);
            }
        }
        else
        {
            player.machineGun.SetActive(false);
            //player.machineCrosshairs.SetActive(false);
        }

        // missile
        if (player.activeWeapon == PlayerController.Weapons.Missile)
        {
            if (player.missileLauncher.activeSelf == false)
            {
                player.missileLauncher.SetActive(true);
                magSize = 5;
                myInfo.magSize = magSize;
                fireRate = 1f;
                dmg = 30;
                if (x > 1)
                {
                    dmg = 12;
                }
                shake = 0.15f;
                //player.missileCrosshairs.SetActive(true);
            }
        }
        else
        {
            player.missileLauncher.SetActive(false);
            //player.missileCrosshairs.SetActive(false);
        }

        // shotgun
        if (player.activeWeapon == PlayerController.Weapons.Shotgun)
        {
            if (player.shotgunGun.activeSelf == false)
            {
                player.shotgunGun.SetActive(true);
                magSize = 5;
                myInfo.magSize = magSize;
                fireRate = 1f;
                dmg = 5;
                if (x > 1)
                {
                    dmg = 2;
                }
                shake = 0.175f;
                //player.shotgunCrosshairs.SetActive(true);
            }
        }
        else
        {
            player.shotgunGun.SetActive(false);
            //player.shotgunCrosshairs.SetActive(false);
        }

        // sniper
        if (player.activeWeapon == PlayerController.Weapons.Sniper)
        {
            if (player.sniperRifle.activeSelf == false)
            {
                player.sniperRifle.SetActive(true);
                magSize = 5;
                myInfo.magSize = magSize;
                fireRate = 1f;
                dmg = 40;
                if(x > 1)
                {
                    dmg = 15;
                }
                shake = 0.1f;
                //player.sniperCrosshairs.SetActive(true);
            }
        }
        else
        {
            player.sniperRifle.SetActive(false);
            //player.sniperCrosshairs.SetActive(false);
        }

        // Check if the player has pressed the fire button and if enough time has elapsed since they last fired
        if ((rewiredPlayer.GetAxis("FireTrigger") > 0.1f || Input.GetKeyDown(KeyCode.Mouse0)) && Time.time > nextFire && !isLocked)
		{

        #region
        
			if (mag > 0)
			{
				mag--;
                //myInfo.updateAmmo(mag);

				// Update the time when our player can fire next
				nextFire = Time.time + fireRate;

				player.weaponAnim.Play("fireR");
                fpsCam.GetComponent<StressReceiver>().InduceStress(shake);

                // blaster
                if (player.activeWeapon == PlayerController.Weapons.Blaster)
                {
                    Instantiate(player.blasterMuzzle, player.blasterEnd.position, player.transform.localRotation);
                    GameObject overlapper = Instantiate(overlapObj, player.blasterEnd.position, Quaternion.identity);
                    overlapper.transform.localEulerAngles += player.transform.localEulerAngles;
                    OverlapChecker ovc = overlapper.GetComponent<OverlapChecker>();
                    ovc.damage = dmg;
                    ovc.myID = player.playerID;
                    for (int i = 0; i < x; i++)
                    {
                        shootProjectile(player.blasterShotRotAdd + ((x - 1) * 3), player.blasterEnd, player.blasterProjectile, player.blasterShotSpeed);
                    }
                }

                // grenade launcher
                if (player.activeWeapon == PlayerController.Weapons.Grenade)
                {
                    Instantiate(player.grenadeMuzzle, player.grenadeLauncherEnd.position, player.transform.localRotation);
                    GameObject overlapper = Instantiate(overlapObj, player.grenadeLauncherEnd.position, Quaternion.identity);
                    overlapper.transform.localEulerAngles += player.transform.localEulerAngles;
                    OverlapChecker ovc = overlapper.GetComponent<OverlapChecker>();
                    ovc.damage = dmg;
                    ovc.myID = player.playerID;
                    for (int i = 0; i < x; i++)
                    {
                        shootProjectile(player.grenadeShotRotAdd + ((x - 1) * 3), player.grenadeLauncherEnd, player.grenadeProjectile, player.grenadeShotSpeed);
                    }   
                }

                // machine gun
                if (player.activeWeapon == PlayerController.Weapons.Machine)
                {
                    Instantiate(player.machineMuzzle, player.machineGunEnd.position, player.transform.localRotation);
                    GameObject overlapper = Instantiate(overlapObj, player.machineGunEnd.position, Quaternion.identity);
                    overlapper.transform.localEulerAngles += player.transform.localEulerAngles;
                    OverlapChecker ovc = overlapper.GetComponent<OverlapChecker>();
                    ovc.damage = dmg;
                    ovc.myID = player.playerID;
                    for (int i = 0; i < x; i++)
                    {
                        shootProjectile(player.machineShotRotAdd + ((x - 1) * 3), player.machineGunEnd, player.machineProjectile, player.machineShotSpeed);
                    }  
                }

                // missile launcher
                if (player.activeWeapon == PlayerController.Weapons.Missile)
                {
                    Instantiate(player.missileMuzzle, player.missileLauncherEnd.position, player.transform.localRotation);
                    GameObject overlapper = Instantiate(overlapObj, player.missileLauncherEnd.position, Quaternion.identity);
                    overlapper.transform.localEulerAngles += player.transform.localEulerAngles;
                    OverlapChecker ovc = overlapper.GetComponent<OverlapChecker>();
                    ovc.damage = dmg;
                    ovc.myID = player.playerID;
                    for (int i = 0; i < x; i++)
                    {
                        shootProjectile(player.missileShotRotAdd + ((x - 1) * 3), player.missileLauncherEnd, player.missileProjectile, player.missileShotSpeed);
                    }
                }

                // shotgun 
                if (player.activeWeapon == PlayerController.Weapons.Shotgun)
                {
                    Instantiate(player.shotgunMuzzle, player.shotgunEnd.position, player.transform.localRotation);
                    GameObject overlapper = Instantiate(overlapObj, player.shotgunEnd.position, Quaternion.identity);
                    overlapper.transform.localEulerAngles += player.transform.localEulerAngles;
                    OverlapChecker ovc = overlapper.GetComponent<OverlapChecker>();
                    ovc.damage = dmg * 6;
                    ovc.myID = player.playerID;
                    for (int i = 0; i < x; i++)
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            shootProjectile(player.shotgunShotRotAdd + ((x - 1) * 3), player.shotgunEnd, player.shotgunProjectile, player.shotgunShotSpeed);
                        }
                    } 
                }

                // sniper
                if (player.activeWeapon == PlayerController.Weapons.Sniper)
                {
                    Instantiate(player.sniperMuzzle, player.sniperRifleEnd.position, player.transform.localRotation);
                    GameObject overlapper = Instantiate(overlapObj, player.sniperRifleEnd.position, Quaternion.identity);
                    overlapper.transform.localEulerAngles += player.transform.localEulerAngles;
                    OverlapChecker ovc = overlapper.GetComponent<OverlapChecker>();
                    ovc.damage = dmg;
                    ovc.myID = player.playerID;
                    for (int i = 0; i < x; i++)
                    {
                        shootProjectile(player.sniperShotRotAdd + ((x - 1) * 3), player.sniperRifleEnd, player.sniperProjectile, player.sniperShotSpeed);
                    }
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

        if ((rewiredPlayer.GetButton("Reload") || Input.GetKeyDown(KeyCode.R)) && mag < magSize)
		{
			mag = 0;
			player.StartCoroutine(Reload());
		}

        if(rewiredPlayer.GetButton("LeftBumper") && myInfo.rm.isOver)
        {
            if(myInfo.rm.players == 2)
            {
                WhichScene.SceneToLoad = "GameplayBase";
                SceneManager.LoadScene("LoadingScene");
            }
            else if (myInfo.rm.players == 4)
            {
                WhichScene.SceneToLoad = "GameplayBase4p";
                SceneManager.LoadScene("LoadingScene");
            }
        }

        if (rewiredPlayer.GetButton("RightBumper") && myInfo.rm.isOver)
        {
            WhichScene.SceneToLoad = "NewMainMenu";
            SceneManager.LoadScene("LoadingScene");
        }

    }

    void shootProjectile(float randomShotRot, Transform gunEnd, GameObject shotProjectile, float shotSpeed)
    {
        GameObject projectile = Instantiate(shotProjectile, gunEnd.position, player.gameObject.transform.rotation); //Spawns the selected projectile
        ProjectileScript ps = projectile.GetComponent<ProjectileScript>();
        ps.dmg = dmg; // set our damage properly
        ps.burst = bloodBurst;
        ps.modApp = player.modApp; // set this to utilize vampirism
        ps.myID = myInfo.id;
        float myX = Random.Range(-randomShotRot, randomShotRot);
        float myY = Random.Range(-randomShotRot, randomShotRot);
        float myZ = Random.Range(-randomShotRot, randomShotRot);
        projectile.transform.Rotate(myX, myY, myZ);
        if(modApp.supercharge)
        {
            shotSpeed *= 1.5f;
        }
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * shotSpeed); //Set the speed of the projectile by applying force to the rigidbody
    }


        private IEnumerator Reload()
	{
        //mag = 0;
        myInfo.reloadPrompt.SetActive(false);
		player.weaponAnim.Play("loadR");
		player.SetState(PlayerState.reloadState);
		yield return new WaitForSeconds(1.01f);
        myInfo.reloadPrompt.SetActive(true);
        mag = magSize;
        //myInfo.updateAmmo(mag);
        player.SetState(PlayerState.normalState);
	}
}
