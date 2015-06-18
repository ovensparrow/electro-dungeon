using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed;


	private bool hitBorder = false;

	public Transform WestWall;
	public Transform EastWall;
	public Transform NorthWall;
	public Transform SouthWall;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		//Getting Input from the user via Horizontal and vertical axis (Input my varie) wasd
		float Horizontal = Input.GetAxis ("Horizontal");
		float Vertical = Input.GetAxis ("Vertical");

		Vector3 TempPos = gameObject.transform.position; // Getting player's position


		if ((TempPos.x - gameObject.transform.localScale.x / 2 >= WestWall.position.x && Horizontal < 0) || (TempPos.x + gameObject.transform.localScale.x / 2 <= EastWall.position.x && Horizontal > 0)) {
			TempPos.x += Horizontal * speed * Time.deltaTime;
		}

		if ((TempPos.y - gameObject.transform.localScale.y / 2 >= SouthWall.position.y && Vertical < 0) || (TempPos.y + gameObject.transform.localScale.y / 2<= NorthWall.position.y && Vertical > 0)){
			TempPos.y += Vertical * speed * Time.deltaTime;
		}

		//update players position
		gameObject.transform.position = TempPos;


	}
	
}
