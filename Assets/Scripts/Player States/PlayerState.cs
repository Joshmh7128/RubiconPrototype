using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
	public static NormalState normalState = new NormalState();
	public static ReloadState reloadState = new ReloadState();

	public abstract void EnterState(PlayerController player);
	public abstract void LeaveState(PlayerController player);
	public abstract void Update(PlayerController player);
	public abstract void FixedUpdate(PlayerController player);
	public abstract void LateUpdate(PlayerController player);
}
