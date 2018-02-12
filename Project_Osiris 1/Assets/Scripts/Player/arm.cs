using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arm : MonoBehaviour {


	public bool isReloading = false;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {



		Vector2 target = new Vector2 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 dir = target - myPos;
        dir.Normalize();

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (Input.GetButton("Fire2"))
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        }
        else {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
			



    }
}
