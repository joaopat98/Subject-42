using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public GameObject menu;
    public AudioPlayer Sounds;
    void Update()
    {
        if(Input.GetButtonDown("Interact"))
        {
            Sounds.PlayOnce("Click");
            menu.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
