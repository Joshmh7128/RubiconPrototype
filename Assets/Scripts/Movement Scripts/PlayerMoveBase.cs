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
    private Rewired.Player rewiredPlayer;

    // Start is called before the first frame update
    public PlayerMoveBase(PlayerController player)
	{
        rewiredPlayer = Rewired.ReInput.players.GetPlayer(player.playerID - 1);
        this.player = player;
		playerBody = player.playerBody;
        /* // depricated
        axis1 = "Joy" + player.playerID + "Axis1";
        axis2 = "Joy" + player.playerID + "Axis2";
        axis5bump = "Joy" + player.playerID + "Axis5Bump";
        axis4bump = "Joy" + player.playerID + "Axis4Bump";
        */
    }

	// Update is called once per frame
	public void FixedUpdate()
	{
        // basic stick based movement
		inputVector = new Vector3(rewiredPlayer.GetAxis("MoveHorizontalX"), 0, rewiredPlayer.GetAxis("MoveHorizontalY"));
		//inputVector = player.transform.TransformDirection(inputVector);

        // In order to properly get the bumper buttons you MUST set them manually through the inspector, they are next to each other near the joystick 1 definitions 
        // up and down movement
        
        if (rewiredPlayer.GetButton("RightBumper") || Input.GetKey(KeyCode.E))
        {
            // move up
            inputVector.y += 1;
            
        }

        if (rewiredPlayer.GetButton("LeftBumper") || Input.GetKey(KeyCode.Q))
        {
            // move down
            inputVector.y -= 1;
        }

        if (Input.GetKey(KeyCode.W))
        {
            // for playtesting without a controller
            inputVector.z += 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            // for playtesting without a controller
            inputVector.x -= 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            // for playtesting without a controller
            inputVector.z -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            // for playtesting without a controller
            inputVector.x += 1;
        }

        inputVector = player.transform.TransformDirection(inputVector);

        playerBody.velocity = inputVector * (player.speed * 50) * Time.deltaTime;

    }
    /*
	public void FixedUpdate()
	{ // move the player
		playerBody.velocity = inputVector * (player.speed * 50) * Time.deltaTime;
	}*/
}
