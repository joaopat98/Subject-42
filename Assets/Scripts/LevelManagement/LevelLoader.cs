using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator Transition;

    public float TransitionTime = 1.5f;

    Player player;

    public int index = -1;
    
    public void LoadNextLevel()
    {
        if(index == -1)
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }
        StartCoroutine(LoadLevel(index));
    }

    IEnumerator LoadLevel(int LevelIndex)
    {
        Transition.SetTrigger("Start");
        yield return new WaitForSeconds(TransitionTime);
        SceneManager.LoadScene(LevelIndex);
    }
}
