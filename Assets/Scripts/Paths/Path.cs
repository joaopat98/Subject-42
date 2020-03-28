using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Collection of nodes that represent a path to be followed by an agent
/// </summary>
public class Path : MonoBehaviour
{

    /// <summary>
    /// Get list of positions that compose this path
    /// </summary>
    public List<Vector3> GetAllNodes()
    {
        return transform.Cast<Transform>().Select(t => t.position).ToList();
    }
}
