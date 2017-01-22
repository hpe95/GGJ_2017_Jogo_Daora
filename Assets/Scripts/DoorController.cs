using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.layer == LayerMask.NameToLayer("Player"))
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
