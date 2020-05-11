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
    AbilityType abilityType;
    Player player;

    Dictionary<AbilityType, Image> ActivatedIcons = new Dictionary<AbilityType, Image>();
    Dictionary<AbilityType, Image> DefaultIcons = new Dictionary<AbilityType, Image>();

    Image WheelImage;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        WheelImage = transform.Find("PowerWheel").GetComponent<Image>();
        WheelImage.gameObject.SetActive(false);
        foreach (var type in typeOrder)
        {
            if (type != AbilityType.None)
            {
                var wheelPart = WheelImage.transform.Find("PowerWheel" + type);
                DefaultIcons[type] = wheelPart.Find(type.ToString()).GetComponent<Image>();
                ActivatedIcons[type] = wheelPart.Find(type + "Activated").GetComponent<Image>();
            }
        }
    }

    /// <summary>
    /// Receives Input from the right joystick and switches power
    /// depending on the direction the joystick is pushed
    /// </summary>
    public void Update()
    {
        if (Input.GetButtonDown("Power Wheel"))
        {
            WheelImage.gameObject.SetActive(true);
            abilityType = player.Abilities[player.CurrentAbility].type;
            foreach (var icon in ActivatedIcons)
            {
                icon.Value.enabled = icon.Key == abilityType;
            }
        }
        if (Input.GetButton("Power Wheel"))
        {
            AbilityType newAbility = UpdateSelected();
            if (newAbility != AbilityType.Empty && newAbility != abilityType)
            {
                foreach (var icon in ActivatedIcons)
                {
                    icon.Value.enabled = icon.Key == newAbility;
                }
                abilityType = newAbility;
            }
        }
        if (Input.GetButtonUp("Power Wheel"))
        {
            WheelImage.gameObject.SetActive(false);
            scrollVal = 0;
            player.SwitchAbility(abilityType);
        }
    }

    AbilityType UpdateSelected()
    {
        AbilityType newAbility = abilityType;
        //Get Direction of Right Joystick
        Vector2 JoystickDirection = Joystick.GetJoystick2Dir();
        int wheelNum = typeOrder.FindIndex(t => t == newAbility);
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
        if (wheelNum != -1)
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
                    newAbility = typeOrder[wheelNum];
            }
        }
        return newAbility;
    }
}
