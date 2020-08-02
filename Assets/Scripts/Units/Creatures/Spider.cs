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
        animator = transform.parent.GetComponent<Animator>();
        health = transform.parent.GetComponent<Health>();
        fieldOfView = transform.parent.GetComponent<FieldOfView>();
        creature = transform.parent.GetComponent<Creature>();
        pivot = transform.parent.Find("Pivot");
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
        if (!webActive)
        {
            if (creature.aggro == null)
            {
                foreach (Transform target in fieldOfView.visibleTargets)
                {
                    if (target.GetComponent<Player>() != null)
                        creature.aggro = target.gameObject;
                    else if (target.GetComponent<Creature>() != null)
                    {
                        Creature targetCreature = GetComponent<Creature>();
                        if (targetCreature != null && targetCreature.creatureType == Creature.CreatureType.Small && targetCreature.creatureEating == Creature.CreatureEating.Herbivore)
                            creature.aggro = target.gameObject;
                    }
                }
            }
            else
            {
                creature.AttackAggro();
            }
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
