using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Creature : MonoBehaviour
{
    public enum CreatureType
    {
        Small,
        Medium,
        Big
    }

    public CreatureType creatureType;

    public GameObject aggro;
    public float attackRange;
    public float speed;
    public float acceleration;
    public float attackSpeed;

    public Vector2 wanderingCenter;

    public float wanderingSpeedRatio;
    public float wanderingRadius;
    public float pauseTime;
    public float wanderTime;

    private Vector2 currentWanderDirection;
    private float pauseTimer;
    private float wanderTimer;

    public bool Dead { get; private set; }

    private Rigidbody2D body;
    private Transform pivot;
    private Animator animator;
    private Health health;
    private FieldOfView fieldOfView;

    private float attackTimer;


    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        fieldOfView = GetComponent<FieldOfView>();
        pivot = transform.Find("Pivot");
    }

    private void Start()
    {
        health.onDeath += OnDeath;
        pauseTimer = pauseTime;
    }

    private void Update()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    public void Wander()
    {
        if (wanderTimer > 0)
        {
            wanderTimer -= Time.deltaTime;
            if (wanderTimer <= 0)
                pauseTimer = pauseTime;

            Move(currentWanderDirection, speed * wanderingSpeedRatio);
        }

        if (pauseTimer > 0)
        {
            pauseTimer -= Time.deltaTime;
            if (pauseTimer <= 0)
            {
                Vector2 offsetWandering = Globals.DegreeToVector2(Random.Range(0f, 360f));
                offsetWandering *= wanderingRadius;
                offsetWandering += wanderingCenter;

                currentWanderDirection = (offsetWandering - (Vector2)transform.position).normalized;

                wanderTimer = wanderTime;
            }

            Move(currentWanderDirection, 0);
        }
    }

    public void Move(Vector2 direction, float magnitude)
    {
        // Move it toward the input order
        Vector2 velocity = Vector2.MoveTowards(body.velocity, direction * magnitude, acceleration * Time.fixedDeltaTime);
        body.velocity = velocity;

        animator.SetFloat("Speed", magnitude);


        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        pivot.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

        float pivotAngle = pivot.eulerAngles.z;

        if (pivotAngle >= 225 && pivotAngle <= 315)
        { animator.SetInteger("Angle", 270); }
        else if (pivotAngle > 135 && pivotAngle < 225)
        { animator.SetInteger("Angle", 180); }
        else if (pivotAngle >= 45 && pivotAngle <= 135)
        { animator.SetInteger("Angle", 90); }
        else if (pivotAngle > 315 || pivotAngle < 45)
        { animator.SetInteger("Angle", 0); }
    }

    public void AttackAggro()
    {
        if (aggro != null)
        {
            Creature aggroCreature = aggro.GetComponent<Creature>();
            if (aggroCreature)
            {
                if (aggroCreature.Dead)
                {
                    aggro = null;
                }
            }
        }

        if (aggro != null && attackTimer <= 0)
        {
            if ((aggro.transform.position - transform.position).magnitude > attackRange)
            {
                Move((aggro.transform.position - transform.position).normalized, speed);
            }
            else
            {
                Move((aggro.transform.position - transform.position).normalized, 0);
                animator.SetTrigger("Attack");
                attackTimer = 1 / attackSpeed;
            }
        }
    }

    public void Attack()
    {
        if (attackTimer <= 0)
        {
            animator.SetTrigger("Attack");
            attackTimer = 1 / attackSpeed;
        }
    }


    private void OnDeath()
    {
        Dead = true;
        animator.SetTrigger("Die");
    }
}
