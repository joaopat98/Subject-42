using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using JoystickUtils;
using TMPro;
using UnityEditor;

public class MainMenu : MonoBehaviour
{
    private int current = 0;
    private bool AxisInUse = false;
    public Color unselected = new Color(1f, 1f, 1f, 1f);
    public Color selected = new Color(0f, 1f, 1f, 1f);
    public GameObject credit;
    public AudioPlayer Sounds;

    public TextMeshProUGUI Play;
    public TextMeshProUGUI Credits;
    public TextMeshProUGUI Quit;

    private List<TextMeshProUGUI> Titles = new List<TextMeshProUGUI>();
    
    void updateHovering()
    {
        for( int i = 0; i < Titles.Count; i++)
        {
            if( i == current)
            {
                Titles[i].color = selected;
            }
            else
            {
                Titles[i].color = unselected;
            }
        }
    }

    void Execute()
    {
        Sounds.PlayOnce("Click");
        switch (current)
        {
            case 0:
                 SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
            case 1:
                credit.SetActive(true);
                gameObject.SetActive(false);
                break;
            default:
                Application.Quit();
                break;
        }
    }

    void Start()
    {
        Titles.Add(Play);
        Titles.Add(Credits);
        Titles.Add(Quit);
        credit.SetActive(false);
        Sounds.PlayLoop("MainMenu");
    }

    void Update()
    {
        if(Input.GetButtonDown("Interact"))
        {
            Execute();
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

        current = Mathf.Clamp(current, 0, Titles.Count-1);

        updateHovering();
    }
}
