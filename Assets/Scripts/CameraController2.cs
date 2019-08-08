using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController2 : MonoBehaviour
{

    //camera public values
    public float XMinRotation;
    public float XMaxRotation;
    [Range(1.0f, 10.0f)]
    public float Xsensitivity;
    [Range(1.0f, 10.0f)]
    public float Ysensitivity;
    private Camera cam;
    private float rotAroundX, rotAroundY;
    private bool camMoved = false;

    public Transform target;                        //player object to track
    public float smoothSpeed;                       //amount of smoothing for movement
    public float slerpAmount;

    public float offset;                            //forward camera offset amount

    // Use this for initialization
    void Start()
    {
        cam = this.GetComponent<Camera>();
        rotAroundX = transform.eulerAngles.x;
        rotAroundY = transform.eulerAngles.y;
    }

    private void Update()
    {
        rotAroundX += Input.GetAxis("Mouse Y") * Xsensitivity;
        rotAroundY += Input.GetAxis("Mouse X") * Ysensitivity;

        rotAroundX += Input.GetAxis("Joy2Axis5") * -Xsensitivity;
        rotAroundY += Input.GetAxis("Joy2Axis4") * Ysensitivity;

        // Clamp rotation values
        rotAroundX = Mathf.Clamp(rotAroundX, XMinRotation, XMaxRotation);

        CameraRotation();
    }

    private void CameraRotation()
    {
        target.transform.rotation = Quaternion.Euler(-rotAroundX, rotAroundY, 0);
        cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, target.transform.rotation, slerpAmount);
    }

    private void LateUpdate()
    {
        Vector3 targetPos = target.position + (transform.forward * offset);
        Vector3 smoothPos = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
        transform.position = smoothPos;
    }


}