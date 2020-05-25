using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour

{
    public GameObject menu;
    
    void Update()
    {
        if(Input.GetButtonDown("Interact"))
        {
            menu.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
