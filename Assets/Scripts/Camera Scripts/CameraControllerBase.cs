using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerBase
{
	private PlayerController player;
	private Camera cam;
	private float rotAroundX, rotAroundY;
	private bool camMoved = false;
    private Rewired.Player rewiredPlayer;
    private int playing;

	// Use this for initialization
	public CameraControllerBase(PlayerController player)
	{
		this.player = player;
		cam = player.GetComponent<Camera>();
		rotAroundX = player.transform.eulerAngles.x;
		rotAroundY = player.transform.eulerAngles.y;
        rewiredPlayer = Rewired.ReInput.players.GetPlayer(player.playerID - 1);
	}

	public void FixedUpdate()
	{
        playing = player._weaponSystems.myInfo.rm.focusedPlayer;
        if(playing == 0 || playing == player.playerID)
        {
            rotAroundX += Input.GetAxis("Mouse Y") * player.Xsensitivity;
            rotAroundY += Input.GetAxis("Mouse X") * player.Ysensitivity;
        }

		rotAroundX += rewiredPlayer.GetAxis("LookHorizontalX") * -player.Xsensitivity;
		rotAroundY += rewiredPlayer.GetAxis("LookHorizontalY") * player.Ysensitivity;

		// Clamp rotation values
		rotAroundX = Mathf.Clamp(rotAroundX, player.XMinRotation, player.XMaxRotation);

		CameraRotation();
	}

	private void CameraRotation()
	{
		player.target.transform.rotation = Quaternion.Euler(-rotAroundX, rotAroundY, 0);
		cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, player.target.transform.rotation, player.slerpAmount);
	}

	public void LateUpdate()
	{
		Vector3 targetPos = player.target.position + (player.transform.forward * player.offset);
		Vector3 smoothPos = Vector3.Lerp(player.transform.position, targetPos, player.smoothSpeed * Time.deltaTime);
		player.transform.position = smoothPos;
	}
}
