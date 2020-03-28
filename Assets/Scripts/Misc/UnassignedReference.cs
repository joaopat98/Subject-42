using UnityEngine;

public static partial class GameObject_ExtensionMethods
{
    // Stop Execution when an editor field was not assigned when it should have been
    public static void UnassignedReference(this GameObject go, string referenceName)
    {
        Debug.LogError("Unassigned Inspector reference: " + referenceName, go);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}