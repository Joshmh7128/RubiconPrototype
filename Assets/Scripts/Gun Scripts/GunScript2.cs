using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript2 : GunScriptBase
{
	public GunScript2(PlayerController player) : base(player) { }

	/*protected override bool CheckFireButton()
	{
		return Input.GetAxis("Joy2Axis10") > 0.1f;
	}

	protected override bool CheckReloadButton()
	{
		return Input.GetButton("Player2Reload");
	}*/
}
