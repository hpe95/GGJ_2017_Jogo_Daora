using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour {
	private LevelLoader level;
	void Start(){
		level = FindObjectOfType<LevelLoader> ();
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.layer == LayerMask.NameToLayer ("Player"))
			level.nextLevel ();
	}
}
