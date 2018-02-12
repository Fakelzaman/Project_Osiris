using Pathfinding;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Seeker))]
public class EnemyAI : MonoBehaviour {

	//What to chase
	public Transform target;

	//Time per second will update path
	public float updateRate = 2f;

	//caching
	private Seeker seeker;
	private Rigidbody2D rb;

	//The calculated path
	public Path path;

	//Ai Speed per second
	public float speed = 300f;
	public ForceMode2D fMode;

	[HideInInspector]
	public bool pathIsEnded = false;

	//The max distance from AI to waypoint to continue
	public float nextWaypointDistance = 3f;

	//Current waypoint going
	private int currentWaypoint = 0;

	private bool searchingForPlayer = false;

	void Start(){
		seeker = GetComponent<Seeker> ();
		rb = GetComponent<Rigidbody2D> ();

		if (target == null) {
			if (!searchingForPlayer) {
				searchingForPlayer = true;
				StartCoroutine (SearchForPlayer());
			}
			return;
		
		}

		//start a new path to teh target position and returns result to OnPathComplete()
		seeker.StartPath (transform.position, target.position, OnPathComplete);

		StartCoroutine (UpdatePath());
	}

	IEnumerator SearchForPlayer(){
		GameObject sResult = GameObject.FindGameObjectWithTag ("Player");
		if (sResult == null) {
			yield return new WaitForSeconds (1f);
			StartCoroutine (SearchForPlayer ());
		} else {
			searchingForPlayer = false;
			target = sResult.transform;
			StartCoroutine (UpdatePath ());
			yield return false; 
		}
	}

	IEnumerator UpdatePath(){
		if (target == null) {
			if (!searchingForPlayer) {
				searchingForPlayer = true;
				StartCoroutine (SearchForPlayer());
			}
			yield return false;
		}

		seeker.StartPath (transform.position, target.position, OnPathComplete);

		yield return new WaitForSeconds (1f / updateRate);
		StartCoroutine (UpdatePath ());

	}

	public void OnPathComplete(Path p){
		//Debug.Log ("We have a path, Error:" + p.error);
		if (!p.error) {
			path = p;
			currentWaypoint = 0;
		}
	
	}

	void FixedUpdate() {
		if (target == null) {
			if (!searchingForPlayer) {
				searchingForPlayer = true;
				StartCoroutine (SearchForPlayer());
			}
			return;
		}

		//TODO: Always look at player

		if (path == null) {
			return;
		}

		if (currentWaypoint >= path.vectorPath.Count) {
			if (pathIsEnded) {
				return;
			}
			//Debug.Log ("End of path reached");
			pathIsEnded = true;
			return;
		}

		pathIsEnded = false;

		//Direction to the next waypoint
		Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
		dir *= speed * Time.fixedDeltaTime;

		//Move the AI

		rb.AddForce (dir, fMode);

		if (Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]) < nextWaypointDistance) {
			currentWaypoint++;
			return;
		}
	
	}


}
