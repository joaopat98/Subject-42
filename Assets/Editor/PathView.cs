using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Path))]
public class PathView : Editor
{
    [DrawGizmo(GizmoType.Selected)]
    static void DrawGizmos(Path path, GizmoType gizmoType)
    {
        for (int i = 0; i < path.transform.childCount; i++)
        {
            Gizmos.DrawLine(path.transform.GetChild(i).position, path.transform.GetChild((i + 1) % path.transform.childCount).position);
        }
    }
}
