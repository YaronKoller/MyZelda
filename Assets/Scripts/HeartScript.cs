using UnityEngine;
using System.Collections;

public class HeartScript : MonoBehaviour {

	private Sprite[] heartSprites;


	// Use this for initialization
	void Start () {
		heartSprites = Resources.LoadAll<Sprite>("Hearts");
	}


	// Update is called once per frame
	void Update () {
		if(LinkScript.playerHealth == 3.0f)
			GetComponent<SpriteRenderer>().sprite = heartSprites[0];
		
		else if(LinkScript.playerHealth == 2.5f)
			GetComponent<SpriteRenderer>().sprite = heartSprites[1];
		
		else if(LinkScript.playerHealth == 2.0f)
			GetComponent<SpriteRenderer>().sprite = heartSprites[2];
		
		else if(LinkScript.playerHealth == 1.5f)
			GetComponent<SpriteRenderer>().sprite = heartSprites[3];
		
		else if(LinkScript.playerHealth == 1.0f)
			GetComponent<SpriteRenderer>().sprite = heartSprites[4];
		
		else if(LinkScript.playerHealth == 0.5f)
			GetComponent<SpriteRenderer>().sprite = heartSprites[5];
		
		else
			Destroy(gameObject);
	}
}