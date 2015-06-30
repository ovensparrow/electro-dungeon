using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Player : MonoBehaviour {

	public float speed;

	public GUIText positiontext;

	int pos_x = 0;
	int pos_y = 0;
	int world_width = 8;
	int world_height = 8;

	Room rc;

	private bool hitBorder = false;

	// Walls
	public Transform WestWall;
	public Transform EastWall;
	public Transform NorthWall;
	public Transform SouthWall;

	//Guns
	public GameObject NorthGun;
	public GameObject EastGun;
	public GameObject SouthGun;
	public GameObject WestGun;

	//Doors
	public GameObject WestDoor;
	public GameObject EastDoor;
	public GameObject NorthDoor;
	public GameObject SouthDoor;

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
		rc = new Room ();
		print (rc.room_biome);

	}
	
	// Update is called once per frame
	void Update () {


		//check the players position for the doors to be correct


		//West Door
		if (pos_x == 0) {
			WestDoor.SetActive (false);
		} else {
			WestDoor.SetActive (true);
		}


		//North Door
		if (pos_y == 0) {
			NorthDoor.SetActive (false);
		} else {
			NorthDoor.SetActive (true);
		}

		//East Door

		if (pos_x >= world_width) {
			EastDoor.SetActive (false);
		} else {
			EastDoor.SetActive (true);
		}

		//South Door

		if (pos_y <= world_width * -1) {
			SouthDoor.SetActive (false);
		} else {
			SouthDoor.SetActive (true);
		}
		
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
		positiontext.text = "(" + pos_x + "," + pos_y + ")";


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

	void OnTriggerEnter2D(Collider2D other){ //Collisions with triggers

		Vector3 tempPos = gameObject.transform.position;

		switch (other.tag) { // checks for door collisions
			case "WestDoor":
				print ("West Door");
				pos_x -=1;	
				
				tempPos = EastDoor.transform.position;
				tempPos.x -= gameObject.transform.localScale.x;
				
				break;
			case "EastDoor":
				print ("East Door");
				pos_x+=1;

				tempPos = WestDoor.transform.position;
				tempPos.x += gameObject.transform.localScale.x;

				break;
			case "NorthDoor":
				print ("North Door");
				pos_y+=1;

				tempPos = SouthDoor.transform.position;
				tempPos.y += gameObject.transform.localScale.y;

				break;
			case "SouthDoor":
				print ("South Door");
				pos_y -=1;
				
				tempPos = NorthDoor.transform.position;
				tempPos.y -= gameObject.transform.localScale.y;	


				break;
		}
		gameObject.transform.position = tempPos;
	}


}
