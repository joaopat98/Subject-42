using System.Collections.Generic;
using UnityEngine;

public static class TransformChildrenExtension
{
    public static List<Transform> GetChildren(this Transform t)
    {
        List<Transform> children = new List<Transform>();
        foreach (Transform child in t)
        {
            children.Add(child);
        }
        return children;
    }
}