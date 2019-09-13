using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScriptBase
{
	private PlayerController player;

    private InfoTracker myInfo;

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
	private float nextFire;                                                // Float to store the time the player will be allowed to fire again, after firing

	public GunScriptBase(PlayerController player)
	{
		this.player = player;
        myInfo = GameObject.Find("Player" + player.playerID.ToString()).GetComponent<InfoTracker>();
		laserLine = player.GetComponent<LineRenderer>();
		fpsCam = player.GetComponentInParent<Camera>();
		mag = magSize;
		player.flashLight.SetActive(false);
        shotDuration = new WaitForSeconds(laserTime);
	}


	public void Update()
	{

		// Check if the player has pressed the fire button and if enough time has elapsed since they last fired
		if ((Input.GetAxis("Joy" + player.playerID + "Axis10") > 0.1f || Input.GetMouseButtonDown(0)) && Time.time > nextFire)
		{

			if (mag > 0)
			{
				mag--;
                myInfo.updateAmmo(mag);

				// Update the time when our player can fire next
				nextFire = Time.time + fireRate;

				player.weaponAnim.Play("fireR");
				player.muzzle.Play();

				// Start our ShotEffect coroutine to turn our laser line on and off
				player.StartCoroutine(ShotEffect());

				// Create a vector at the center of our camera's viewport
				Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

				// Declare a raycast hit to store information about what our raycast has hit
				RaycastHit hit;

				// Set the start position for our visual effect for our laser to the position of gunEnd
				laserLine.SetPosition(0, player.gunEnd.position);

				// Check if our raycast has hit anything
				if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
				{
					// Set the end position for our laser line 
					laserLine.SetPosition(1, hit.point);

					if (hit.collider.gameObject.CompareTag("Player"))
					{
						player.InstantiateBlood(hit.point);
                        hit.collider.gameObject.GetComponent<InfoTracker>().TakeDamage(dmg);
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
			}
		}

		if (Input.GetButton("Player" + player.playerID + "Reload") && mag < magSize)
		{
			mag = 0;
			player.StartCoroutine(Reload());
		}
	}

	private IEnumerator ShotEffect()
	{

		// Turn on our line renderer
		laserLine.enabled = true;
		player.flashLight.SetActive(true);

		//Wait for x seconds
		yield return shotDuration;

		// Deactivate our line renderer after waiting
		laserLine.enabled = false;
		player.flashLight.SetActive(false);
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
