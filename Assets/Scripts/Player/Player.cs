﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using JoystickUtils;

public class Player : MonoBehaviour
{
    /// <summary>
    /// Rigidbody attached to this GameObject
    /// </summary>
    Rigidbody rb;
    public Material DeadMaterial;
    /// <summary>
    /// Speed at which the player shall move
    /// </summary>
    [Header("Movement")] public float MoveSpeed;

    [Header("State")] public bool isAlive = true;

    /// <summary>
    /// Abilities the player will start with
    /// </summary>
    public List<AbilityType> StartingAbilities;
    /// <summary>
    /// List of abilities the player currently has
    /// </summary>
    /// <returns></returns>
    [HideInInspector] public List<Ability> Abilities;
    /// <summary>
    /// Index of the players current ability in <see cref="Abilities"/>
    /// </summary>
    /// <returns></returns>
    [HideInInspector] public int CurrentAbility = 0;

    /// <summary>
    /// Field of view where the player can use the power
    /// </summary>
    [Header("Object Interaction")] [Range(0, 180)] public float ViewAngle = 20.0f;

    /// <summary>
    /// Distance in front of the player. It limits how far can he use the power.
    /// </summary>
    public float ViewRange = 7f;
    public float TelekinesisRange = 7f;
    public float TelekinesisMoveLimit = 0.5f;
    public float TelekinesisMoveClose = 1;
    public float TelekinesisMoveSpeed = 7f;
    public float TelekinesisRotateSpeed = 180f;

    /// <summary>
    /// Initialize the <see cref="Abilities"/> array according to the ability types
    /// set in <see cref="StartingAbilities"/>. If <see cref="StartingAbilities"/> is empty,
    /// the array will be initialized with an instance of <see cref="EmptyAbility"/>,
    /// indicating that the player has no abilities
    /// </summary>
    void InitAbilities()
    {
        Abilities = new List<Ability>();
        if (StartingAbilities.Count == 0)
            Abilities.Add(new EmptyAbility(this));
        else
        {
            foreach (var abilityType in StartingAbilities)
            {
                switch (abilityType)
                {
                    case AbilityType.Test1:
                        Abilities.Add(new Test1Ability(this));
                        break;
                    case AbilityType.Test2:
                        Abilities.Add(new Test2Ability(this));
                        break;
                    case AbilityType.Test3:
                        Abilities.Add(new Test3Ability(this));
                        break;
                    case AbilityType.Clairvoyance:
                        Abilities.Add(new Clairvoyance(this));
                        break;
                    case AbilityType.ElectricityAbility:
                        Abilities.Add(new ElectricityAbility(this));
                        break;
                    case AbilityType.Telekinesis:
                        Abilities.Add(new TelekinesisAbility(this));
                        break;
                    default:
                        break;
                }
            }
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        InitAbilities();
    }

    void Update()
    {
        // Movement
        if (isAlive)
            Move();

        // Update the current ability's state
        Abilities[CurrentAbility].Update();

        // Switch abilities depending on user input
        if (Input.GetButtonDown("Switch Left") ^ Input.GetButtonDown("Switch Right"))
        {
            Abilities[CurrentAbility].SwitchAbility(
                (Input.GetButtonDown("Switch Left") ? -1 : 0) +
                (Input.GetButtonDown("Switch Right") ? 1 : 0)
                );
        }
    }

    /// <summary>
    /// Routine to move the player;
    /// </summary>
    private void Move()
    {
        // Get the camera angle relative to the world z axis
        var camTransform = Camera.main.transform;
        var camAngle = Mathf.Rad2Deg * Mathf.Atan2(camTransform.forward.x, camTransform.forward.z);

        // Apply camera angle to the movement direction, making movement relative to the camera
        var dir = Joystick.GetJoystick1Dir().ToHorizontalDir().CameraCorrect();

        // Rotate player towards the direction it is moving in
        if (dir != Vector3.zero)
            rb.MoveRotation(Quaternion.LookRotation(dir, Vector3.up));
        else
            rb.angularVelocity = Vector3.zero;
        rb.velocity = new Vector3(MoveSpeed * dir.x, rb.velocity.y, MoveSpeed * dir.z);
    }

    /// <summary>
    /// Disable input and schedule level restart
    /// </summary>
    public void Kill()
    {
        isAlive = false;
        GetComponent<Renderer>().material = DeadMaterial;
        StartCoroutine(Reset());
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
