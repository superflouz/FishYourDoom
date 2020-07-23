using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lootable : MonoBehaviour, IInteractive
{
    [System.Serializable]
    public struct Loot {
        public uint dropWeight;
        public Item itemPrefab;
    }

    public List<Loot> lootTable = new List<Loot>();

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
        player.Inventory.Add(GenerateLoot().itemPrefab);
        Destroy(gameObject);
    }


    public Loot GenerateLoot()
    {
        // totalWeight could be stored globally and computed at Awake time.
        // The current local usage allows for dynamic item pool.
        uint totalWeight = 0;
        foreach (Loot loot in lootTable) {
            totalWeight += loot.dropWeight;
        }

        uint randomValue = (uint)Random.Range(0, (int)totalWeight);
        uint currentWeightCheck = 0;
        Loot returnLoot = lootTable[0];

        foreach (Loot loot in lootTable) {
            currentWeightCheck += loot.dropWeight;

            if (currentWeightCheck > randomValue) {
                returnLoot = loot;
                break;
            }
        }
        return returnLoot;
    }
}
