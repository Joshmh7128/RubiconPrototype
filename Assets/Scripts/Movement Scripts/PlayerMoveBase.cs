using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveBase
{
	private PlayerController player;
	private Rigidbody playerBody;
	private Vector3 inputVector;
    private string axis1;
    private string axis2;
    private string axis5bump;
    private string axis4bump;

	// Start is called before the first frame update
	public PlayerMoveBase(PlayerController player)
	{
		this.player = player;
		playerBody = player.playerBody;
        axis1 = "Joy" + player.playerID + "Axis1";
        axis2 = "Joy" + player.playerID + "Axis2";
        axis5bump = "Joy" + player.playerID + "Axis5Bump";
        axis4bump = "Joy" + player.playerID + "Axis4Bump";

    }

	// Update is called once per frame
	public void Update()
	{
        // basic stick based movement
		inputVector = new Vector3(Input.GetAxis(axis1), 0, -Input.GetAxis(axis2));
		//inputVector = player.transform.TransformDirection(inputVector);

        // In order to properly get the bumper buttons you MUST set them manually through the inspector, they are next to each other near the joystick 1 definitions 
        // up and down movement
        
        if (Input.GetButton(axis5bump))
        {
            // move up
            //inputVector += new Vector3(0, 1, 0);
            inputVector.y += 1;
            
        }

        if (Input.GetButton(axis4bump))
        {
            // move down
            //inputVector += new Vector3(0, -1, 0);
            inputVector.y -= 1;
        }

        if (Input.GetKey(KeyCode.W))
        {
            // for playtesting without a controller
            //inputVector += new Vector3(0, 0, 1);
            inputVector.z += 1;
        }

        inputVector = player.transform.TransformDirection(inputVector);

    }

	public void FixedUpdate()
	{ // move the player
		playerBody.velocity = inputVector * (player.speed * 50) * Time.deltaTime;
	}
}
