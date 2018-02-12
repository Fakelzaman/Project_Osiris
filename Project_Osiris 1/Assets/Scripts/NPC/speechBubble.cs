using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class speechBubble : MonoBehaviour {

	private int maxBob = 50;
	private int bob = 0;
	private NPC npc;

	public Canvas chat;

	// Use this for initialization
	void Start () {
		
		chat.gameObject.SetActive (false);
		npc = GetComponentInParent<NPC>();
	}
	
	// Update is called once per frame
	void Update () {

		if (bob < maxBob) {
			transform.Translate (0.0f, 0.005f, 0.0f);
			bob++;
			if (bob == maxBob) {
				bob = 100;
			}
		} else {
			transform.Translate (0.0f, -0.005f, 0.0f);
			bob--;
			if (bob == maxBob) {
				bob = 0;
			}
		}
	}

	private void OnTriggerStay2D(Collider2D col){

		if (Input.GetKey (KeyCode.F)) {
			chat.gameObject.SetActive (true);
			npc.isInteract = !npc.isInteract;
		}
	
	
	}
	private void OnTriggerExit2D(Collider2D col){
	
		npc.isInteract = false;
		chat.gameObject.SetActive (false);
	
	}
}
