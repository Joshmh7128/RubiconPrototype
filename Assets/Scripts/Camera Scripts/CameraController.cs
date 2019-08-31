using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : CameraControllerBase
{
	public CameraController(PlayerController player) : base(player) { }

	/*protected override float GetAxisX()
	{
		return Input.GetAxis("Joy1Axis5");
	}

	protected override float GetAxisY()
	{
		return Input.GetAxis("Joy1Axis4");
	}*/
}
