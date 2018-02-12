using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour {

	public Text healthTot;
	public Image healthBar;
    public Inventory_real inv;

	public Text backUpammo;
	public Image[] inClip = new Image[6];
	public int leftInClip = 5;
	public int spareAmmo = 24;
	public bool isFullClip = true;
	bool isInvoking = false;

	public AudioClip reloading;
	public AudioClip firing;
	public AudioSource audio;

	public float health = 100.0f;

	// Use this for initialization
	void Start () {
        //inv = GameObject.Find("InventoryUI").GetComponent<Inventory_real>();
        //spareAmmo = inv.GetAmountOfItem(2);
    }
	
	// Update is called once per frame
	void Update () {

		if (leftInClip == 5 || spareAmmo == 0) {
			isFullClip = true;
		}

		printHealth ();
		printAmmo ();
		healthBar.fillAmount = health / 100f;
	}


	void printHealth(){

        healthTot.text = health + "/100";
	
	}

	public bool shotBullet(){
		audio.clip = firing;
		if (leftInClip != 5) {
			isFullClip = false;
		}
		if (leftInClip >= 0) {
			
			inClip [leftInClip].fillAmount = 0;
			leftInClip--;
			return true;
		}
        else {
			return false;	
		}
	}


	public void printAmmo(){
        backUpammo.text = "|" + spareAmmo;
	}
		

}
