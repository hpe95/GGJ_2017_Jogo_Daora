using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {

	private bool canRotate = false;
	private Transform transform;
	// Use this for initialization
	void Start () {
		transform = GetComponent<Transform> ();
	}
	
	void Update () {
		if(!canRotate)
			StartCoroutine (Wait ());
	}

	IEnumerator Wait(){
		canRotate = true;
		if(canRotate)
			transform.Rotate (0, 180, 0);
		yield return new WaitForSeconds (5);
		canRotate = false;
	}
}
