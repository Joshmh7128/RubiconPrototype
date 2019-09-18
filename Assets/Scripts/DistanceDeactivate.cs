using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceDeactivate : MonoBehaviour
{
    Transform playerOne;
    Transform playerTwo;
    bool isEnabled;
    float renderDistance = 1000;
    // Start is called before the first frame update
    void Start()
    {
       playerOne = GameObject.Find("Player1").GetComponent<Transform>();
       playerTwo = GameObject.Find("Player2").GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(playerOne.position, gameObject.transform.position) > renderDistance)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
