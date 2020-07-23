using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : Weapon
{
    public Player player;
    public Hook hook;
    private Stats playerStats;
    private Animator playerAnimator;
    private Animator animator;

    private bool hookThrown;
    
    public float attackPower;
    public float attackSpeed;

    public float throwStrenght;
    public float lineLenght;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerAnimator = player.GetComponent<Animator>();
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
        animator.SetTrigger("Attack");
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
        // Set animation trigger   
        playerAnimator.SetBool("HookThrown", hookThrown);

        hookThrown = true;
        hook.gameObject.SetActive(true);
        hook.Throw(direction, throwStrenght);      
    }

    private void RetrieveHook()
    {
        // Set animation trigger
        playerAnimator.SetBool("HookThrown", hookThrown);

        hookThrown = false;
        hook.gameObject.SetActive(false);   
        
        // Reel in hook
    }
}
