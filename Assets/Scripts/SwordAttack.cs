using UnityEngine;
using System.Collections;

public class SwordAttack : MonoBehaviour {
	public int counter = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (counter <= 90) {
			gameObject.transform.Rotate (0, 0, -10);
			counter += 10;
		} else if (counter >= 90) {
			Destroy (gameObject);
		}
	}
}
