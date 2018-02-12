using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	public static GameMaster gm;

	// Use this for initialization
	void Start () {
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GM").GetComponent<GameMaster> ();
		}
	}
	
	// Update is called once per frame
	void Update () {



	}

	public static void KillCharacter(Enemy character){
		Destroy (character.gameObject);
	
	}

}
