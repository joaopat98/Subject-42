using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractCollider : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(this.gameObject.transform.position);
        if (other.CompareTag("Guard"))
        {
            Debug.Log("Detect sound");
            Guard guard = other.gameObject.GetComponent<Guard>();
            guard.action = new GuardCheck(guard, this.gameObject.transform.position);
        }
    }
}
