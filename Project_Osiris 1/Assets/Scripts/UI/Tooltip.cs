using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {
    private string data;
    private Item item;
    private GameObject tooltip;

    private void Start()
    {
        tooltip = GameObject.Find("Tooltip");
        tooltip.SetActive(false);
    }

    private void Update()
    {
        if (tooltip.activeSelf) {
            tooltip.transform.position = Input.mousePosition;
        }
    }


    public void Activate(Item item) {
        this.item = item;
        ConstructDataString();
        tooltip.SetActive(true);
    }

    public void Deactivate()
    {
        tooltip.SetActive(false);
    }

    public void ConstructDataString() {
        data = item.getTooltip();
        tooltip.transform.GetChild(0).GetComponent<Text>().text = data;
    }
}
