using UnityEngine;
using System.Collections;

public class Chest : MonoBehaviour {

	public Player p;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			print ("Player Collision");
			p.chests_collected += 1;
			Destroy (gameObject);
		}
	}


}
