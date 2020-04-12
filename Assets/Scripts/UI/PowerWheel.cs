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
        GameObject.Find("PowerWheel").GetComponent<RawImage>().enabled = true;
        //Get Direction of Right Joystick
        Vector2 JoystickDirection = Joystick.GetJoystick2Dir();
        int CurrentAbility = Ability;

        //Upper right power
        if (JoystickDirection.x > 0 && JoystickDirection.y > 0)
        {
            CurrentAbility = 0;
            GameObject.Find("PowerWheelTime").GetComponent<RawImage>().enabled = true;
        }
        //Upper left power
        else if (JoystickDirection.x < 0 && JoystickDirection.y > 0)
        {
            CurrentAbility = 1;
            GameObject.Find("PowerWheelElectric").GetComponent<RawImage>().enabled = true;
        }
        //Lower left power
        else if (JoystickDirection.x < 0 && JoystickDirection.y < 0)
        {
            CurrentAbility = 2;
            GameObject.Find("PowerWheelTelekinesis").GetComponent<RawImage>().enabled = true;
        }
        //Lower right power
        else if (JoystickDirection.x > 0 && JoystickDirection.y < 0)
        {
            CurrentAbility = 3;
            GameObject.Find("PowerWheelClaivoryance").GetComponent<RawImage>().enabled = true;
        }
        //This happens when the joystick is not moved.
        else
        {
            GameObject.Find("PowerWheel").GetComponent<RawImage>().enabled = true;
            GameObject.Find("PowerWheelTime").GetComponent<RawImage>().enabled = false;
            GameObject.Find("PowerWheelElectric").GetComponent<RawImage>().enabled = false;
            GameObject.Find("PowerWheelClaivoryance").GetComponent<RawImage>().enabled = false;
            GameObject.Find("PowerWheelTelekinesis").GetComponent<RawImage>().enabled = false;
        }
        return CurrentAbility;
    }

    /// <summary>
    /// When R2 is no longer pressed, the power 
    /// wheel dissapears
    /// </summary>
    public void CleanUp()
    {
        GameObject.Find("PowerWheel").GetComponent<RawImage>().enabled = false;
        GameObject.Find("PowerWheelTime").GetComponent<RawImage>().enabled = false;
        GameObject.Find("PowerWheelElectric").GetComponent<RawImage>().enabled = false;
        GameObject.Find("PowerWheelClaivoryance").GetComponent<RawImage>().enabled = false;
        GameObject.Find("PowerWheelTelekinesis").GetComponent<RawImage>().enabled = false;
    }
}
