using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelekinesisCollider : MonoBehaviour
{
    public GameObject obj;

    void Reset()
    {
        gameObject.layer = LayerMask.NameToLayer("Telekinesis");
    }

    public ITelekinesisObject GetTelekinesisObject()
    {
        if (obj != null)
            return obj.GetComponent<ITelekinesisObject>();
        else
        {
            return GetComponent<ITelekinesisObject>();
        }
    }
}
