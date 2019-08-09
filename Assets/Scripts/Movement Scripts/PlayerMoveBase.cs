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
		inputVector = new Vector3(Input.GetAxis("Joy" + player.playerID + "Axis1"), 0, -Input.GetAxis("Joy" + player.playerID + "Axis2"));
		inputVector = player.transform.TransformDirection(inputVector);
	}

	public void FixedUpdate()
	{
		playerBody.velocity = inputVector * (player.speed * 50) * Time.deltaTime;
	}
}
