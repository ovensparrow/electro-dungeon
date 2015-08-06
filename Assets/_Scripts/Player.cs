using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Player : MonoBehaviour {

	public float speed;

	public GameObject room;

	public GUIText healthbar;
	public int health;

	public int chests_collected;

	public bool gameover = false;
	public GUIText gameovertext;


	public GUIText wepinfo;
	public GUIText score;

	//Weapons
	public GameObject sword;
	public GameObject projectile;


	public GameObject background;

	public GUIText InstructionsText;

	public Vector2 current_player_pos;
	public GameObject current_room;


	int world_width = 8;
	int world_height = 8;

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

	public enum Direction
	{
		NORTH,EAST,SOUTH,WEST
	};

	enum Weapons{
		SWORD,GUN
	};
	int weapon_count = 2;

	Direction gun_dir = Direction.EAST;
	Weapons wep_equiped = Weapons.SWORD;


	// Use this for initialization
	void Start () {
		health = 10;
		RotatedGuns.Add (NorthGun);
		RotatedGuns.Add (EastGun);
		RotatedGuns.Add (SouthGun);
		RotatedGuns.Add (WestGun);
		
		InstructionsText.text = "Press space to use weapon.\n Use W,A,S,D to nav.\n Press E to rotate Weapon and Press R to switch Weapon\n Press ESC to Exit \n Press Q to Reload";
		UpdateGunDirection ();

		game_world = GenerateWorld (world_width, world_height);
		chests_collected = 0;

	
		healthbar.text = GenerateHealth (5);
		

		current_player_pos = Vector2.zero;
		current_room = game_world[(int)current_player_pos.y * -1][(int)current_player_pos.x];
	}
	
	// Update is called once per frame
	void Update () {

		//Getting Input from the user via Horizontal and vertical axis (Input my varie) wasd
		float Horizontal = Input.GetAxis ("Horizontal");
		float Vertical = Input.GetAxis ("Vertical");

		bool quit = Input.GetKeyDown (KeyCode.Escape);
		bool reload = Input.GetKeyDown (KeyCode.Q);

		if (reload == true) {
			Application.LoadLevel("MainGame");
		}

		if (quit == true) {
			Application.Quit ();
		}

		bool RotateGun = Input.GetKeyUp (KeyCode.E);
		bool UseWeapon = Input.GetKeyUp (KeyCode.Space);
		bool SwitchWeapon = Input.GetKeyUp (KeyCode.R);

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


		if (SwitchWeapon == true) {
			int wep_number = (int) wep_equiped;
			wep_number = (wep_number + 1) % weapon_count;
			wep_equiped = (Weapons) wep_number;
		}
	




		if (UseWeapon == true) {
			if (wep_equiped == Weapons.SWORD){
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
			}else if (wep_equiped == Weapons.GUN){
				GameObject go_proj = Instantiate(projectile) as GameObject;
				go_proj.GetComponent<Projectile>().speed = 10;
				go_proj.GetComponent<Projectile>().proj_dir = gun_dir;
				//go_proj.transform.parent = gameObject.transform;

				Vector3 proj_pos = go_proj.transform.position;

				switch (gun_dir){
					case Direction.NORTH:
						proj_pos = NorthGun.transform.position;
						break;
					case Direction.SOUTH:
						proj_pos = SouthGun.transform.position;
						break;
					case Direction.EAST:
						proj_pos = EastGun.transform.position;
						break;
					case Direction.WEST:
						proj_pos = WestGun.transform.position;
						break;
				}
				go_proj.transform.position = proj_pos;


			}





			UseWeapon = false;
		}



		wepinfo.text = "Current Wep:\n" + wep_equiped.ToString ();
		score.text = "Score: " + chests_collected;

		//update players position
		gameObject.transform.position = TempPos;

		current_room = game_world[(int)current_player_pos.y * -1][(int)current_player_pos.x];
		current_room.SetActive (true);
		healthbar.text = GenerateHealth (health);

		if (health <= 0) {
			gameover = true;
		}
		
		if (gameover == true) {
			gameovertext.text = "Game Over.\n" + score.text;
		}



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
				current_player_pos.x -=1;	
				
				tempPos = EastDoor.transform.position;
				tempPos.x -= gameObject.transform.localScale.x;
				
				break;
			case "EastDoor":
				current_player_pos.x+=1;

				tempPos = WestDoor.transform.position;
				tempPos.x += gameObject.transform.localScale.x;

				break;
			case "NorthDoor":
				current_player_pos.y+=1;

				tempPos = SouthDoor.transform.position;
				tempPos.y += gameObject.transform.localScale.y;

				break;
			case "SouthDoor":
				current_player_pos.y -=1;
				
				tempPos = NorthDoor.transform.position;
				tempPos.y -= gameObject.transform.localScale.y;	


				break;
		}
		gameObject.transform.position = tempPos;
	}

	public string GenerateHealth (int hp){
		string HealthString = "";
		for (int i = 0; i < hp; i++) {
			HealthString = "|" + HealthString;
		}

		return HealthString;
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
