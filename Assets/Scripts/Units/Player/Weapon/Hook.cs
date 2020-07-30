using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public Player player;
    private Rigidbody2D body;
    private Collider2D coll;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Throw(Vector2 direction, float force)
    {
        body.transform.position = player.transform.position;
        body.velocity = Vector2.zero;
        body.AddForce(direction.normalized * force, ForceMode2D.Impulse);        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Hook touched a creature
        if(collision.gameObject.GetComponent<Creature>() != null)
        {

        }
        // Hook touched a wall
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {

        }
    }
}
