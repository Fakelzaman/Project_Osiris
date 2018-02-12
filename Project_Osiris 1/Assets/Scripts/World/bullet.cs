using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

    private Vector2 currPos;
    public float initSpeed = 0.001f;
    private Rigidbody2D bulletRig;


    // Use this for initialization
    void Start () {


        Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x,Screen.height - Input.mousePosition.y));
        Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 dir = target - myPos;
        dir = new Vector2(dir.x, -dir.y -0.4f);
        dir.Normalize(); //Direction from player to mouse pointer.
        

        bulletRig = GetComponentInChildren<Rigidbody2D>();
        bulletRig.velocity = dir * initSpeed;

		RaycastHit2D bulletHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), dir, Mathf.Infinity);
		Debug.DrawRay(transform.position, dir, Color.green);

		if (bulletHit.collider != null) {

			print (bulletHit.collider);
		}


		if (bulletHit.collider.tag == "Enemy") {
			
			Destroy (this.gameObject);
		}
		
    }
	
	// Update is called once per frame
	void Update () {
        currPos = transform.position;
    }


    IEnumerator destroy() {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }


}
