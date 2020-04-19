using UnityEngine;

public class Test1Ability : Ability
{
    public Test1Ability(Player player) : base(player)
    {

    }
    public override void Update()
    {
        if (Input.GetButtonDown("Power") || Input.GetMouseButtonDown(0))
        {
            Debug.Log("Test1");
        }
    }
}