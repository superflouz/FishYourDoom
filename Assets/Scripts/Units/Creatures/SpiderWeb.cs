using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWeb : MonoBehaviour
{
    public Spider spider;

    // Start is called before the first frame update
    void Start()
    {
        spider = transform.parent.GetComponent<Spider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (spider.webActive)
        {
            // Apply root to collider
            spider.PreyDetected(collision.gameObject);
            spider.webActive = false;
        }
    }
}
