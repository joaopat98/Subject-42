using UnityEngine;

public class Test2Ability : Ability
{
    public Test2Ability(Player player) : base(player)
    {

    }
    public override void Update()
    {
        if (Input.GetButtonDown("Power"))
        {
            Debug.Log("Test2");
        }
    }
}