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

    /// <summary>
    /// Should only be instatiated by the player instance for the starting abilities or when a new ability is obtained
    /// </summary>
    /// <param name="player">Usually this</param>
    public Ability(Player player)
    {
        this.player = player;
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
}