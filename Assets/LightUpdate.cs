using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightUpdate : MonoBehaviour
{
    Guard guard;
    Light spotlight;
    VLight volume;
    public float Falloff = 5;
    // Start is called before the first frame update
    void OnEnable()
    {
        guard = GetComponentInParent<Guard>();
        spotlight = GetComponent<Light>();
        volume = GetComponent<VLight>();
    }

    // Update is called once per frame
    void Update()
    {
        spotlight.range = guard.ViewRange;
        spotlight.innerSpotAngle = 2 * guard.ViewAngle;
        spotlight.spotAngle = 2 * guard.ViewAngle + Falloff;
        volume.spotRange = guard.ViewRange;
        volume.spotAngle = 2 * guard.ViewAngle;
    }
}
