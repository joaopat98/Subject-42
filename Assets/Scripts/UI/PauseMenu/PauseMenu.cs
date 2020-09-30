using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class PauseMenu : MonoBehaviour
{
    private bool show = false;
    [HideInInspector] public int current;
    private bool AxisInUse = false;

    public Sprite resumeNormal;
    public Sprite resumeSelected;
    public Sprite restartNormal;
    public Sprite restartSelected;
    public Sprite quitNormal;
    public Sprite quitSelected;

    private List<Sprite> Normal = new List<Sprite>();
    private List<Sprite> Selected = new List<Sprite>();
    private List<Image> Buttons = new List<Image>();

    public Image Resume;
    public Image Restart;
    public Image Quit;


    void Start()
    {
        Normal.Add(resumeNormal);
        Normal.Add(restartNormal);
        Normal.Add(quitNormal);

        Selected.Add(resumeSelected);
        Selected.Add(restartSelected);
        Selected.Add(quitSelected);

        Buttons.Add(Resume);
        Buttons.Add(Restart);
        Buttons.Add(Quit);

        current = 0;

        for (int i = 0; i < Buttons.Count; i++)
        {
            Buttons[i].sprite = Normal[i];
            Buttons[i].color = new Color(Buttons[i].color.r, Buttons[i].color.g, Buttons[i].color.b, 1f);
        }
    }

    void execute()
    {
        switch (current)
        {
            case 0:
                show = !show;
                break;
            case 1:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            default:
                Application.Quit();
                break;
        }
    }

    void updateSelected()
    {
        for (int i = 0; i < Buttons.Count; i++)
        {
            if (i == current)
            {
                Buttons[i].sprite = Selected[i];
            }

            else
            {
                Buttons[i].sprite = Normal[i];
            }
        }
    }


    void Update()
    {
        if (Input.GetButtonDown("Start"))
        {
            current = 0;
            show = !show;
        }

        if (show)
        {
            if (Input.GetButtonDown("Interact"))
            {
                execute();
            }

            else if (Input.GetAxisRaw("DPad Y") == 0)
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

            current = Mathf.Clamp(current, 0, 2);

            updateSelected();
        }

        GetComponent<Canvas>().enabled = show;
    }
}