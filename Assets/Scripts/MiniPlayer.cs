using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPlayer : MonoBehaviour
{
    [Range(0, 3)]
    public int id;
    private Rewired.Player myPlayer;
    private float rotAroundX, rotAroundY;
    private Quaternion targetRot;

    private void OnEnable()
    {
        this.gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
        myPlayer = Rewired.ReInput.players.GetPlayer(id);
        rotAroundX = this.gameObject.transform.eulerAngles.x;
        rotAroundY = this.gameObject.transform.eulerAngles.y;
    }

    private void OnDisable()
    {
        this.gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    private void FixedUpdate()
    {
        rotAroundX += Input.GetAxis("Mouse Y") * 3.5f;
        rotAroundY += Input.GetAxis("Mouse X") * 3.5f;

        rotAroundX += myPlayer.GetAxis("LookHorizontalX") * -3.5f;
        rotAroundY += myPlayer.GetAxis("LookHorizontalY") * 3.5f;
        rotAroundX = Mathf.Clamp(rotAroundX, -90, 90);
        targetRot = Quaternion.Euler(-rotAroundX, rotAroundY, 0);
        this.gameObject.transform.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, targetRot, 0.25f);
    }
}
