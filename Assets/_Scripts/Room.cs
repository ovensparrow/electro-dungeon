using UnityEngine;
using System.Collections;




public class Room : MonoBehaviour {

	public enum biomes {forest,cave,dungeon}

	public biomes room_biome;
	
	
	// Use this for initialization
	public Room () {
		int b = Random.Range (0, 3);
		room_biome = (biomes)b;
	}

	//Room



}
