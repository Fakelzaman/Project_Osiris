using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour {

	void Update () {
		float size = Random.Range (0.2f, 0.3f);
	    transform.localScale = new Vector3 (size, size, size);
		Destroy (this.gameObject, 0.02f);
	}
}
