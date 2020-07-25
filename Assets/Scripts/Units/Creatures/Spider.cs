using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public bool webActive;

    private Transform pivot;
    private Animator animator;
    private Health health;
    private FieldOfView fieldOfView;
    private Creature creature;


    void Awake()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        fieldOfView = GetComponent<FieldOfView>();
        creature = GetComponent<Creature>();
        pivot = transform.Find("Pivot");
    }

    // Start is called before the first frame update
    private void Start()
    {
        health.onAttack += OnAttackReceived;

        if (!webActive)
            animator.SetTrigger("Skip Web");
    }

    // Update is called once per frame
    void Update()
    {
        if (creature.aggro == null)
        {
            foreach (Transform target in fieldOfView.visibleTargets)
            {
                if (target.GetComponent<Player>())
                    creature.aggro = target.gameObject;
            }
        }
        else
        {
            creature.AttackAggro();
        }
    }

    public void PreyDetected(GameObject source)
    {
        animator.SetTrigger("Prey Detected");
        creature.aggro = source;
    }

    private void OnAttackReceived(GameObject source)
    {
        creature.aggro = source;
    }
}
