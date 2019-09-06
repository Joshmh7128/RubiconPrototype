using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveBase
{
	private PlayerController player;
	private Rigidbody playerBody;
	private Vector3 inputVector;

	// Start is called before the first frame update
	public PlayerMoveBase(PlayerController player)
	{
		this.player = player;
		playerBody = player.playerBody;
	}

	// Update is called once per frame
	public void Update()
	{
        // basic stick based movement
		inputVector = new Vector3(Input.GetAxis("Joy" + player.playerID + "Axis1"), 0, -Input.GetAxis("Joy" + player.playerID + "Axis2"));
		//inputVector = player.transform.TransformDirection(inputVector);

        // In order to properly get the bumper buttons you MUST set them manually through the inspector, they are next to each other near the joystick 1 definitions 
        // up and down movement
        
        if (Input.GetButton("Joy" + player.playerID + "Axis5Bump"))
        {
            // move up
            inputVector += new Vector3(0, 1, 0);
            
        }

        if (Input.GetButton("Joy" + player.playerID + "Axis4Bump"))
        {
            // move down
            inputVector += new Vector3(0, -1, 0);
        }
        inputVector = player.transform.TransformDirection(inputVector);

    }

	public void FixedUpdate()
	{
		playerBody.velocity = inputVector * (player.speed * 50) * Time.deltaTime;
	}
}
