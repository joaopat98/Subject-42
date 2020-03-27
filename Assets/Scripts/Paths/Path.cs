using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Path : MonoBehaviour
{

    public List<Vector3> GetAllNodes()
    {
        return transform.Cast<Transform>().Select(t => t.position).ToList();
    }
}
