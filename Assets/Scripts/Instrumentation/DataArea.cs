using UnityEngine;
using UnityEngine.SceneManagement;

public class DataArea : MonoBehaviour
{
    string levelName;
    int activeColliders = 0;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
            activeColliders++;
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
            activeColliders--;
    }

    void Start()
    {
        levelName = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        if (activeColliders > 0)
            DataCollector.AreaUpdate(levelName, name);
    }
}