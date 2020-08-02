using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpider : MonoBehaviour
{
    private GameObject source;
    public float damage;

    // Start is called before the first frame update
    void Awake()
    {
        source = transform.parent.transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<Spider>())
        {
            Health health = collision.GetComponent<Health>();
            if (health != null)
            {
                health.ModifyHealth(-damage, source);
            }
        }
    }
}
