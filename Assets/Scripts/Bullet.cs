using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	float Time_left = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Time_left += Time.deltaTime;
		if (Time_left >= 5) {
			Destroy (gameObject);
		}
	}
}
