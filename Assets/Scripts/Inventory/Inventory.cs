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
    public GameObject itemPrefab;

    public float buttonHeight = 30;

    private int numberOfCategories;

    private Transform inventory;
    private Transform navigationTabs;
    private Transform scrollBar;

    public float spaceBetweenInventoryAndScrollBar = 0;

    public uint numberOfColumns = 12;
    public uint defaultNumberofRows = 6;
    public float spaceBetweenColumns = 5;
    public float spaceBetweenRows = 5;
    public float border = 10;

    void Awake()
    {
        inventory = transform.GetChild(0);
        navigationTabs = transform.GetChild(1);
        scrollBar = transform.GetChild(2);
    }

    // Start is called before the first frame update
    void Start()
    {
        ResizePanelForItemGrid(inventory);

        RectTransform scrollBarRectTransform = scrollBar.GetComponent<RectTransform>();
        RectTransform inventoryRectTransform = inventory.GetComponent<RectTransform>();
        scrollBarRectTransform.localPosition = new Vector2(inventoryRectTransform.rect.width / 2 + spaceBetweenInventoryAndScrollBar, 0);
        scrollBarRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inventoryRectTransform.rect.height);

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

        Item itemInstance;
        if (itemList.Exists(i => i.id.CompareTo(item.id) == 0)) {
        //if (false) {
            itemInstance = itemList.Find(i => i.id.CompareTo(item.id) == 0);
            itemInstance.Quantity++;
        } else {
            itemInstance = Instantiate(item);
            itemInstance.Quantity = 1;
            itemInstance.GetComponent<RectTransform>().localPosition = ComputePositionOfItem(itemList.Count, (int)item.category);
            itemList.Add(itemInstance);
            itemInstance.transform.SetParent(inventory.GetChild((int)item.category), false);
        }

        itemInstance.UpdateText();
    }

    public void Remove(Item item)
    {
        List<Item> itemList = itemLists[(int)item.category];

        if (itemList.Exists(i => i.id.CompareTo(item.id) == 0)) {
            Item itemInstance = itemList.Find(i => i.id.CompareTo(item.id) == 0);
            if (--itemInstance.Quantity <= 0) {
                itemList.Remove(itemInstance);
                Destroy(itemInstance.gameObject);
                RefreshPositionOfItems(itemInstance.category);
            } else {
                itemInstance.UpdateText();
            }
        } else {
            Debug.LogWarning("Tried to remove an item not existing in an item list : " + item.id);
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

        GameObject subInventory = Instantiate(subInventoryPrefab, inventory);
        subInventory.name = enumName;

        ResizePanelForItemGrid(subInventory.transform);
    }

    private void CreateTabButton(int index)
    {
        GameObject invTab = Instantiate(inventoryTabPrefab, navigationTabs, false);
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
        invTab.GetComponent<RectTransform>().localPosition = new Vector2(buttonPosX + buttonWidth * index, buttonPosY);
        // Set object's name and label
        invTab.name = enumName + "Button";
        invTab.GetComponentInChildren<Text>().text = enumName;
        // Set on click event
        invTab.GetComponent<Button>().onClick.AddListener(() => ChangeActiveSubInventory(index));
    }

    private Vector2 ComputePositionOfItem(int positionInList, int categoryIndex)
    {
        Rect itemRect= itemPrefab.GetComponent<RectTransform>().rect;
        RectTransform inventoryRectTransform = inventory.GetChild(categoryIndex).GetComponent<RectTransform>();

        float x = border + (spaceBetweenColumns + itemRect.width) * (positionInList % numberOfColumns);
        float y = border + (spaceBetweenRows + itemRect.height) * (int)(positionInList / numberOfColumns);

        float minY = border + (itemRect.height + spaceBetweenRows) * (defaultNumberofRows - 1);

        // Increase size of inventory if the current height is not enough for a new object
        // Or reduce the size if its too tall but not more than the minimum size
        if (inventoryRectTransform.rect.height - border < y + itemRect.height) {
            inventoryRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,
                inventoryRectTransform.rect.height + itemRect.height + spaceBetweenRows
                //y + border + itemRect.height + spaceBetweenRows
            );
        } else if (positionInList % numberOfColumns == numberOfColumns - 1 && y >= minY
            && inventoryRectTransform.rect.height - border - spaceBetweenRows > y + itemRect.height
        ) {
            inventoryRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,
                inventoryRectTransform.rect.height - (itemRect.height + spaceBetweenRows)
                //y + border + itemRect.height + (y != minY ? spaceBetweenRows : 0)
            );
        }
        y *= -1;

        return new Vector2(x, y);
    }

    private void ResizePanelForItemGrid(Transform panel)
    {
        panel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,
            border * 2
            + itemPrefab.GetComponent<RectTransform>().rect.height * numberOfColumns
            + spaceBetweenColumns * (numberOfColumns - 1)
        );

        float height = border * 2 + (defaultNumberofRows <= 0 ? 0 :
            itemPrefab.GetComponent<RectTransform>().rect.height * defaultNumberofRows
            + spaceBetweenRows * (defaultNumberofRows - 1)
        );
        panel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }

    private void RefreshPositionOfItems(Item.Category category)
    {
        RefreshPositionOfItems(itemLists[(int)category]);
    }

    private void RefreshPositionOfItems(List<Item> itemList)
    {
        for (int i = 0; i < itemList.Count; i++) {
            Item item = itemList[i];
            item.GetComponent<RectTransform>().localPosition = ComputePositionOfItem(i, (int)item.category);
        }
    }
}
