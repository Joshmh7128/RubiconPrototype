using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateTest : MonoBehaviour
{
    public int speed;
    public GameObject ourObject;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("space"))
        {
            GameObject projectile = Instantiate(ourObject, transform.position, Quaternion.identity) as GameObject; //Spawns the selected projectile
            //projectile.transform.LookAt(hit.point); //Sets the projectiles rotation to look at the point clicked
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * speed); //Set the speed of the projectile by applying force to the rigidbody
        }
    }
}
