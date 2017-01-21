using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {
	public bool gotHit = false;

	private Vector3 newPos;
	private PlayerController shoot;
	private Rigidbody2D rb;
	private bool canShoot = false;
	// Use this for initialization
	void Start () {
		shoot = FindObjectOfType<PlayerController> ();
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!canShoot) {
			StartCoroutine (Wait ());
		}
	}

	IEnumerator Wait(){
		canShoot = true;
		if(canShoot && shoot.shootForce != 0){
			newPos = transform.position;
			newPos.x = shoot.shootForce;
			transform.position = newPos;
		}
			//rb.AddForce (new Vector2 (shoot.shootForce, rb.velocity.y));
		yield return new WaitForSeconds (1f);
		//rb.AddForce (new Vector2 (0, 0));
		canShoot = false;
	}
		
}
