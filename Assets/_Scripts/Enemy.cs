using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public enum direction {left,right}
	direction dir;

	float counter;
	public float speed;


	// Use this for initialization
	void Start () {


		dir = direction.left;
		counter = 0;
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 TempPos = gameObject.transform.position;


		if (dir == direction.left) {
			counter -= speed * Time.deltaTime;
			TempPos.x -= speed * Time.deltaTime;
		}

		if (dir == direction.right) {
			counter += speed * Time.deltaTime;
			TempPos.x += speed * Time.deltaTime;
		}

		if (counter <= -1.0f) {
			dir = direction.right;
		} else if (counter >= 1.0f) {
			dir = direction.left;
		}

		gameObject.transform.position = TempPos;
	}
}
