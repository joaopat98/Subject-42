using UnityEngine;

public class Test3Ability : Ability
{
    public Test3Ability(Player player) : base(player)
    {

    }
    public override void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            Debug.Log("Test3");
        }
    }
}