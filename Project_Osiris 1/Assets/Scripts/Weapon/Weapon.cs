using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour {

	public float fireRate = 0f;
	public int Damage = 30;
	public LayerMask whatToHit;
	public float GunRange = 100;

	public int maxAmmo = 6;
	private int currentAmmo;
	public float reloadTime = 0.5f;
	public bool isReloading = false;

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
		currentAmmo = maxAmmo;
		firePoint = transform.Find ("FirePoint");
		audio = GetComponent<AudioSource> ();
		if (firePoint == null) {
			Debug.LogError ("No FirePoint Found");
		}
		if (audio == null) {
			Debug.LogError ("No Audio Found");
		}
	}

	void OnEnabled(){
		isReloading = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isReloading) {
			return;
		}

		if (currentAmmo <= 0 || Input.GetKeyDown (KeyCode.R)) {
			StartCoroutine(Reload ());
			return;
		}
			

		if (fireRate == 0) {
			if (Input.GetMouseButtonDown (0) && Input.GetMouseButton (1)  && !isReloading) {
				if (player.GetComponent<PlayerCombat> ().shotBullet ()) {
					Shoot ();
				}
			}
		}
		else if(fireRate != 0){
			if (Input.GetMouseButton (0) && Input.GetMouseButton (1) && Time.time > timeToFire && !isReloading) {
				if (player.GetComponent<PlayerCombat> ().shotBullet ()) {
					timeToFire = Time.time + 1 / fireRate;
					Shoot ();
				}
			}

		}
			
	}

	IEnumerator Reload(){
		Debug.Log ("Reloading");
		isReloading = true;
		yield return new WaitForSeconds (reloadTime * (maxAmmo - currentAmmo));
		if (player.GetComponent<PlayerCombat> ().spareAmmo > 5) {
			player.GetComponent<PlayerCombat> ().spareAmmo = player.GetComponent<PlayerCombat> ().spareAmmo - (maxAmmo - currentAmmo);
			currentAmmo = maxAmmo;
		} else {
			currentAmmo = currentAmmo + player.GetComponent<PlayerCombat> ().spareAmmo;
			player.GetComponent<PlayerCombat> ().spareAmmo = 0;
		}
		player.GetComponent<PlayerCombat> ().leftInClip = currentAmmo - 1;

		for(int i = 0; i < player.GetComponent<PlayerCombat> ().leftInClip + 1; i++){
				player.GetComponent<PlayerCombat> ().inClip[i].fillAmount = 100;
		}

		isReloading = false;

	}

	void Shoot(){
		currentAmmo--;

		audio.Play ();
		Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, mousePosition - firePointPosition, GunRange, whatToHit);
		GameObject trail = Effect ();
		Debug.DrawLine (firePointPosition, (mousePosition - firePointPosition)* 100, Color.cyan);
		if (hit.collider != null) {
			Debug.DrawLine (firePointPosition, hit.point, Color.red);

			Debug.Log ("We Hit " + hit.collider.name + " and did " + Damage + " damage");
			Enemy enemy = hit.collider.GetComponentInParent<Enemy> ();
			if (enemy != null) {
				hit.collider.gameObject.GetComponentInParent<Enemy> ().DamagePlayer (Damage);
				Vector2 dis = firePointPosition - mousePosition;
				//Destroy (trail.gameObject, Mathf.Sqrt(Mathf.Sqrt(dis.x) + Mathf.Sqrt(dis.y)) / trail.GetComponent<MoveTrail>().moveSpeed);
			}
		}
	}

	GameObject Effect(){
		//Transform trail = (Transform) Instantiate (bulletTrailPrefab, firePoint.position, firePoint.rotation);
		Transform clone = (Transform) Instantiate (MuzzleFlashPrefab, firePoint.position, firePoint.rotation);
		return null;//trail.gameObject;
	}
}
