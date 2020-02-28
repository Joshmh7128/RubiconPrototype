using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    bool isExplosion = false; // is this an explosion of a rocket or grenade?
    float dmgMult = 1f;
    public ModApplication modApp;
    public int dmg;
    public int myID;
    public ParticleSystem burst;
    public GameObject impactParticle;
    private Rigidbody rb;

    private void Start()
    {
        if(modApp.ChargeCheck())
        {
            this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x * 3, 
                this.gameObject.transform.localScale.y * 3, this.gameObject.transform.localScale.z * 3);
            dmgMult = 1.5f;
        }
        rb = transform.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        float radius = 0.15f;

        Vector3 direction = rb.velocity; // Gets the direction of the projectile, used for collision detection
        if (rb.useGravity)
            direction += Physics.gravity * Time.deltaTime; // Accounts for gravity if enabled
        direction = direction.normalized;

        float detectionDistance = rb.velocity.magnitude * Time.deltaTime; // Distance of collision detection for this frame

        if (Physics.SphereCast(transform.position, radius, direction, out hit, detectionDistance)) // Checks if collision will happen
        {
            GameObject hitObj = hit.collider.gameObject;
            if (hitObj.CompareTag("Player"))
            {
                if (hitObj.GetComponent<InfoTracker>().id != myID)
                {
                    hitObj.GetComponent<InfoTracker>().TakeDamage((int)(dmg * dmgMult));
                    Instantiate(burst, transform.position, Quaternion.identity);
                    modApp.VampCheck((int)(dmg * dmgMult));
                }
            }
            else if (hitObj.CompareTag("Breakable"))
            {
                hitObj.GetComponent<BreakableObject>().TakeDamage((int)(dmg * dmgMult));
            }
            else if (hitObj.CompareTag("Joke"))
            {
                hitObj.GetComponent<JokeObject>().TakeDamage((int)(dmg * dmgMult));
            }

            transform.position = hit.point + (hit.normal * 0.15f); // Move projectile to point of collision

            GameObject impactP = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject; // Spawns impact effect


            ParticleSystem[] trails = GetComponentsInChildren<ParticleSystem>(); // Gets a list of particle systems, as we need to detach the trails
                                                                                 //Component at [0] is that of the parent i.e. this object (if there is any)
            for (int i = 1; i < trails.Length; i++) // Loop to cycle through found particle systems
            {
                ParticleSystem trail = trails[i];

                if (trail.gameObject.name.Contains("Trail"))
                {
                    trail.transform.SetParent(null); // Detaches the trail from the projectile
                    Destroy(trail.gameObject, 2f); // Removes the trail after seconds
                }
            }
            Destroy(impactP, 3.5f); // Removes impact effect after delay
            Destroy(gameObject); // Removes the projectile
        }
    }

    private void OnCollisionEnter(Collision hit)
    {
        if (hit.collider.gameObject.CompareTag("Player"))
        {
            if(hit.gameObject.GetComponent<InfoTracker>().id != myID)
            {
                hit.collider.gameObject.GetComponent<InfoTracker>().TakeDamage((int)(dmg * dmgMult));
                Instantiate(burst, transform.position, Quaternion.identity);
                modApp.VampCheck((int)(dmg * dmgMult));
            }
        }
        else if (hit.collider.gameObject.CompareTag("Breakable"))
        {
            hit.collider.gameObject.GetComponent<BreakableObject>().TakeDamage((int)(dmg * dmgMult));
        }
        else if (hit.collider.gameObject.CompareTag("Joke"))
        {
            hit.collider.gameObject.GetComponent<JokeObject>().TakeDamage((int)(dmg * dmgMult));
        }
    }
}
