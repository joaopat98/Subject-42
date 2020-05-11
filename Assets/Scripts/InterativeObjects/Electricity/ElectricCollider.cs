using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricCollider : MonoBehaviour
{
    public GameObject obj;

    public IElectricObject GetElectricObject()
    {
        if (obj != null)
            return obj.GetComponent<IElectricObject>();
        else
        {
            return GetComponent<IElectricObject>();
        }
    }
}
