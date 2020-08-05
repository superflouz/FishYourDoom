using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lootable : MonoBehaviour, IInteractive
{
    public List<WeightedObject> lootTable = new List<WeightedObject>();

    public uint lootCount = 1;
    public float timeToLoot = 1;

    private LootingManager lootingManager;

    void Awake()
    {
        lootingManager = GameObject.Find("LootingManager").GetComponent<LootingManager>();
    }

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
        System.Action loot = () => {
            player.Inventory.Add(GenerateLoot().objectPrefab);
            if (--lootCount <= 0) {
                Destroy(gameObject);
            }
        };
        lootingManager.Loot(this, loot);
    }


    public WeightedObject GenerateLoot()
    {
        return WOManager.Draw(lootTable);
    }
}
