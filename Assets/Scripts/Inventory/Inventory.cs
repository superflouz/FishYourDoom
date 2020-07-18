using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private static readonly int numberOfEnums = Enum.GetValues(typeof(Item.Category)).Length;
    public List<Item>[] itemLists = new List<Item>[numberOfEnums];
    public Transform[] childLists = new Transform[numberOfEnums];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < itemLists.Length; i++) {
            itemLists[i] = new List<Item>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add(Item item)
    {
        List<Item> itemList = itemLists[(int)item.category];

        if (itemList.Exists(i => i.id.CompareTo(item.id) == 0)) {
            itemList.Find(i => i.id.CompareTo(item.id) == 0).Quantity++;
        } else {
            Item newInstance = Instantiate(item);
            newInstance.Quantity = 1;
            itemList.Add(newInstance);
            newInstance.transform.parent = childLists[(int)item.category];
        }
    }
}
