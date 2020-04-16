using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using JoystickUtils;

public class PowerWheel : MonoBehaviour
{

    Player player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    /// <summary>
    /// Receives Input from the right joystick and switches power
    /// depending on the direction the joystick is pushed
    /// </summary>
    public int PowerSwitch(int Ability)
    {
        //Activate Power Wheel
        showInactivePowers();
        //Get Direction of Right Joystick
        Vector2 JoystickDirection = Joystick.GetJoystick2Dir();
        int CurrentAbility = Ability;
        AbilityType Type = AbilityType.Empty;
        GameObject PowerToActivate = new GameObject();
        GameObject PowerToDesactivate = new GameObject();

        //Up Power
        if (JoystickDirection.y > 0.7 && (JoystickDirection.x > -0.7 && JoystickDirection.x < 0.7))
        {
            CurrentAbility = 0;
            Type = AbilityType.Reveal;
            PowerToActivate = GameObject.Find("RevealActivated");
            PowerToDesactivate = GameObject.Find("Reveal");


        }
        //Left Power
        else if (JoystickDirection.x > 0.7 && (JoystickDirection.y >= -0.7) && (JoystickDirection.y <= 0.7))
        {
            CurrentAbility = 1;
            Type = AbilityType.Telekinesis;
            PowerToActivate = GameObject.Find("TelekinesisActivated");
            PowerToDesactivate = GameObject.Find("Telekinesis");

        }
        //Right Power
        else if (JoystickDirection.x < -0.7 && (JoystickDirection.y >= -0.7) && (JoystickDirection.y <= 0.7))
        {
            CurrentAbility = 2;
            Type = AbilityType.Electricity;
            PowerToActivate = GameObject.Find("ElectricActivated");
            PowerToDesactivate = GameObject.Find("Electric");
        }

        //Lower right power
        else if (JoystickDirection.y < -0.7 && (JoystickDirection.x > -0.7 && JoystickDirection.x < 0.7))
        {
            CurrentAbility = 3;
            PowerToActivate = GameObject.Find("TimeActivated");
        }
        //This happens when the joystick is not moved.
        else
        {
            GameObject.Find("PowerWheel").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("ElectricActivated").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("RevealActivated").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("TelekinesisActivated").GetComponent<SpriteRenderer>().enabled = false;
        }
        if (!Type.Equals(AbilityType.Empty))
        {
            PowerToActivate.GetComponent<SpriteRenderer>().enabled = true;
            PowerToDesactivate.GetComponent<SpriteRenderer>().enabled = false;
        }
        return CurrentAbility;
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
