using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{ 
    // Player components
    public Animator animator;
    public IWeapon currentWeapon;
    public List<IWeapon> equippedWeapons;
    public Transform pivot;

    private Rigidbody2D body;  
    private Vector2 moveInput;
    private Vector2 currentVelocity;

    // Movement variables
    public float speed = 5;
    public float acceleration = 100;

    private void Awake()
    {
        // Get components
        body = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        Move(moveInput);
    }

    void Update()
    {
        
    }

    // Fired by InputSystem
    public void OnScroll(InputValue value)
    {
        float val = value.Get<float>();

        if(val > 0)
        {
            // Scroll Up
        }
        else if (val < 0)
        {
            // Scroll Down
        }
    }

    // Fired by InputSystem
    public void OnAttack(InputValue value)
    {

    }

    // Fired by InputSystem
    public void OnSpecialAttack(InputValue value)
    {

    }

    // Fired by InputSystem
    public void OnInteract(InputValue value)
    {
        
    }

    // Fired by InputSystem
    public void OnMove(InputValue value)
    {
        // Retrieve value and affect moveInput
        moveInput = value.Get<Vector2>();
        if (moveInput.magnitude < 0.2)
            moveInput = Vector2.zero;     
    }

    // Move the character
    private void Move(Vector2 direction)
    {
        // Move it toward the input order
        Vector2 velocity = Vector2.MoveTowards(body.velocity, moveInput * speed, acceleration * Time.fixedDeltaTime);
        body.velocity = velocity;

        // Rotation
        if (body.velocity.magnitude > 0)
        {
            float angle = Mathf.Atan2(body.velocity.x, body.velocity.y) * Mathf.Rad2Deg;
            pivot.rotation = (Quaternion.RotateTowards(pivot.rotation, Quaternion.AngleAxis(angle, Vector3.back), 3600 * Time.fixedDeltaTime));         
        }

        animator.SetFloat("Speed", moveInput.magnitude);

        float pivotAngle = pivot.eulerAngles.z;
        Debug.Log(pivotAngle);

        if (pivotAngle >= 225 && pivotAngle <= 315) { /* looking right */ animator.SetInteger("Angle", 270); }
        else if (pivotAngle > 135 && pivotAngle < 225) { /* looking down */ animator.SetInteger("Angle", 180); }
        else if (pivotAngle >= 45 && pivotAngle <= 135) { /* looking left */ animator.SetInteger("Angle", 90); }
        else if (pivotAngle > 315 || pivotAngle < 45) { /* looking up */ animator.SetInteger("Angle", 0); }
    }

    private void SwitchWeapon()
    {

    }

}
