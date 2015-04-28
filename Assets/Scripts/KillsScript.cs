using UnityEngine;
using System.Collections;

public class KillsScript : MonoBehaviour {
	
	private Sprite[] moblinSprites;

	// Use this for initialization
	void Start () {
		moblinSprites = Resources.LoadAll<Sprite>("Moblins");
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<SpriteRenderer>().sprite = moblinSprites [12 - LinkScript.enemiesKilled];
	}
}