using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Player components
    private Health health;
    private Animator animator;
    private Rigidbody2D body;
    
    // Start is called before the first frame update
    void Start()
    {
        // Get components
        health = GetComponent<Health>();
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
