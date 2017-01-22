using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour {

	public GameObject magnet;
	
	// Update is called once per frame
	void Update () {
		Collider2D col = Physics2D.OverlapCircle(magnet.transform.position, 1, LayerMask.NameToLayer("Player"));
			
	}
}
