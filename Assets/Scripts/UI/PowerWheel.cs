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

        //Up Power
        if (JoystickDirection.y > 0 && (JoystickDirection.x > -0.5 && JoystickDirection.x < 0.5))
        {
            CurrentAbility = 0;
            Type = AbilityType.Clairvoyance;
            PowerToActivate = GameObject.Find("RevealActivated");
          
        }
        //Left Power
        else if (JoystickDirection.x > 0 && (JoystickDirection.y > -0.5) && (JoystickDirection.y < 0.5))
        {
            CurrentAbility = 1;
            Type = AbilityType.Telekinesis;
            PowerToActivate = GameObject.Find("TelekinesisActivated");
            
        }
        //Right Power
        else if (JoystickDirection.x < 0 && (JoystickDirection.y > -0.5) && (JoystickDirection.y < 0.5))
        {
            CurrentAbility = 2;
            Type = AbilityType.ElectricityAbility;
            PowerToActivate = GameObject.Find("ElectricActivated");
        }

        //Lower right power
        else if (JoystickDirection.y < 0 && (JoystickDirection.x > -0.5 && JoystickDirection.x < 0.5))
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
