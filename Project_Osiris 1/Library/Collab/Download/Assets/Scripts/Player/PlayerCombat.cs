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
	int leftInClip = 5;
	int spareAmmo = 24;
	public bool isFullClip = true;

	public AudioClip reloading;
	public AudioClip firing;
	public AudioSource audio;

	public float health = 100.0f;

	// Use this for initialization
	void Start () {
        inv = GameObject.Find("InventoryUI").GetComponent<Inventory_real>();
        spareAmmo = inv.GetAmountOfItem(2);
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

	public bool reloadAmmo(){ 
		if (leftInClip < 5) {    
			if (spareAmmo > 0) {
				StartCoroutine (reloadTime ());		   
			}
		}

		return isFullClip;

	}

	public bool shotBullet(){
		audio.clip = firing;
		if (leftInClip != 6) {
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
        spareAmmo = inv.GetAmountOfItem(2);
        backUpammo.text = "|" + spareAmmo;
	}

	IEnumerator reloadTime(){
		
		yield return new WaitForSeconds (0.9f);
		audio.clip = reloading;
		audio.Play ();
		leftInClip++;
		inClip [leftInClip].fillAmount = 100;
        inv.TakeItems(2);
        spareAmmo = inv.GetAmountOfItem(2);

        reloadAmmo ();
	}

}
