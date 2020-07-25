using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpider : MonoBehaviour
{
    public GameObject source;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (!collision.GetComponent<Spider>())
        //{
            Health health = collision.GetComponent<Health>();
            if (health != null)
            {
                health.ModifyHealth(-damage, source);
            }
        //}
    }
}
