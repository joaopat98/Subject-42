using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Base class to manage the player's ability state
/// </summary>

public abstract class Ability
{

    public static Dictionary<System.Type, AbilityType> Types = new Dictionary<System.Type, AbilityType>()
    {
        {typeof(ElectricityAbility),AbilityType.Electricity},
        {typeof(RevealAbility),AbilityType.Reveal},
        {typeof(TelekinesisAbility),AbilityType.Telekinesis},
        {typeof(EmptyAbility),AbilityType.Empty},
        {typeof(Test1Ability),AbilityType.Test1},
        {typeof(Test2Ability),AbilityType.Test2},
        {typeof(Test3Ability),AbilityType.Test3},
    };
    public static Ability FromType(AbilityType type, Player player)
    {
        switch (type)
        {
            case AbilityType.Electricity:
                return new ElectricityAbility(player);
            case AbilityType.Reveal:
                return new RevealAbility(player);
            case AbilityType.Telekinesis:
                return new TelekinesisAbility(player);
            case AbilityType.Test1:
                return new Test1Ability(player);
            case AbilityType.Test2:
                return new Test2Ability(player);
            case AbilityType.Test3:
                return new Test3Ability(player);
            default:
                return new EmptyAbility(player);
        }
    }

    public AbilityType type { get { return Types[GetType()]; } }
    protected Player player;
    protected Transform transform;
    protected Material playerMaterial;
    protected Coroutine currentFade;

    /// <summary>
    /// Should only be instatiated by the player instance for the starting abilities or when a new ability is obtained
    /// </summary>
    /// <param name="player">Usually this</param>
    public Ability(Player player)
    {
        this.player = player;
        transform = player.transform;
        playerMaterial = transform.Find("Null.1").GetComponent<Renderer>().material;
    }

    /// <summary>
    /// Update the ability's state
    /// </summary>
    public abstract void Update();

    /// <summary>
    /// Change the active ability. Override to implement cleanup and transition behaviour between abilities
    /// </summary>
    /// <param name="delta">Negative to cycle left in the list, positive to cycle right</param>
    public virtual void SwitchAbility(int delta)
    {
        int next = player.CurrentAbility + delta;
        while (next < 0)
        {
            next += player.Abilities.Count;
        }
        player.CurrentAbility = next % player.Abilities.Count;
    }

    protected void FadeInColor(Color color)
    {
        playerMaterial.SetColor("GlowColor", color);
        if (currentFade != null)
            player.StopCoroutine(currentFade);
        currentFade = player.StartCoroutine(FadeInColorRoutine());
    }

    protected IEnumerator FadeInColorRoutine()
    {
        float t = 0;
        float prevIntensity = playerMaterial.GetFloat("GlowIntensity");
        while (t < player.FadeInTime)
        {
            var i = EasingFunction.EaseInCubic(prevIntensity, 1, t / player.FadeInTime);
            playerMaterial.SetFloat("GlowIntensity", i);
            yield return 0;
            t += Time.deltaTime;
        }
        playerMaterial.SetFloat("GlowIntensity", 1);
        currentFade = null;
    }

    protected void FadeOutColor() {
        if (currentFade != null)
            player.StopCoroutine(currentFade);
        currentFade = player.StartCoroutine(FadeOutColorRoutine());
    }

    protected IEnumerator FadeOutColorRoutine()
    {
        float t = 0;
        float prevIntensity = playerMaterial.GetFloat("GlowIntensity");
        while (t < player.FadeOutTime)
        {
            var i = EasingFunction.EaseInCubic(prevIntensity, 0, t / player.FadeOutTime);
            playerMaterial.SetFloat("GlowIntensity", i);
            yield return 0;
            t += Time.deltaTime;
        }
        playerMaterial.SetFloat("GlowIntensity", 0);
        currentFade = null;
    }

}