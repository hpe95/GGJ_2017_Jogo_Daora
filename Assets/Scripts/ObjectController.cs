using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {
	public bool gotHit = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator Wait(){
		yield return new WaitForSeconds (3);
	}
}
