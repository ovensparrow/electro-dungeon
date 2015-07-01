using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Player : MonoBehaviour {

	public float speed;

	public GameObject room;

	public GameObject sword;

	public GameObject background;

	public GUIText positiontext;

	public Vector2 current_player_pos;
	public GameObject current_room;


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
	List<List<GameObject>> game_world = new List<List<GameObject>>();

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

		game_world = GenerateWorld (world_width, world_height);

		current_player_pos = Vector2.zero;
		current_room = game_world[(int)current_player_pos.y * -1][(int)current_player_pos.x];
	}
	
	// Update is called once per frame
	void Update () {
		

		//Getting Input from the user via Horizontal and vertical axis (Input my varie) wasd
		float Horizontal = Input.GetAxis ("Horizontal");
		float Vertical = Input.GetAxis ("Vertical");

		bool RotateGun = Input.GetKeyUp (KeyCode.E);
		bool UseWeapon = Input.GetKeyUp (KeyCode.Space);
		


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

		//check the players position for the doors to be correct
		
		//West Door
		if (current_player_pos.x == 0) {
			WestDoor.SetActive (false);
		} else {
			WestDoor.SetActive (true);
		}
		
		
		//North Door
		if (current_player_pos.y == 0) {
			NorthDoor.SetActive (false);
		} else {
			NorthDoor.SetActive (true);
		}
		
		//East Door
		
		if (current_player_pos.x >= world_width - 1) {
			EastDoor.SetActive (false);
		} else {
			EastDoor.SetActive (true);
		}
		
		//South Door
		
		if (current_player_pos.y <= world_height * -1 + 1) {
			SouthDoor.SetActive (false);
		} else {
			SouthDoor.SetActive (true);
		}

		if (UseWeapon == true) {
			GameObject wep = Instantiate(sword) as GameObject;
			wep.transform.parent = gameObject.transform;
			Vector3 WepPos = gameObject.transform.position;

			switch (gun_dir){
				case Direction.EAST:
					WepPos.x+= gameObject.transform.localScale.x;
					WepPos.y+= gameObject.transform.localScale.y /4f;
					break;
				case Direction.WEST:
					WepPos.x-= gameObject.transform.localScale.x;
					WepPos.y+= gameObject.transform.localScale.y /4f;

					wep.transform.Rotate(new Vector3(0,0,180));
					break;
				case Direction.NORTH:
					WepPos.y+= gameObject.transform.localScale.y;
					wep.transform.Rotate(new Vector3(0,0,90));

					break;
				case Direction.SOUTH:
					WepPos.y-= gameObject.transform.localScale.y /2;
					wep.transform.Rotate(new Vector3(0,0,270));
					
					break;
			}
			wep.transform.position = WepPos;
			UseWeapon = false;
		}





		//update players position
		gameObject.transform.position = TempPos;
		positiontext.text = "(" + current_player_pos.x + "," + current_player_pos.y + ")";

		current_room = game_world[(int)current_player_pos.y * -1][(int)current_player_pos.x];
		current_room.SetActive (true);

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

		current_room.SetActive (false);
		Vector3 tempPos = gameObject.transform.position;
		switch (other.tag) { // checks for door collisions
			case "WestDoor":
				print ("West Door");
				current_player_pos.x -=1;	
				
				tempPos = EastDoor.transform.position;
				tempPos.x -= gameObject.transform.localScale.x;
				
				break;
			case "EastDoor":
				print ("East Door");
				current_player_pos.x+=1;

				tempPos = WestDoor.transform.position;
				tempPos.x += gameObject.transform.localScale.x;

				break;
			case "NorthDoor":
				print ("North Door");
				current_player_pos.y+=1;

				tempPos = SouthDoor.transform.position;
				tempPos.y += gameObject.transform.localScale.y;

				break;
			case "SouthDoor":
				print ("South Door");
				current_player_pos.y -=1;
				
				tempPos = NorthDoor.transform.position;
				tempPos.y -= gameObject.transform.localScale.y;	


				break;
		}
		gameObject.transform.position = tempPos;
	}

	public List<List<GameObject>> GenerateWorld(int width,int height){

		List<List<GameObject>> golist = new List<List<GameObject>> ();

		for (int y = 0; y < height; y++) {

			List<GameObject> tempgolist = new List<GameObject>();

			for (int x = 0; x < width; x++){
				GameObject room_go = Instantiate(room) as GameObject;
				room_go.GetComponent<Room>().background = background;
				room_go.SetActive(false);
				tempgolist.Add(room_go);
			}

			golist.Add(tempgolist);
		}

		return golist;
		
	}


}
