using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Player))]
public class PlayerView : Editor
{
    private static Vector3 Rotate(Vector3 point, Vector3 pivot, float angle)
    {
        var dir = point - pivot;
        dir = Quaternion.Euler(0, angle, 0) * dir;
        point = dir + pivot;
        return point;
    }

    [DrawGizmo(GizmoType.Selected)]
    static void DrawGizmos(Player player, GizmoType gizmoType)
    {
        Vector3 origin = player.transform.position;
        Vector3 front = origin + (player.transform.forward * player.ViewRange);
        Gizmos.DrawLine(origin, Rotate(front, origin, player.ViewAngle));
        Gizmos.DrawLine(origin, Rotate(front, origin, -player.ViewAngle));
        for (float angle = 0; angle < player.ViewAngle; angle += 0.5f)
        {
            Gizmos.DrawLine(Rotate(front, origin, angle), Rotate(front, origin, angle + 0.5f));
            Gizmos.DrawLine(Rotate(front, origin, -angle), Rotate(front, origin, -angle - 0.5f));
        }
        int numSpheres = 20;
        for (int i = 0; i < numSpheres; i++)
        {
            Gizmos.DrawSphere(player.Center + player.transform.forward * (player.ViewRange / (float)(numSpheres - 1) * i), player.CastSelectRadius);
        }
    }
}

