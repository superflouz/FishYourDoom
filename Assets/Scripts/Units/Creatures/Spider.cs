using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public bool webActive;
    public GameObject aggro;
    public float attackRange;
    public float speed;
    public float acceleration;
    public float attackSpeed;

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
        health.onAttack += OnAttackReceived;
        health.onDeath += OnDeath;

        if (!webActive)
            animator.SetTrigger("Skip Web");
    }

    // Update is called once per frame
    void Update()
    {
        if (aggro != null && attackTimer <= 0)
        {
            if ((aggro.transform.position - transform.position).magnitude > attackRange)
            {
                Move((aggro.transform.position - transform.position).normalized, speed);
            }
            else
            {
                Move((aggro.transform.position - transform.position).normalized, 0);
                animator.SetBool("Attack", true);
                attackTimer = 1 / attackSpeed;
            }
        }
        else if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                animator.SetBool("Attack", false);
            }
        }
        else if (aggro == null)
        {
            foreach (Transform target in fieldOfView.visibleTargets)
            {
                if (target.GetComponent<Player>())
                    aggro = target.gameObject;
            }
        }
    }

    private void Move(Vector2 direction, float magnitude)
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

    public void PreyDetected(GameObject source)
    {
        animator.SetTrigger("Prey Detected");
        aggro = source;
    }

    private void OnAttackReceived(GameObject source)
    {
        aggro = source;
    }

    private void OnDeath()
    {
        animator.SetInteger("Angle", 1);
        animator.SetTrigger("Die");
    }

}
