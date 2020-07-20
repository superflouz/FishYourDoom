using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public Player player;
    private Rigidbody2D body;
    private Collider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    public void Throw(Vector2 direction, float force)
    {
        body.transform.position = player.transform.position;
        body.velocity = Vector2.zero;
        body.AddForce(direction.normalized * force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
