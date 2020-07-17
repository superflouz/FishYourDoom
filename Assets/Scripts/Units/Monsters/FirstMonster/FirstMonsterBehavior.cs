using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstMonsterBehavior : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D body;
    private GameObject player;

    public float speed = 3;
    public float acceleration = 50;
    public float movementScale = 0.1f;

    private float velocity;
    private Vector2 currentVelocity;

    private GameObject target;
    public GameObject Target
    {
        get { return target; }
        set { target = value; }
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    void Update()
    {
        
    }

    private void Move(Vector2 direction)
    {
        var move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(direction.x, direction.y, 0) * movementScale;

        velocity = Mathf.Clamp01(velocity + Time.deltaTime * acceleration);
        body.velocity = Vector2.SmoothDamp(body.velocity, move * speed, ref currentVelocity, acceleration, speed);
    }

    public void Attack()
    {

    }

    public void SpecialAttack()
    {

    }

}
