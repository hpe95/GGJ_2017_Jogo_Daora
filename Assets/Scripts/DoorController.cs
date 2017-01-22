using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour {
	private LevelLoader level;
	private PlayerController player;
	private bool ending = false;
	private Animator anim;

	void Start(){
		level = FindObjectOfType<LevelLoader> ();
		anim = GetComponent<Animator> ();
	}

	void Update(){
		if (player == null) {
			player = FindObjectOfType<PlayerController> ();
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.layer == LayerMask.NameToLayer ("Player") && !ending) {
			print ("odasdai");
			ending = true;
			int dir;
			if (transform.position.x > col.gameObject.transform.position.x)
				dir = 1;
			else
				dir = -1;
			//print (player);
			player.isDead = true;
			StartCoroutine (sumindo (dir));

		}
	}

	IEnumerator sumindo(int dir){
		Color c = player.sr.color;
		anim.SetTrigger ("OpenDoor");
		while (player.sr.color.a >= 0) {
			print ("asdasdds");
			player.Move (dir/10);
			player.sr.color = new Color (c.r, c.g, c.b, player.sr.color.a - 1f / 24);
			yield return new WaitForSeconds (0.1f);
		}
		level.nextLevel ();
		ending = false;
	}
}
