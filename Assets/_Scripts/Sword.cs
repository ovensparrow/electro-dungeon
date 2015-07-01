using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour {

	public float lifeSpan;

	// Use this for initialization
	void Start () {
		Invoke ("die", lifeSpan);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void die(){
		Destroy (gameObject);
	}

}
