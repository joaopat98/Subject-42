using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public LevelLoader LevelLoader;
    public bool Enabled = true;
    private void OnTriggerEnter(Collider other)
    {
        if (Enabled)
        {
            if (other.tag == "Player")
            {
                other.GetComponentInChildren<Player>().Playing = false;

                LevelLoader.LoadNextLevel();
            }
        }
    }
}
