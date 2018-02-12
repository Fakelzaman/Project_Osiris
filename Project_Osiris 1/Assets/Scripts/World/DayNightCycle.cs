using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour {

	private int dayLength; //minutes
	private int dayStart;
	private int nightStart; //minutes
	public int currentTime;
	public float cycleSpeed = 10;
	private bool isDay;
	private Vector3 sunPosition;
	public Light sun;
	public GameObject earth;
	public ArrayList sprites = new ArrayList();
	public float blackcolor = 1.0f;

	// Use this for initialization
	void Start () {
		dayLength = 1440;
		dayStart = 300;
		nightStart = 1080;
		currentTime = 720;
		StartCoroutine(TimeOfDay());
		earth = gameObject.transform.parent.gameObject;

		GameObject[] gos = GameObject.FindGameObjectsWithTag ("Visual");
		foreach (GameObject go in gos) {
			sprites.Add (go);

		}

	}
	
	// Update is called once per frame
	void Update () {

		if (currentTime > 0 && currentTime < dayStart) {
			isDay = false;
		} else if (currentTime >= dayStart && currentTime < nightStart) {
			isDay = true;
		} else if (currentTime >= nightStart && currentTime < dayLength) {
			isDay = false;
		} else if (currentTime >= dayLength) {
			currentTime = 0;
		}


		if (!isDay) {

			if (blackcolor > 0.4f) {
			foreach (GameObject go in sprites) {
				
					Color temp = go.GetComponent<SpriteRenderer> ().color;
					temp.b = blackcolor + 0.30f;
					temp.r = blackcolor;
					temp.g = blackcolor;
					go.GetComponent<SpriteRenderer> ().color = temp;

				}
				blackcolor = blackcolor - 0.001f;
			}
		}

		if (isDay) {

			if (blackcolor < 1.0f) {
			foreach (GameObject go in sprites) {
				
					Color temp = go.GetComponent<SpriteRenderer> ().color;
					temp.b = blackcolor + 0.30f;
					temp.r = blackcolor;
					temp.g = blackcolor;
					go.GetComponent<SpriteRenderer> ().color = temp;

				}
				blackcolor = blackcolor + 0.001f;
			}
		}


	}


	IEnumerator TimeOfDay(){
		while (true) {
			currentTime += 1;
			int hours = Mathf.RoundToInt (currentTime / 60);
			int minutes = currentTime % 60;
			//Debug.Log (hours + ":" + minutes);
			yield return new WaitForSeconds (1F / cycleSpeed);
		
		
		}

	}
}
