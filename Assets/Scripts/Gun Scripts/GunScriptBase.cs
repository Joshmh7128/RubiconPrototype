using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScriptBase
{
	private PlayerController player;
	
	private int mag;
	private int magSize = 24;
	private Camera fpsCam;                                                // Holds a reference to the first person camera
	private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);    // WaitForSeconds object used by our ShotEffect coroutine, determines time laser line will remain visible
	private LineRenderer laserLine;                                        // Reference to the LineRenderer component which will display our laserline
	private float nextFire;                                                // Float to store the time the player will be allowed to fire again, after firing

	public GunScriptBase(PlayerController player)
	{
		this.player = player;
		// Get and store a reference to our LineRenderer component
		laserLine = player.GetComponent<LineRenderer>();
		// Get and store a reference to our Camera by searching this GameObject and its parents
		fpsCam = player.GetComponentInParent<Camera>();
		mag = magSize;
		player.ammoBar.transform.localScale = new Vector3(1, 1, 1);
		player.flashLight.SetActive(false);
	}


	public void Update()
	{

		// Check if the player has pressed the fire button and if enough time has elapsed since they last fired
		if (Input.GetAxis("Joy" + player.playerID + "Axis10") > 0.1f && Time.time > nextFire)
		{
			//nextFire = Time.time + fireRate;

			if (mag > 0)
			{
				mag--;
				player.ammoBar.transform.localScale = new Vector3((float)mag / magSize, 1, 1);

				// Update the time when our player can fire next
				nextFire = Time.time + player.fireRate;

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
				if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, player.weaponRange))
				{
					// Set the end position for our laser line 
					laserLine.SetPosition(1, hit.point);

					if (hit.collider.gameObject.CompareTag("Arena"))
					{
						player.InstantiateBurst(hit.point);
					}
					if (hit.collider.gameObject.CompareTag("Player"))
					{
						player.InstantiateBlood(hit.point);
						hit.collider.gameObject.GetComponent<HealthTracker>().TakeDamage(1);
					}
				}
				else
				{
					// If we did not hit anything, set the end of the line to a position directly in front of the camera at the distance of weaponRange
					laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * player.weaponRange));
				}
			}
			else
			{
				player.StartCoroutine(Reload());
			}
		}

		if (Input.GetButton("Player" + player.playerID + "Reload") && mag < 24)
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

		//Wait for .07 seconds
		yield return shotDuration;

		// Deactivate our line renderer after waiting
		laserLine.enabled = false;
		player.flashLight.SetActive(false);
	}

	private IEnumerator Reload()
	{
		mag = 0;
		player.ammoAnim.Play("ammoBump");
		player.weaponAnim.Play("loadR");
		yield return new WaitForSeconds(1.01f);
		mag = magSize;
		player.ammoBar.transform.localScale = new Vector3(1, 1, 1);
	}
}
