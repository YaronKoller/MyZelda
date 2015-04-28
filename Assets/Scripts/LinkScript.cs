using UnityEngine;
using System.Collections;

public class LinkScript : MonoBehaviour {

	public static float playerHealth;
	public static int enemiesKilled;

	private Sprite[] linkSprites;
	private string direction;
	private string mode;
	private int counter;


	// Use this for initialization
	void Start () {
		linkSprites = Resources.LoadAll<Sprite>("Link");
		playerHealth = 3.0f;
		enemiesKilled = 0;
		direction = "down";
		mode = "stationary";
		counter = 0;
	}

	
	// Update is called once per frame
	void Update () {
		if(playerHealth <= 0){
			GameOver();
			Destroy(GetComponent<Rigidbody>());
		}

		else if(enemiesKilled == 12){
			YouWin();
			Destroy(GetComponent<Rigidbody>());
		}
		
		else if(mode == "hit"){
			if(counter > 0){
				counter--;
				GetComponent<Rigidbody2D>().velocity /= 2;
			}
			else
				StopMoving();
		}

		else{
			if(Input.GetKey(KeyCode.W)){
				GetComponent<SpriteRenderer>().sprite = linkSprites[14];
				GetComponent<Rigidbody2D>().velocity = new Vector2(0,5);
				direction = "up";
				mode = "moving";
			}
			
			else if(Input.GetKey(KeyCode.A)){
				GetComponent<SpriteRenderer>().sprite = linkSprites[13];
				GetComponent<Rigidbody2D>().velocity = new Vector2(-5,0);
				direction = "left";
				mode = "moving";
			}
			
			else if(Input.GetKey(KeyCode.S)){
				GetComponent<SpriteRenderer>().sprite = linkSprites[12];
				GetComponent<Rigidbody2D>().velocity = new Vector2(0,-5);
				direction = "down";
				mode = "moving";
			}
			
			else if(Input.GetKey(KeyCode.D)){
				GetComponent<SpriteRenderer>().sprite = linkSprites[3];
				GetComponent<Rigidbody2D>().velocity = new Vector2(5,0);
				direction = "right";
				mode = "moving";
			}
			
			else if(Input.GetKey(KeyCode.Space)){
				if(direction == "up"){
					GetComponent<SpriteRenderer>().sprite = linkSprites[38];
				}
				else if(direction == "left"){
					GetComponent<SpriteRenderer>().sprite = linkSprites[37];
				}
				else if(direction == "down"){
					GetComponent<SpriteRenderer>().sprite = linkSprites[36];
				}
				else if(direction == "right"){
					GetComponent<SpriteRenderer>().sprite = linkSprites[39];
				}
				GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
				mode = "attack";
			}
			
			else{
				StopMoving();
			}
		}
	}


	void StopMoving(){
		if(direction == "up"){
			GetComponent<SpriteRenderer>().sprite = linkSprites[2];
		}
		else if(direction == "left"){
			GetComponent<SpriteRenderer>().sprite = linkSprites[1];
		}
		else if(direction == "down"){
			GetComponent<SpriteRenderer>().sprite = linkSprites[0];
		}
		else if(direction == "right"){
			GetComponent<SpriteRenderer>().sprite = linkSprites[15];
		}
		GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
		mode = "stationary";
	}


	void OnCollisionEnter2D (Collision2D other){
		if(other.gameObject.name.StartsWith("Enemy")){
			Vector2 enemy = (other.transform.position - this.transform.position).normalized;

			if(mode == "attack")
				CollisionCheck(other, enemy, 100, true);

			else if(mode == "stationary")
				CollisionCheck(other, enemy, 50, false);

			else
				LinkHit(enemy);
		}
	}


	void CollisionCheck (Collision2D other, Vector2 enemy, int factor, bool isAttack){
		bool succesful = false;

		if((direction == "up") && (enemy.y > 0)){
			succesful = true;
		}
		else if((direction == "left") && (enemy.x < 0)){
			succesful = true;
		}
		else if((direction == "down") && (enemy.y < 0)){
			succesful = true;
		}
		else if((direction == "right") && (enemy.x > 0)){
			succesful = true;
		}

		if(succesful){
			other.rigidbody.velocity = enemy * factor;
			other.transform.SendMessage((other.rigidbody.name + "Hit"), isAttack, SendMessageOptions.RequireReceiver);
		}
		else
			LinkHit(enemy);
	}


	void LinkHit (Vector2 enemy){
		if(direction == "up"){
			GetComponent<SpriteRenderer>().sprite = linkSprites[14];
		}
		else if(direction == "left"){
			GetComponent<SpriteRenderer>().sprite = linkSprites[13];
		}
		else if(direction == "down"){
			GetComponent<SpriteRenderer>().sprite = linkSprites[12];
		}
		else if(direction == "right"){
			GetComponent<SpriteRenderer>().sprite = linkSprites[3];
		}
		GetComponent<Rigidbody2D>().velocity = enemy * -50;
		playerHealth -= 0.5f;
		counter = 10;
		mode = "hit";
	}


	void GameOver(){
		GetComponent<SpriteRenderer>().sprite = linkSprites[81];
	}


	void YouWin(){
		GetComponent<SpriteRenderer>().sprite = linkSprites[61];
		GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
	}
}