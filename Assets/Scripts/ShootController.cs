using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour {
	private float distance = 0f;
	public GameObject gameObject;
	// Use this for initialization
	void Start () {
		GameObject player = GameObject.Find ("Player");
		Transform playerTransform = player.transform;
		Vector2 pos = playerTransform.position;
		transform.position = new Vector2 (pos.x, pos.y);
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.right);
		Debug.DrawLine (transform.position, hit.point, Color.red);

		distance = Mathf.Abs (hit.point.y - transform.position.y);
	}
}
