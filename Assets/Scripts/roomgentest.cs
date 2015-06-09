using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class roomgentest : MonoBehaviour {
	public GameObject room_part;
	public GameObject border_prefab;

	public List<GameObject> rooms;
	public int number_of_rooms;
	// Use this for initialization
	void Start () {
		for (int i = 0; i <= number_of_rooms -2; i ++) {
			GameObject gm = Instantiate(room_part) as GameObject;
			Vector3 TempPos = rooms[rooms.Count -1].transform.position;

			int direction = Random.Range(1,5);
			switch(direction){
			case 1:
				TempPos.x+= 2.5f;
				break;
			case 2:
				TempPos.x-= 2.5f;
				break;
			case 3:
				TempPos.y-= 2.5f;
				break;
			case 4:
				TempPos.y+= 2.5f;
				break;
			}
			gm.transform.position = TempPos;
			for (int c = 0; c != rooms.Count; c++){
				GameObject rm = rooms[c];
				if (rm.transform.position == gm.transform.position){
					rooms.RemoveAt (c);
					Destroy (rm);
					number_of_rooms+=1;
				}
			}
			rooms.Add(gm);

		}
		foreach (GameObject rm in rooms) {
			rm.GetComponent<Renderer>().material.color = new Color(Random.value,Random.value,Random.value);
			GameObject border = Instantiate(border_prefab) as GameObject;
			Vector3 tempPos = border.transform.position;
			tempPos.x = rm.transform.position.x;
			tempPos.y = rm.transform.position.y;
			border.transform.position = tempPos;
		}


	}
	
	// Update is called once per frame
	void Update () {
	}
}
