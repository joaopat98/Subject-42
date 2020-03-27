using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Guard))]
public class GuardView : Editor
{
    private static Vector3 Rotate(Vector3 point, Vector3 pivot, float angle)
    {
        var dir = point - pivot;
        dir = Quaternion.Euler(0, angle, 0) * dir;
        point = dir + pivot;
        return point;
    }

    [DrawGizmo(GizmoType.Selected)]
    static void DrawGizmos(Guard guard, GizmoType gizmoType)
    {
        Vector3 origin = guard.transform.position;
        Vector3 front = origin + (guard.transform.forward * (guard.action == null || guard.action.GetType() == typeof(GuardPatrol) ? guard.ViewRange : guard.ChaseRange));
        Gizmos.DrawLine(origin, Rotate(front, origin, guard.ViewAngle));
        Gizmos.DrawLine(origin, Rotate(front, origin, -guard.ViewAngle));
        for (float angle = 0; angle < guard.ViewAngle; angle += 0.5f)
        {
            Gizmos.DrawLine(Rotate(front, origin, angle), Rotate(front, origin, angle + 0.5f));
            Gizmos.DrawLine(Rotate(front, origin, -angle), Rotate(front, origin, -angle - 0.5f));
        }
    }
}

