using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[System.Serializable]
	public class EnemyStats{
		public int health = 100;
	}

	public EnemyStats enemyStats = new EnemyStats();


	public void DamagePlayer(int damage){
		enemyStats.health -= damage;
		Effect ();
		if (enemyStats.health <= 0) {
			GameMaster.KillCharacter (this);
		
		}
	
	}

	void Effect(){
		StartCoroutine(hitRed());


	}

	IEnumerator hitRed(){
		Color temp = GetComponent<SpriteRenderer> ().color;
		temp.b = 0.2f;
		temp.r = 1.0f;
		temp.g = 0.2f;
		GetComponent<SpriteRenderer> ().color = temp;

		yield return new WaitForSeconds (0.1f);

		temp = GetComponent<SpriteRenderer> ().color;
		temp.b = 1.0f;
		temp.r = 1.0f;
		temp.g = 1.0f;
		GetComponent<SpriteRenderer> ().color = temp;

	}

}
