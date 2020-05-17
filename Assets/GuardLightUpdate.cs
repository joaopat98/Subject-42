using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class GuardLightUpdate : MonoBehaviour
{
    Guard guard;
    Light spotlight;
    VLight volume;
    public float Falloff = 5;
    [Range(0, Mathf.Infinity)]
    public float ColorFadeTime = 0.25f;
    private GuardActionType currentState;
    private Coroutine currentFade;
    // Start is called before the first frame update
    void OnEnable()
    {
        guard = GetComponentInParent<Guard>();
        spotlight = GetComponent<Light>();
        volume = GetComponent<VLight>();
        currentState = GuardActionType.Patrol;
        spotlight.color = guard.PatrolColor;
        volume.colorTint = guard.PatrolColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (guard.action != null && currentState != guard.action.type)
        {
            currentState = guard.action.type;
            if (currentFade != null)
                StopCoroutine(currentFade);
            currentFade = StartCoroutine(FadeToColor());
        }
        spotlight.range = guard.ViewRange;
        spotlight.innerSpotAngle = 2 * guard.ViewAngle;
        spotlight.spotAngle = 2 * guard.ViewAngle + Falloff;
        volume.spotRange = guard.ViewRange;
        volume.spotAngle = 2 * guard.ViewAngle;
    }

    IEnumerator FadeToColor()
    {
        var prevColor = spotlight.color;
        Color toColor;
        switch (currentState)
        {
            case GuardActionType.Patrol:
                toColor = guard.PatrolColor;
                break;
            case GuardActionType.Check:
                toColor = guard.CheckColor;
                break;
            case GuardActionType.Chase:
                toColor = guard.ChaseColor;
                break;
            default:
                Debug.LogError("Color for " + currentState.ToString() + " not defined!");
                toColor = Color.black;
                break;
        }
        float t = 0;
        while (t < ColorFadeTime)
        {
            float fac = EasingFunction.EaseInOutCubic(0, 1, t / ColorFadeTime);
            Color newColor = Color.Lerp(prevColor, toColor, fac);
            spotlight.color = newColor;
            volume.colorTint = newColor;
            yield return 0;
            t += Time.deltaTime;
        }
        spotlight.color = toColor;
        volume.colorTint = toColor;
        currentFade = null;
    }
}
