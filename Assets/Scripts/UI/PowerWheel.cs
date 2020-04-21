using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using JoystickUtils;

public class PowerWheel : MonoBehaviour
{
    public float ScrollSpeed = 1;
    private static List<AbilityType> typeOrder = new List<AbilityType>() {
        AbilityType.Reveal,
        AbilityType.Telekinesis,
        AbilityType.None,
        AbilityType.Electricity
        };
    float scrollVal = 0;

    Player player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    /// <summary>
    /// Receives Input from the right joystick and switches power
    /// depending on the direction the joystick is pushed
    /// </summary>
    public AbilityType PowerSwitch(AbilityType abilityType)
    {
        //Activate Power Wheel
        showInactivePowers();
        //Get Direction of Right Joystick
        Vector2 JoystickDirection = Joystick.GetJoystick2Dir();
        int wheelNum = typeOrder.FindIndex(t => t == abilityType);
        //Up Power
        if (JoystickDirection.y > 0.7 && (JoystickDirection.x > -0.7 && JoystickDirection.x < 0.7))
        {
            wheelNum = 0;
        }
        //Right Power
        else if (JoystickDirection.x > 0.7 && (JoystickDirection.y > -0.7) && (JoystickDirection.y < 0.7))
        {
            wheelNum = 1;
        }
        //Down power
        else if (JoystickDirection.y < -0.7 && (JoystickDirection.x > -0.7 && JoystickDirection.x < 0.7))
        {
            wheelNum = 2;
        }
        //Left Power
        else if (JoystickDirection.x < -0.7 && (JoystickDirection.y > -0.7) && (JoystickDirection.y < 0.7))
        {
            wheelNum = 3;
        }

        scrollVal = -Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;
        Debug.Log(scrollVal);
        if (wheelNum == -1)
        {
        }
        else
        {
            if (player.Abilities[0].type != AbilityType.Empty)
            {
                while (scrollVal >= 1)
                {
                    scrollVal -= 1;
                    do
                    {
                        wheelNum = (wheelNum + 1) % typeOrder.Count;
                    } while (player.Abilities.FindIndex(a => a.type == typeOrder[wheelNum]) == -1);
                }
                while (scrollVal <= -1)
                {
                    scrollVal += 1;
                    do
                    {
                        wheelNum = (wheelNum - 1 + typeOrder.Count) % typeOrder.Count;
                    } while (player.Abilities.FindIndex(a => a.type == typeOrder[wheelNum]) == -1);
                }
                if (player.Abilities.FindIndex(a => a.type == typeOrder[wheelNum]) != -1)
                    abilityType = typeOrder[wheelNum];
            }
        }
        Debug.Log(abilityType);
        if (abilityType != AbilityType.Empty)
        {
            GameObject.Find("PowerWheel").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("ElectricActivated").GetComponent<SpriteRenderer>().enabled = abilityType == AbilityType.Electricity;
            GameObject.Find("RevealActivated").GetComponent<SpriteRenderer>().enabled = abilityType == AbilityType.Reveal;
            GameObject.Find("TelekinesisActivated").GetComponent<SpriteRenderer>().enabled = abilityType == AbilityType.Telekinesis;
        }
        return abilityType;
    }

    /// <summary>
    /// When R2 is no longer pressed, the power 
    /// wheel dissapears
    /// </summary>
    public void CleanUp()
    {
        GameObject.Find("PowerWheel").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("ElectricActivated").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("RevealActivated").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("TelekinesisActivated").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("Time").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("Electric").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("Telekinesis").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("Reveal").GetComponent<SpriteRenderer>().enabled = false;
        scrollVal = 0;
    }

    private void showInactivePowers()
    {
        GameObject.Find("PowerWheel").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("Time").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("Electric").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("Telekinesis").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("Reveal").GetComponent<SpriteRenderer>().enabled = true;
    }
}
