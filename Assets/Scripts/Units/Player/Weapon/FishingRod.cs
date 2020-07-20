using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : Weapon
{
    public Player player;
    public Hook hook;
    private Stats playerStats;
    private Animator animator;
    
    public float attackPower;
    public float attackSpeed;

    public float throwStrenght;
    public float lineLenght;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerStats = player.Stats;
    }

    void Update()
    {
        
    }

    public override void Attack()
    {
        // Set animation trigger
    }

    public override void SpecialAttack()
    {
        Vector2 v = Globals.DegreeToVector2(transform.parent.rotation.eulerAngles.z - 90);
        ThrowHook(v);
        // Set animation trigger
    }   

    private void ThrowHook(Vector2 direction)
    {
        if (hook != null)
            hook.Throw(direction, throwStrenght);
    }
}
