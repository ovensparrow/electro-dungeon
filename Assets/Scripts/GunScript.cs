using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour {
	float time = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time >= 1) {
			Destroy (gameObject);
		}
	}
}
