using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAbility : MonoBehaviour
{
    public AbilityType abilityType;
    public float Range = 1f;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && Vector3.Distance(transform.position, player.transform.position) < Range)
        {
            player.Abilities.Add(Ability.FromType(abilityType, player));
            Destroy(gameObject);
        }
    }
}
