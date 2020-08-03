using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWeb : MonoBehaviour
{
    public Spider spider;

    // Start is called before the first frame update
    void Awake()
    {
        spider = transform.parent.Find("Controls").GetComponent<Spider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (spider.webActive)
        {
            bool triggered = false;

            if (collision.GetComponent<Player>() != null)
                triggered = true;
            else if (collision.GetComponent<Creature>() != null)
            {
                Creature targetCreature = collision.GetComponent<Creature>();
                if (targetCreature != null && targetCreature.creatureType == Creature.CreatureType.Small && targetCreature.transform.Find("Controls")?.GetComponent<Spider>() == null)
                    triggered = true;
            }

            if (triggered)
            {
                CrowdControl prey = collision.GetComponent<CrowdControl>();
                if (prey != null)
                {
                    prey.Stunned = true;
                    prey.stunTimer = 3;
                }
                spider.PreyDetected(collision.gameObject);
                spider.webActive = false;
            }
        }
    }
}
