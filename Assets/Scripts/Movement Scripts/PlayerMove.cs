using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerMoveBase
{
	public PlayerMove(PlayerController player) : base(player) { }

	/*protected override float CheckAxisX()
	{
		return Input.GetAxis("Joy1Axis1");
	}

	protected override float CheckAxisZ()
	{
		return Input.GetAxis("Joy1Axis2");
	}*/
}
