using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishingRod : Weapon
{
    public Player player;
    public Transform pivot;
    public Hook hook;
    public GameObject hookBody;
    private Stats playerStats;
    private Animator playerAnimator;
    private Animator animator;
    private PlayerInput playerInput;
    private PlayerInput hookInput;

    private bool hookThrown;
    
    public float attackPower;
    public float attackSpeed;

    public float throwStrenght;
    public float lineLenght;

    private float ReelTimer;
    private bool isReeling;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerAnimator = player.GetComponent<Animator>();
        hookInput = GetComponent<PlayerInput>();
        playerInput = player.GetComponent<PlayerInput>();       
        playerStats = player.Stats;

        isReeling = false;
        hookThrown = false;
        hook.player = player;

        hookInput.currentActionMap.Disable();
    }

    void Start()
    {        
       
    }

    void Update()
    {
        if(ReelTimer > 0)
        {
            ReelTimer -= Time.deltaTime;

            hook.transform.position = Vector2.MoveTowards(hook.transform.position, player.transform.position, 0.05f);
        }
        else if (isReeling)
        {
            HookFinishedReeling();            
        }
    }

    // Fired by InputSystem
    public void OnReel()
    {

    }

    // Fired by InputSystem
    public void OnInstantReel()
    {
        HookSpecialAction();
    }

    // Fired by the player using this weapon
    public override void Attack()
    {
        // Set animation trigger
        animator.SetInteger("Angle", playerAnimator.GetInteger("Angle"));
        animator.SetTrigger("Attack");
    }

    // Fired by the player using this weapon
    public override void SpecialAttack()
    {
        HookSpecialAction();
    }

    private void HookSpecialAction()
    {
        if (hookThrown == false)
        {
            Vector2 v = Globals.DegreeToVector2(pivot.rotation.eulerAngles.z - 90);
            ThrowHook(v);
        }
        else if (hookThrown == true)
        {
            RetrieveHook();
        }
    }

    private void ThrowHook(Vector2 direction)
    {
        playerInput.currentActionMap.Disable();
        hookInput.currentActionMap.Enable();       

        hookThrown = true;
        hook.gameObject.SetActive(true);
        hookBody.SetActive(false);

        hook.Throw(direction, throwStrenght);

        // Set animation trigger   
        playerAnimator.SetBool("HookThrown", true);
    }

    private void RetrieveHook()
    {       
        // Set animation trigger
        playerAnimator.SetBool("HookThrown", false);

        // Reel in hook (start minigame here I guess..?)
        ReelTimer = 0.4f;
        isReeling = true;
    }

    private void HookFinishedReeling()
    {
        hookInput.currentActionMap.Disable();
        playerInput.currentActionMap.Enable();


        // (end minigame here I guess..?)
        isReeling = false;
        hookThrown = false;
        hook.gameObject.SetActive(false);
        hookBody.SetActive(true);
    }
}
