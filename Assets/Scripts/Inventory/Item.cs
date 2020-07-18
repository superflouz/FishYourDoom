using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Category : int
    {
        Consumable = 0,
        Weapon = 1
    }

    public Category category;

    public string id;

    private uint quantity;
    public uint Quantity
    {
        get { return quantity; }
        set { quantity = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
