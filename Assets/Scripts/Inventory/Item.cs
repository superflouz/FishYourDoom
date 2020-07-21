using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public enum Category : int
    {
        Consumable = 0,
        Lure = 1,
        Dart = 2,
        Trap = 3,
        Ingredient = 4,
        Weapon = 5
    }

    public Category category;

    public string id;

    public Sprite itemSprite;

    private Transform background;
    private Transform item;
    private Transform text;
    private Transform border;

    [SerializeField]
    private uint quantity;
    public uint Quantity
    {
        get { return quantity; }
        set { quantity = value; }
    }

    // Start is called before the first frame update
    void Awake()
    {
        background = transform.GetChild(0);
        item = transform.GetChild(1);
        border = transform.GetChild(2);
        text = transform.GetChild(3);

        item.GetComponent<Image>().sprite = itemSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText()
    {
        text.GetComponent<Text>().text = Quantity.ToString();
    }
}
