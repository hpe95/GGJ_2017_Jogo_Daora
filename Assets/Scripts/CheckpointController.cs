using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour {

	PlayerController player;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController> ();	
	}
	
	// Update is called once per frame
	void Update () {
		Collider2D maybePlayer = Physics2D.OverlapCircle (transform.position, 1,1 << LayerMask.NameToLayer("Player"));
		if (maybePlayer != null) {
			player.setCheckpoint (gameObject);
		}
	}
}
