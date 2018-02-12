using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {


	private Vector2 waypointActual;
	private int wpEl;
	private Rigidbody2D rb;
	public bool isInteract = false;
	public float health = 100;

	public float velo = 10.0f;
	private ArrayList waypointList = new ArrayList ();

	private bool walk = true;
	private bool isWaiting = false;

	AudioSource audio;
	public AudioClip Ow;
	public AudioClip death;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		GameObject[] waypoints = GameObject.FindGameObjectsWithTag ("Waypoint");
		foreach (GameObject waypoint in waypoints) {
			waypointList.Add (new Vector2(waypoint.transform.parent.position.x, waypoint.transform.parent.position.y));
		
		}
		waypointActual = new Vector2(transform.position.x, transform.position.y);
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (health < 0f) {
			if (!audio.isPlaying) {
				audio.clip = death;
				audio.Play ();
				StartCoroutine (Die ());
			} 
		}

		checkDistance();
		if (walk)
			Walk ();
		else if(!walk && !isWaiting && !isInteract ){
			StartCoroutine(Wait());
		}

	}

	void Walk(){
		//animation.CrossFade ("walk");
		moveNPCTowards (waypointActual);
	}
		

	void checkDistance(){
		if (Vector2.Distance (new Vector2 (transform.position.x, transform.position.y), new Vector2(waypointActual.x, waypointActual.y)) < 2.0f) {
			rb.velocity = Vector2.zero;
			selectNewWaypoint ();

		}
	}

	void selectNewWaypoint(){
		if (++wpEl == waypointList.Count) {
			wpEl = 0;
		}
		waypointActual = (Vector2) waypointList[wpEl];

		walk = false;
	}

	void moveNPCTowards(Vector2 waypointActual){

		Vector2 dir = new Vector2(waypointActual.x - transform.position.x, waypointActual.y - transform.position.y);


			rb.velocity = dir * velo;
		
	}

	IEnumerator Wait(){
		isWaiting = true;
		//animation.CrossFade ("idle");
		yield return new WaitForSeconds (5.0f);
		walk = true;
		isWaiting = false;
	}

	IEnumerator Die(){
		yield return new WaitForSeconds (2.0f);
		Destroy (this.gameObject);
	}

	public void loseHealth(float dmg){
		health = health - dmg;
		if (health > 0) {
			audio.clip = Ow;
			audio.Play ();
		} 
	}



}
