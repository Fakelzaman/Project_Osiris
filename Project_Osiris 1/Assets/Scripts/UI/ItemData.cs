using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemData : MonoBehaviour, IBeginDragHandler,IDragHandler,IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {

    public Item item;
    public int amount;
    public int slot;

    private Inventory_real inv;
    private Tooltip tooltip;
    private Vector2 offset;

    void Start()
    {
        inv = GameObject.Find("InventoryUI").GetComponent<Inventory_real>();
        tooltip = inv.GetComponent<Tooltip>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            this.transform.position = eventData.position - offset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(inv.slots[slot].transform);
        this.transform.position = inv.slots[slot].transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (slot > 9) // Disables tooltip for items in the hotbar(TODO)
        {
            tooltip.Activate(item);
        }
        this.transform.localScale = new Vector3(1.2f, 1.2f, 1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Deactivate();
        this.transform.localScale = new Vector3(1, 1, 1);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (item != null)
        {
            offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
            //this.transform.SetParent(this.transform.root);
            this.transform.position = eventData.position - offset;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }
}
