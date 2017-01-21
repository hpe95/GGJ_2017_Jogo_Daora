using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour {
	public float distance = 0f;
	public float force = 0f;
	private PlayerController player;
	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController> ();

		Transform playerTransform = player.transform;
		Vector2 pos = playerTransform.position;
		transform.position = new Vector2 (pos.x, pos.y);

	}
	
	// Update is called once per frame
	void Update () {
		force = player.shootForce;
		RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.right * force);
		Debug.DrawLine (transform.position, hit.point, Color.red);

		if (hit.collider != null && hit.collider.tag == "AssassinObjects") {
			distance = Mathf.Abs (hit.point.x - transform.position.x);
			print (distance);
		}


	}
}
