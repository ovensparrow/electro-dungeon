using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float speed;
	

	public Player.Direction proj_dir;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		Vector3 TempPos = gameObject.transform.position;



		switch (proj_dir) {
			case Player.Direction.EAST:
				TempPos.x += speed * Time.deltaTime;
				break;
			case Player.Direction.WEST:
				TempPos.x -= speed * Time.deltaTime;
				break;
			case Player.Direction.NORTH:
				TempPos.y += speed * Time.deltaTime;
				break;
			case Player.Direction.SOUTH:
				TempPos.y -= speed * Time.deltaTime;
				break;
		}

		gameObject.transform.position = TempPos;






	}
}
