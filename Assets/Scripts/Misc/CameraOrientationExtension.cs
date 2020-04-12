using UnityEngine;

public static class CameraOrientationExtension
{
    /// <summary>
    /// Get the camera angle relative to the world z axis
    /// </summary>
    /// <param name="camera"></param>
    /// <returns></returns>
    public static float GetHorizontalOrientation(this Camera camera)
    {

        var camTransform = camera.transform;
        return Mathf.Rad2Deg * Mathf.Atan2(camTransform.forward.x, camTransform.forward.z);
    }

    /// <summary>
    /// Rotate this vector around the Y axis so it's relative to the main camera's orientation
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Vector3 CameraCorrect(this Vector3 v)
    {
        return Quaternion.Euler(0, Camera.main.GetHorizontalOrientation(), 0) * v;
    }
}