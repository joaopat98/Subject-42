using UnityEngine;

public static class HorizontalDirExtension
{
    /// <summary>
    /// Convert a Vector2 to a Vector3 in the xOz plane
    /// </summary>
    public static Vector3 ToHorizontalDir(this Vector2 v)
    {
        return new Vector3(v.x, 0, v.y);
    }
}