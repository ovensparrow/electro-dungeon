using UnityEngine;
using System.Collections;

public class rooms : MonoBehaviour {

	float time = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time >= 1) {
			transform.Rotate(new Vector3(0,0,-90));
			time = 0;
		}
	}
}
