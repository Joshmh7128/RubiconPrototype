using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Breakable"))
        {
            other.GetComponent<BreakableObject>().TakeDamage(25);
            Debug.Log("Hit object");
            Destroy(this.gameObject);
        }
    }
}
