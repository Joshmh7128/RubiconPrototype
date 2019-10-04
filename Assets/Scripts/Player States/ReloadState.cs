using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadState : PlayerState
{
	public override void EnterState(PlayerController player)
	{
		throw new System.NotImplementedException();
	}

	public override void LeaveState(PlayerController player)
	{
		throw new System.NotImplementedException();
	}

	public override void Update(PlayerController player)
	{
		//player._cameraController.Update();
		//player._playerMover.Update();
	}

	public override void FixedUpdate(PlayerController player)
	{
		player._cameraController.FixedUpdate();
		player._playerMover.FixedUpdate();
	}

	public override void LateUpdate(PlayerController player)
	{
		player._cameraController.LateUpdate();
		//player._playerMover.LateUpdate();
	}
}
