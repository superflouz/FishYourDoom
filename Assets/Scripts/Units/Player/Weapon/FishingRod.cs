using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : Weapon
{
    public Player player;
    public Hook hook;
    private Stats playerStats;
    private Animator animator;

    private bool hookThrown;
    
    public float attackPower;
    public float attackSpeed;

    public float throwStrenght;
    public float lineLenght;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerStats = player.Stats;

        hookThrown = false;
        hook.player = player;
    }

    void Start()
    {        
       
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
        if (hookThrown == false)
        {
            Vector2 v = Globals.DegreeToVector2(transform.parent.rotation.eulerAngles.z - 90);
            ThrowHook(v);            
        }
        else if (hookThrown == true)
        {
            RetrieveHook();           
        }
    }   

    private void ThrowHook(Vector2 direction)
    {
        hookThrown = true;
        hook.gameObject.SetActive(true);
        hook.Throw(direction, throwStrenght);

        // Set animation trigger
    }

    private void RetrieveHook()
    {
        hookThrown = false;
        hook.gameObject.SetActive(false);

        // Set animation trigger
    }
}
