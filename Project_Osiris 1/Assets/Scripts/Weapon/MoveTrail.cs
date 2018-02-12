using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrail : MonoBehaviour {

	public int moveSpeed = 230;
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3(0,-1,0) * Time.deltaTime * moveSpeed);
		Destroy (this.gameObject, 1);
	}

}
