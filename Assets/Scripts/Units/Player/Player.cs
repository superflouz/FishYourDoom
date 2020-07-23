using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Player components
    private Health health;
    private Stats stats;
    private Animator animator;
    private Rigidbody2D body;

    private Inventory inventory;
    public Inventory Inventory
    {
        get { return inventory; }
    }

    public Stats Stats
    {
        get { return stats; }
        set { stats = value; }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // Get components
        health = GetComponent<Health>();
        stats = GetComponent<Stats>();
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        inventory = GameObject.Find("InventoryUI").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
