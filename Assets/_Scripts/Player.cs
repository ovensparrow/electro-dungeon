using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Player : MonoBehaviour {

	public float speed;


	private bool hitBorder = false;

	// Walls
	public Transform WestWall;
	public Transform EastWall;
	public Transform NorthWall;
	public Transform SouthWall;

	public GameObject NorthGun;
	public GameObject EastGun;
	public GameObject SouthGun;
	public GameObject WestGun;

	List<GameObject> RotatedGuns = new List<GameObject>();


	enum Direction
	{
		NORTH,EAST,SOUTH,WEST
	};

	Direction gun_dir = Direction.EAST;


	// Use this for initialization
	void Start () {
		RotatedGuns.Add (NorthGun);
		RotatedGuns.Add (EastGun);
		RotatedGuns.Add (SouthGun);
		RotatedGuns.Add (WestGun);

		UpdateGunDirection ();

	
	}
	
	// Update is called once per frame
	void Update () {


		//Getting Input from the user via Horizontal and vertical axis (Input my varie) wasd
		float Horizontal = Input.GetAxis ("Horizontal");
		float Vertical = Input.GetAxis ("Vertical");

		bool RotateGun = Input.GetKeyUp (KeyCode.E);



		Vector3 TempPos = gameObject.transform.position; // Getting player's position


		if ((TempPos.x - gameObject.transform.localScale.x / 2 >= WestWall.position.x && Horizontal < 0) || (TempPos.x + gameObject.transform.localScale.x / 2 <= EastWall.position.x && Horizontal > 0)) {
			TempPos.x += Horizontal * speed * Time.deltaTime;
		}

		if ((TempPos.y - gameObject.transform.localScale.y / 2 >= SouthWall.position.y && Vertical < 0) || (TempPos.y + gameObject.transform.localScale.y / 2<= NorthWall.position.y && Vertical > 0)){
			TempPos.y += Vertical * speed * Time.deltaTime;
		}

		if (RotateGun == true) { // Checks if player wants to rotate the gun
			int TempGunDir = (int) gun_dir;

			//Rotates the gun clockwise
			TempGunDir = (TempGunDir + 1) % 4; 
			gun_dir  = (Direction) TempGunDir;
			UpdateGunDirection ();
			RotateGun = false;
		}


		//update players position
		gameObject.transform.position = TempPos;
		
	}
	void UpdateGunDirection(){
		int i;
		for (i = 0; i < RotatedGuns.Count; i ++) {
			if (((Direction) i) == gun_dir){
				RotatedGuns[i].SetActive (true);
			}else{
				RotatedGuns[i].SetActive (false);
			}
		}
	}
}
