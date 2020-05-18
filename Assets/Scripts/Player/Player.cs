using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using JoystickUtils;
using System.Media;

public class Player : MonoBehaviour
{
    /// <summary>
    /// Rigidbody attached to this GameObject
    /// </summary>
    Rigidbody rb;
    public Material DeadMaterial;

    /// <summary>
    /// Player's animator controller
    /// </summary>
    [HideInInspector] public Animator anim;
    /// <summary>
    /// List of triggers animations
    /// </summary>
    TriggerAnim[] triggerAnim;

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
    public List<Ability> Abilities;
    /// <summary>
    /// Index of the players current ability in <see cref="Abilities"/>
    /// </summary>
    /// <returns></returns>
    [HideInInspector] public int CurrentAbility = 0;

    [HideInInspector] public Vector3 Center;

    /// <summary>
    /// Field of view where the player can use the power
    /// </summary>
    [Header("Object Interaction")] [Range(0, 180)] public float ViewAngle = 20.0f;

    [HideInInspector] public AudioPlayer Sounds;

    /// <summary>
    /// Distance in front of the player. It limits how far can he use the power.
    /// </summary>
    public float ViewRange = 7f;
    public float TelekinesisRange = 7f;
    public float TelekinesisMoveLimit = 0.5f;
    public float TelekinesisMoveClose = 1;
    public float TelekinesisMoveSpeed = 7f;
    public float TelekinesisRotateSpeed = 180f;
    public float CastSelectRadius = 0.5f;
    public float TelekinesisMaxSpeed = 3.0f;

    [Header("Ability Highlights")]
    public float FadeInTime = 0.25f;
    public float FadeOutTime = 1;
    public Color TelekinesisColor, RevealColor, ElectricityColor;

    /// <summary>
    /// Max speed to cool down the player using the clairvoyance power
    /// </summary>
    [Header("Reveal")] public float RevealMaxSpeed;

    /// <summary>
    /// UI for switching powers
    /// </summary>
    PowerWheel PowerWheel;
    bool powerWheelOpen;
    AbilityType selectedAbility;

    float triggerPrevious = 0;
    public int SwitchStatus;

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
                    case AbilityType.Reveal:
                        Abilities.Add(new RevealAbility(this));
                        break;
                    case AbilityType.Electricity:
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
        PowerWheel = GameObject.FindGameObjectWithTag("PowerWheel").GetComponent<PowerWheel>();
        anim = GetComponent<Animator>();
        triggerAnim = (TriggerAnim[])System.Enum.GetValues(typeof(TriggerAnim));
        Sounds = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioPlayer>();
        Sounds.PlayLoop("AmbientSound");

    }

    void Update()
    {
        Center = transform.GetChild(0).position;

        // Movement
        if (isAlive)
            Move();

        // Update the current ability's state
        Abilities[CurrentAbility].Update();

        // Switch abilities depending on user input
        if (Input.GetAxisRaw("Switch") >= 0.9 && triggerPrevious < 0.9)
        {
            SwitchStatus = 1;
            Abilities[CurrentAbility].SwitchAbility(1);
        }
        else if (Input.GetAxisRaw("Switch") <= -0.9 && triggerPrevious > -0.9)
        {
            SwitchStatus = -1;
            Abilities[CurrentAbility].SwitchAbility(-1);
        }
        else
        {
            SwitchStatus = 0;
        }
        triggerPrevious = Input.GetAxisRaw("Switch");

        UpdateMovementAnim();

    }

    /// <summary>
    /// Routine to move the player;
    /// </summary>
    private void Move()
    {
        ///Check if there is any animation triggered 
        bool isAnimTrigger = IsAnimTrigger();

        // Get the camera angle relative to the world z axis
        var camTransform = Camera.main.transform;
        var camAngle = Mathf.Rad2Deg * Mathf.Atan2(camTransform.forward.x, camTransform.forward.z);

        // Apply camera angle to the movement direction, making movement relative to the camera
        var dir = Joystick.GetJoystick1Dir().ToHorizontalDir().CameraCorrect();
        if (Input.GetButton("Slow") && dir.magnitude > 0.5f)
        {
            dir = dir.normalized * 0.5f;
        }

        // Rotate player towards the direction it is moving in
        if (dir != Vector3.zero && !isAnimTrigger)
            rb.MoveRotation(Quaternion.LookRotation(dir, Vector3.up));
        else
            rb.angularVelocity = Vector3.zero;

        rb.velocity = new Vector3(MoveSpeed * dir.x, rb.velocity.y, MoveSpeed * dir.z);



    }

    /// <summary>
    /// Update the animation state
    /// </summary>
    private void UpdateMovementAnim()
    {

        ///Check if there is any animation triggered 
        bool isAnimTrigger = IsAnimTrigger();

        if (!isAnimTrigger)
        {
            Vector2 vel = new Vector2(rb.velocity.x, rb.velocity.z);
            anim.SetFloat("Speed", vel.magnitude);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    public bool IsAnimTrigger()
    {
        bool isTrigger = false;
        foreach (TriggerAnim animation in triggerAnim)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName(animation.ToString()))
            {
                isTrigger = true;
                break;
            }
        }
        return isTrigger;
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
        anim.SetTrigger("Death");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddAbility(AbilityType type)
    {
        if (Abilities[0].type == AbilityType.Empty)
        {
            Abilities[0] = Ability.FromType(type, this);
        }
        else
        {
            Abilities.Add(Ability.FromType(type, this));
        }
    }

    public void SwitchAbility(AbilityType ability)
    {
        int index = Abilities.FindIndex(a => a.type == ability);
        if (index != -1 && index != CurrentAbility)
            Abilities[CurrentAbility].SwitchAbility(index - CurrentAbility);
    }

    public void SwitchAbility(int delta)
    {
        Abilities[CurrentAbility].SwitchAbility(delta);
    }

}
