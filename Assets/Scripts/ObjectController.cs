using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {
	public bool gotHit = false;

	private Vector3 newPos;
	private ShootController shoot;
	// Use this for initialization
	void Start () {
		shoot = FindObjectOfType<ShootController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (shoot.distance != 0) {
			newPos = transform.position;
			newPos.x = shoot.distance;
			transform.position = newPos;
			//transform.position.x += shoot.distance;
		}
	}

	//IEnumerator Wait(){
	//	yield return new WaitForSeconds (3);
	//}
		
}
