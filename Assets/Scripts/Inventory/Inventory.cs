using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Item>[] itemLists;
    public GameObject subInventoryPrefab;
    public GameObject inventoryTabPrefab;

    public float buttonHeight = 30;

    private int numberOfCategories;

    private Transform inventory;
    private Transform navigationTabs;

    // Start is called before the first frame update
    void Start()
    {
        inventory = transform.GetChild(0);
        navigationTabs = transform.GetChild(1);

        numberOfCategories = Enum.GetValues(typeof(Item.Category)).Length;
        itemLists = new List<Item>[numberOfCategories];

        for (int i = 0; i < numberOfCategories; i++) {
            itemLists[i] = new List<Item>();

            CreateSubInventory(i);
            CreateTabButton(i);
        }

        // Set the default sub inventory
        int indexDefaultCategory = (int)Item.Category.Consumable;
        ChangeActiveSubInventory(indexDefaultCategory);
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
            newInstance.transform.parent = inventory.GetChild((int)item.category);
            itemList.Add(newInstance);
        }
    }

    public void ChangeActiveSubInventory(int categoryIndex)
    {
        HideAllSubInventory();
        inventory.GetComponent<ScrollRect>().content = inventory.GetChild(categoryIndex).GetComponent<RectTransform>();
        inventory.GetChild(categoryIndex).gameObject.SetActive(true);
    }

    public void ChangeActiveSubInventory(Item.Category category)
    {
        ChangeActiveSubInventory((int)category);
    }

    public void HideAllSubInventory()
    {
        for (int i = 0; i < inventory.childCount; i++) {
            inventory.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void CreateSubInventory(int index)
    {
        string enumName = Enum.GetName(typeof(Item.Category), index);

        GameObject subInventory = Instantiate(subInventoryPrefab);
        subInventory.name = enumName;
        subInventory.transform.SetParent(inventory, false);
    }

    private void CreateTabButton(int index)
    {
        GameObject invTab = Instantiate(inventoryTabPrefab);
        //int currentLoopIndex = index; Previously required when used to define the onClick event inside of a loop with a lambda. Becuase of closure, a reference on the loop index was kept and the value used was one bigger than the conntrol value.
        string enumName = Enum.GetName(typeof(Item.Category), index);

        Rect invRect = inventory.GetComponent<RectTransform>().rect;
        Vector2 localPos = inventory.GetComponent<RectTransform>().localPosition;
        float buttonWidth = invRect.width / numberOfCategories;
        float buttonPosY = invRect.height / 2 + localPos.y;
        float buttonPosX = invRect.width / 2 * -1 + localPos.x;

        // Set dimensions
        invTab.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, buttonWidth);
        invTab.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, buttonHeight);
        // Set position
        invTab.GetComponent<RectTransform>().Translate(buttonPosX + buttonWidth * index, buttonPosY, 0);
        // Set object's name and label
        invTab.name = enumName + "Button";
        invTab.GetComponentInChildren<Text>().text = enumName;
        // Set on click event
        invTab.GetComponent<Button>().onClick.AddListener(() => ChangeActiveSubInventory(index));
        // Set parent
        invTab.transform.SetParent(navigationTabs, false);
    }
}
