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
        // totalWeight could be stored globally and computed at Awake time.
        // The current local usage allows for dynamic item pool.
        uint totalWeight = 0;
        foreach (WeightedObject loot in lootTable) {
            totalWeight += loot.weight;
        }

        uint randomValue = (uint)Random.Range(0, (int)totalWeight);
        uint currentWeightCheck = 0;
        WeightedObject returnLoot = lootTable[0];

        foreach (WeightedObject loot in lootTable) {
            currentWeightCheck += loot.weight;

            if (currentWeightCheck > randomValue) {
                returnLoot = loot;
                break;
            }
        }
        return returnLoot;
    }
}
