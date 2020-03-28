using UnityEngine;

/// <summary>
/// Ability to be used when the player hasn't obtained any actual abilities yet, has no particular behaviour
/// </summary>
public class EmptyAbility : Ability
{
    public EmptyAbility(Player player) : base(player)
    {

    }
    public override void Update()
    {

    }
}