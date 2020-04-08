/// <summary>
/// Base class to manage the player's ability state
/// </summary>
public abstract class Ability
{
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
        //int next = player.CurrentAbility + delta;
        //while (next < 0)
        //{
        //    next += player.Abilities.Count;
        //}
        //player.CurrentAbility = next % player.Abilities.Count;
    }
}