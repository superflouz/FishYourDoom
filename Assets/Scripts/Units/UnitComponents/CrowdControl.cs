using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdControl : MonoBehaviour
{
    public float stunTimer;
    private ICrowdControllable creature;
    private Animator animator;

    Transform stun;

    private bool stunned;
    public bool Stunned
    {
        get
        {
            return stunned;
        }
        set
        {
            if (value)
            {
                stun.gameObject.SetActive(true);
                animator.SetTrigger("Stunned");
            }
            else
            {
                stun.gameObject.SetActive(false);
                animator.SetTrigger("Unstunned");
            }
            stunned = value;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        creature = GetComponent<ICrowdControllable>();
        animator = GetComponent<Animator>();
        stun = transform.Find("Stun");
    }

    // Update is called once per frame
    void Update()
    {
        if (creature != null)
        {
            if (stunTimer > 0)
            {
                stunTimer -= Time.deltaTime;
                if (stunTimer <= 0)
                {
                    creature.Stunned = false;
                }
            }
        }
    }
}
