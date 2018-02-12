using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryController : MonoBehaviour {

    public GameObject invc;
    private GameObject canvas;
    private Image dim;
    private Color c;
    public Tooltip tooltip;
    public bool invAct;

	// Use this for initialization
	void Start () {
        invc.SetActive(false);
        canvas = GameObject.Find("UI");
        dim = canvas.GetComponent<Image>();
        c = dim.color;
        c.a = 0;
        dim.color = c;
        invAct = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab) && !invAct)
        {
			invc.SetActive(true);
            invAct = true;
            c.a = 0.8f;
            dim.color = c;
        }
        else if(Input.GetKeyDown(KeyCode.Tab) && invAct) {
            invc.SetActive(false);
            invAct = false;
            tooltip.Deactivate();
            c.a = 0;
            dim.color = c;
        }
    }
}
