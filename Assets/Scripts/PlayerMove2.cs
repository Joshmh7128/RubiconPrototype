using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove2 : MonoBehaviour
{
    private Rigidbody playerBody;

    private Vector3 inputVector;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        inputVector = new Vector3(Input.GetAxisRaw("Horizontal2"), Input.GetAxisRaw("TrueVertical2"), Input.GetAxisRaw("Vertical2"));
        inputVector = transform.TransformDirection(inputVector);
    }

    private void FixedUpdate()
    {
        playerBody.velocity = inputVector * (speed * 50) * Time.deltaTime;
    }
}
