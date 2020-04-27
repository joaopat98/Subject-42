using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelekinesisCollider : MonoBehaviour
{
    public GameObject obj;

    public ITelekinesisObject GetTelekinesisObject()
    {
        return obj.GetComponent<ITelekinesisObject>();
    }
}
