using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisibilityPanel : MonoBehaviour
{
    private bool show = false;
    public GameObject Panel;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            show = !show;
        }

        Panel.gameObject.SetActive(show);
    }
}
