using UnityEngine;
using System.Collections;

public class MoblinScript : MonoBehaviour {

	private Sprite[] enemySprites;
	private int actionNum;
	private int counter;
	private int health;
	private bool hit;

	// Use this for initialization
	void Start () {
		enemySprites = Resources.LoadAll<Sprite>("Enemies");
		actionNum = 4;
		counter = 10;
		health = 3;
		hit = false;
	}


	// Update is called once per frame
	void Update () {
		if(health <= 0){
			LinkScript.enemiesKilled++;
			Destroy(gameObject);
		}

		if(hit){
			if(counter > 0){
				counter--;
				GetComponent<Rigidbody2D>().velocity /= 2;
			}
			else{
				GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
				hit = false;
			}
		}

		else if(counter == 0){
			actionNum = Random.Range(0,6);
			counter = 50;
		}

		else{
			if(actionNum == 0){
				GetComponent<SpriteRenderer>().sprite = enemySprites[82];
				GetComponent<Rigidbody2D>().velocity = new Vector2(2,0);
			}
			
			else if(actionNum == 1){
				GetComponent<SpriteRenderer>().sprite = enemySprites[81];
				GetComponent<Rigidbody2D>().velocity = new Vector2(0,2);
			}
			
			else if(actionNum == 2){
				GetComponent<SpriteRenderer>().sprite = enemySprites[80];
				GetComponent<Rigidbody2D>().velocity = new Vector2(-2,0);
			}
			
			else if(actionNum == 3){
				GetComponent<SpriteRenderer>().sprite = enemySprites[79];
				GetComponent<Rigidbody2D>().velocity = new Vector2(0,-2);
			}
			
			else if(counter == 50){
				StopMoving();
			}

			counter--;
		}
	}


	void StopMoving(){
		if(actionNum == 0){
			GetComponent<SpriteRenderer>().sprite = enemySprites[67];
		}
		else if(actionNum == 0){
			GetComponent<SpriteRenderer>().sprite = enemySprites[66];
		}
		else if(actionNum == 0){
			GetComponent<SpriteRenderer>().sprite = enemySprites[65];
		}
		else if(actionNum == 0){
			GetComponent<SpriteRenderer>().sprite = enemySprites[64];
		}
		GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
	}


	void OnCollisionEnter2D (Collision2D other){
		if(other.gameObject.name.StartsWith("Enemy")){
			counter = 0;
		}
	}


	void EnemyMoblinHit(bool isAttack){
		counter = 10;
		hit = true;

		if(isAttack)
			health--;
	}


	void StopAll(){
		StopMoving ();
		counter = -1;
	}
}