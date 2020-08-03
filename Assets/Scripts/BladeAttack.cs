using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeAttack : MonoBehaviour
{
    private GameObject source;
    public float damage;

    // Start is called before the first frame update
    void Awake()
    {
        source = transform.parent.parent.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health health = collision.GetComponent<Health>();
        if (health != null)
        {
            health.ModifyHealth(-damage, source);
        }
    }
}
