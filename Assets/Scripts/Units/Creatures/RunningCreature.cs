using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningCreature : MonoBehaviour
{
    private Creature creature;
    private FieldOfView fieldOfView;



    private void Awake()
    {
        creature = GetComponent<Creature>();
        fieldOfView = GetComponent<FieldOfView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        creature.wanderingCenter = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        creature.Wander();
    }
}
