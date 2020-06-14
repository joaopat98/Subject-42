using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightUpdate : MonoBehaviour
{
    Guard guard;
    Light spotlight;
    VLight volume;
    // Start is called before the first frame update
    void OnEnable()
    {
        guard = GetComponentInParent<Guard>();
        spotlight = GetComponent<Light>();
        volume = GetComponent<VLight>();
        volume.colorTint = spotlight.color;
    }

    // Update is called once per frame
    void Update()
    {
        volume.spotRange = spotlight.range;
        volume.spotAngle = spotlight.innerSpotAngle;
    }
}
