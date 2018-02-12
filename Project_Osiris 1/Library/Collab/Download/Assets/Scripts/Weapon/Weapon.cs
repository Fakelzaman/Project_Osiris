using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public float fireRate = 0f;
	public float Damage = 10f;
	public LayerMask whatToHit;
	public float GunRange = 100;

	float timeToSpawn;
	public float spawnRate;

	public Transform bulletTrailPrefab;
	public Transform MuzzleFlashPrefab;

	public GameObject player;

	float timeToFire = 0f;
	Transform firePoint;

	AudioSource audio;

	// Use this for initialization
	void Awake () {
		firePoint = transform.FindChild ("FirePoint");
		audio = GetComponent<AudioSource> ();
		if (firePoint == null) {
			Debug.LogError ("No FirePoint Found");
		}
		if (audio == null) {
			Debug.LogError ("No Audio Found");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (fireRate == 0) {
			if (Input.GetMouseButtonDown (0) && Input.GetMouseButton (1) && player.GetComponent<PlayerCombat>().shotBullet()) {
				Shoot ();
			}
		}
		else if(fireRate != 0){
			if(Input.GetMouseButton (0) && Input.GetMouseButton (1) && Time.time > timeToFire && player.GetComponent<PlayerCombat>().shotBullet()){
				timeToFire = Time.time + 1 / fireRate;
				Shoot ();
			}

		}
			
	}

	void Shoot(){
		audio.Play ();
		Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, mousePosition - firePointPosition, GunRange, whatToHit);
		Effect ();
		Debug.DrawLine (firePointPosition, (mousePosition - firePointPosition)* 100, Color.cyan);
		if (hit.collider != null) {
			Debug.DrawLine (firePointPosition, hit.point, Color.red);
			Debug.Log ("We Hit " + hit.collider.name + " and did " + Damage + " damage");
		}
	}

	void Effect(){
		Instantiate (bulletTrailPrefab, firePoint.position, firePoint.rotation);
		Transform clone = (Transform) Instantiate (MuzzleFlashPrefab, firePoint.position, firePoint.rotation);
		clone.parent = firePoint;
		float size = Random.Range (0.2f, 0.3f);
		clone.localScale = new Vector3 (size, size, size);
		Destroy (clone.gameObject, 0.02f);
	}
}
