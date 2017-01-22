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
		if (shoot.shootForce != 0) {
			if (!canShoot) {
				StartCoroutine (Wait ());
			}
		}
	}

	IEnumerator Wait(){
		canShoot = true;
		if(canShoot){
			rb.AddForce (new Vector2 (shoot.shootForce, 0));
		}
		yield return new WaitForSeconds (2f);
		canShoot = false;
	}
		
}
