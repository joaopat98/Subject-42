using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class RenderFeatureManager : MonoBehaviour
{
    public Material EnemiesBehindMaterial;
    // Start is called before the first frame update
    void Start()
    {
        EnemiesBehindMaterial.SetFloat("Opacity", 0);
    }

    public void SeeEnemiesBehind(bool see)
    {
        EnemiesBehindMaterial.SetFloat("Opacity", see ? 1 : 0);
    }
}
