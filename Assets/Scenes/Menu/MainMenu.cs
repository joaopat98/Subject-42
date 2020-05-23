using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using JoystickUtils;

public class MainMenu : MonoBehaviour
{
    private int current = 0;
    private bool AxisInUse = false;
    private List<Transform> slots;
    public GameObject Select;

    void Start()
    {
        slots = transform.Find("Panel").Find("MenuItems").GetChildren();
    }

    void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void execute()
    {
        switch (current)
        {
            case 0:
                PlayGame();
                break;
            case 1:
                LoadScene("HowToPlay");
                break;
            case 2:
                LoadScene("Credits");
                break;
            default:
                Application.Quit();
                break;
        }
    }

    void updateHovering()
    {
        Select.transform.position = slots[current].transform.position;
    }


    void Update()
    {
        if(Input.GetButtonDown("Interact"))
        {
            execute();
        }

        else if(Input.GetAxisRaw("DPad Y") == 0)
        {
            AxisInUse = false;
        }

        else if (Input.GetAxisRaw("DPad Y") == -1 && !AxisInUse) 
        {
            current++;
            AxisInUse = true;    
        }

        else if (Input.GetAxisRaw("DPad Y") == 1 && !AxisInUse)
        {
            current--;
            AxisInUse = true;
        }

        current = Mathf.Clamp(current, 0, slots.Count-1);

        updateHovering();
    }
}
