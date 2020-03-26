using UnityEngine;

public static class HorizontalDirExtension
{
    public static Vector3 ToHorizontalDir(this Vector2 v)
    {
        return new Vector3(v.x, 0, v.y);
    }
}