using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lootable : MonoBehaviour, IInteractive
{
    public List<WeightedObject> lootTable = new List<WeightedObject>();

    public uint lootCount = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(Player player)
    {
        player.Inventory.Add(GenerateLoot().objectPrefab);
        if (--lootCount <= 0) {
            Destroy(gameObject);
        }
    }


    public WeightedObject GenerateLoot()
    {
        return WOManager.Draw(lootTable);
    }
}
