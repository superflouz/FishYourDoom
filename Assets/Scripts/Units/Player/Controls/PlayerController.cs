using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{ 
    public Animator animator;
    public FishingRod fishingRod;

    public float speed = 5;
    public float acceleration = 100;

    private Rigidbody2D body;
    private Transform pivot;
    private Vector2 moveInput;
    private Vector2 currentVelocity;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        pivot = transform.Find("Pivot");
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
    public void OnMove(InputValue value)
    {
        // Retrieve value and affect moveInput
        moveInput = value.Get<Vector2>();
        if (moveInput.magnitude < 0.2)
            moveInput = Vector2.zero;

        /* 
        animator.SetFloat("Move X", moveInput.x);
        animator.SetFloat("Move Y", moveInput.y);
        animator.SetFloat("Speed", moveInput.magnitude);
        */
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
            pivot.rotation = (Quaternion.RotateTowards(pivot.rotation, Quaternion.AngleAxis(angle, Vector2.up), 360 * Time.fixedDeltaTime));
        }
    }
}
