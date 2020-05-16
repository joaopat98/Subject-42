using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public LevelLoader LevelLoader;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            LevelLoader.LoadNextLevel();
        }
    }
}
