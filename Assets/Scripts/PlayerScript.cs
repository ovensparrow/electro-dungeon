using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerScript : MonoBehaviour {
	public int speed;

	public GameObject sword;
	public GameObject gun;
	public string equiped;
	public List<GameObject> bullets;
	// Use this for initialization
	void Start () {
		equiped = "sword";
	}
	
	// Update is called once per frame
	void Update () {
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");
		Vector3 Forces = new Vector3 (horizontal * speed * Time.deltaTime, vertical * speed * Time.deltaTime, 0);


		if (Input.GetKeyUp (KeyCode.Alpha1)) {
			equiped = "sword";
		}
		if (Input.GetKeyUp (KeyCode.Alpha2)) {
			equiped = "gun";
		}


		if (Input.GetKeyUp (KeyCode.Space)) {
			if (equiped == "sword"){
				GameObject sw = Instantiate(sword) as GameObject;
				Quaternion rotation = sw.transform.rotation;
				rotation.eulerAngles = new Vector3(0,0,45);
				sw.transform.rotation = rotation;
				Vector3 tempPos = sw.transform.position;
				tempPos.x = transform.position.x + 1;
				tempPos.y = transform.position.y;
				sw.transform.position = tempPos;
				sw.transform.parent = transform;
			}else if (equiped == "gun"){
				GameObject gn = Instantiate(gun) as GameObject;
				Vector3 tempPos = gn.transform.position;
				tempPos.x = transform.position.x + 1;
				tempPos.y = transform.position.y;
				gn.transform.position = tempPos;
				gn.transform.parent = transform;
				GameObject bullet = Instantiate(bullets[0]) as GameObject;
				tempPos = bullet.transform.position;
				tempPos.x = transform.position.x + 3;
				tempPos.y = transform.position.y;
				bullet.transform.position = tempPos;

			}

		}

		GetComponent<Rigidbody>().AddForce (Forces);
	}
}
