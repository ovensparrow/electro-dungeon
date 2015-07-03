using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Room : MonoBehaviour {

	public enum biomes {forest,cave,dungeon}

	public biomes room_biome;

	public GameObject Enemy;

	public GameObject Chest;

	public GameObject background;

	public List<GameObject> Enemies;

	Player p;

	//biomes Materials

	public Material forest;
	public Material dungeon;
	public Material cave;



	//Start
	void Start(){

		p = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();


		int chest_chance = Random.Range (0, 4);

		//Used form biome generation
		int b = Random.Range (0, 3);
		room_biome = (biomes)b;

		int e = Random.Range (0, 2);

		Enemies = new List<GameObject> ();

		for (int i = 0; i < e + 1; i ++) {
			GameObject go = Instantiate(Enemy) as GameObject;
			go.transform.parent = gameObject.transform;

			Vector3 tempPos = go.transform.position;
			tempPos = Vector3.zero;
			tempPos.y += Random.Range (0,3) - 1.5f;
			tempPos.y += Random.Range (0,3) - 1.5f;
			go.transform.position = tempPos;


			Enemies.Add (go);
		}

		switch (room_biome) {
			case biomes.forest:
				background.GetComponent <Renderer>().material = forest;
				break;
			case biomes.cave:
				background.GetComponent <Renderer>().material = cave;
				break;
			case biomes.dungeon:
				background.GetComponent <Renderer>().material = dungeon;
				break;
		}


		if (chest_chance == 0) {
			GameObject go = Instantiate(Chest) as GameObject;
			go.GetComponent<Chest>().p = p;
			go.transform.parent = gameObject.transform;
		}




	}

	//Update
	void Update(){

	}


}
