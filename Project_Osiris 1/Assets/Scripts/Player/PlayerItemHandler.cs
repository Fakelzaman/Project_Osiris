using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemHandler : MonoBehaviour {

    Inventory_real inv;

    void Start() {
        inv = GameObject.Find("InventoryUI").GetComponent<Inventory_real>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pickup")) {
            inv.AddItem(2);
            foreach (Item item in inv.items) {
                Debug.Log(item.ID);
            }
            collision.gameObject.SetActive(false);
        }
    }
}
