using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningCreature : MonoBehaviour
{
    private Creature creature;
    private FieldOfView fieldOfView;
    private Vector2 runDirection = Vector2.zero;

    public float runningTime;
    private float runningTimer;

    private void Awake()
    {
        creature = transform.parent.GetComponent<Creature>();
        fieldOfView = transform.parent.GetComponent<FieldOfView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        creature.wanderingCenter = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (runningTimer > 0)
        {
            runningTimer -= Time.deltaTime;
            if (runningTimer <= 0)
            {
                runDirection = Vector2.zero;
            }
            creature.Move(runDirection.normalized, 1);
        }
        else
        {
            foreach (Transform target in fieldOfView.visibleTargets)
            {
                Creature targetCreature = target.GetComponent<Creature>();
                if (targetCreature != null && targetCreature.creatureEating == Creature.CreatureEating.Carnivore)
                    runDirection += ((Vector2)transform.position - (Vector2)target.position).normalized;
                else if (target.GetComponent<Player>() != null)
                    runDirection += ((Vector2)transform.position - (Vector2)target.position).normalized;
            }
            if (runDirection != Vector2.zero)
            {
                runningTimer = runningTime;
                creature.Move(runDirection.normalized, 1);
            }

            else
                creature.Wander();
        }

    }
}
