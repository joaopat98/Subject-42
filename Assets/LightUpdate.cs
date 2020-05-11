using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightUpdate : MonoBehaviour
{
    Guard guard;
    Light spotlight;
    // Start is called before the first frame update
    void OnEnable()
    {
        guard = GetComponentInParent<Guard>();
        spotlight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        spotlight.range = guard.ViewRange;
        spotlight.innerSpotAngle = 0;
        spotlight.spotAngle = 2 * guard.ViewAngle;
    }
}
