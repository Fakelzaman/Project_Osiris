using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory_real : MonoBehaviour
{
    GameObject inventoryPanel;
    GameObject hotbar;
    GameObject slotPanel;
    GameObject slotPanelHotbar;
    ItemDatabase database;
    public GameObject inventorySlot;
    public GameObject inventoryItem;

    public int slotAmount = 20;
    public int slotAmountHotbar = 9;
    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    void Start()
    {
        database = GetComponent<ItemDatabase>();

        inventoryPanel = GameObject.Find("Inventory Panel");

        hotbar = GameObject.Find("Hotbar");

        slotPanel = inventoryPanel.transform.Find("Slot Panel").gameObject;

        slotPanelHotbar = hotbar.transform.Find("Slot Panel Hotbar").gameObject;

        for (int i = 0; i < slotAmountHotbar; i++) {
            items.Add(new Item());
            slots.Add(Instantiate(inventorySlot));
            slots[i].GetComponent<inventorySlot>().id = i;
            slots[i].transform.SetParent(slotPanelHotbar.transform);
        }

        for (int i = slotAmountHotbar; i < (slotAmountHotbar+slotAmount); i++)
        {
            items.Add(new Item());
            slots.Add(Instantiate(inventorySlot));
            slots[i].GetComponent<inventorySlot>().id = i;
            slots[i].transform.SetParent(slotPanel.transform);
        }

        AddItem(0);
        AddItem(1);
        AddItem(1);
        AddItem(1);
        AddItem(2);
        AddItem(2);
        AddItem(2);
        AddItem(2);
        AddItem(2);
        AddItem(2);
        AddItem(2);
        AddItem(2);
        AddItem(2);
        AddItem(2);
        AddItem(2);
        AddItem(2);
        AddItem(2);
        AddItem(2);
        AddItem(2);


    }

    public void AddItem(int id)
    {
        Item itemToAdd = database.FetchItemById(id);

        if (itemToAdd.Stackable && CheckIfItemIsInInventory(itemToAdd))
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == id)
                {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == -1)
                {
                    items[i] = itemToAdd;
                    GameObject itemObj = Instantiate(inventoryItem);
                    itemObj.GetComponent<ItemData>().item = itemToAdd;
                    itemObj.GetComponent<ItemData>().amount = 1;
                    itemObj.GetComponent<ItemData>().slot = i;
                    itemObj.transform.SetParent(slots[i].transform);
                    itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
                    //itemObj.transform.position = Vector2.zero;
                    itemObj.transform.position = slots[i].transform.position;
                    itemObj.name = itemToAdd.Title;
                    break;

                }
            }
        }
    }

    public void DestroyItem(GameObject ItemToDestroy) {
        Destroy(ItemToDestroy);
    }

    public bool CheckIfItemIsInInventory(Item item) {
        for (int i = 0; i < items.Count; i++) {
            if (items[i].ID == item.ID) {
                return true;
            }
        }
        return false;
    }
    public int GetAmountOfItem(int id) {
        int amount = 0;
        for (int i = 0; i < items.Count; i++) {
            if (items[i].ID == id) {
                ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                amount += data.amount;
            }
        }
        return amount;
    }

    public void TakeItem(int id) {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == id)
            {
                if (slots[i].transform.childCount > 0) { 
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount--;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    if (data.amount == 0)
                    {
                        Destroy(slots[i].transform.GetChild(0).gameObject);
                        items[i] = new Item();
                    }
                    break;
                }
            }
        }

    }

    public void TakeItems(int id, int amount = 1) {
        for (int i = 0; i < amount; i++) {
            TakeItem(id);
        }
    }

}
