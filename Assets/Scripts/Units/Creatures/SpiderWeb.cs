using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWeb : MonoBehaviour
{
    public Spider spider;
    private bool webActive;

    // Start is called before the first frame update
    void Start()
    {
        webActive = transform.parent.GetComponent<Spider>().webActive;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (webActive)
        {
            // Apply root to collider
            spider.PreyDetected(collision.gameObject);
            webActive = false;
        }

    }
}
