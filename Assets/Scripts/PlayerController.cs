using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Polarity polarity;

	public Polarity Polarity {
		get {
			return polarity;
		}
	}
	public float moveSpeed = 0f;

	private Rigidbody2D rb;

	public Rigidbody2D Rb {
		get {
			return rb;
		}
	}

	private SpriteRenderer sr;
	private float moveDirection = 0f;
	public float jumpSpeed = 0f;
	private bool facingRight = true;
	public bool isGrounded = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		moveDirection = Input.GetAxis ("Horizontal");
		this.Move (moveDirection);
		this.Jump ();
	}

	private void Move(float moveDirection){
		if (moveDirection < 0)
			facingRight = false;
		else
			facingRight = true;

		//this.ChangeFacingDirection ();
		if (moveDirection != 0) {
			rb.velocity = new Vector2 (moveDirection * moveSpeed, rb.velocity.y);
		}

	}

	private void Jump(){
		if (!isGrounded ) {
			return;
		} else if(Input.GetKeyDown (KeyCode.Space)){
			rb.velocity = new Vector2 (rb.velocity.x, jumpSpeed);
			isGrounded = false;
		}
	}

	void OnCollisionStay2D(Collision2D col){
		if (col.gameObject.tag == "Ground") {
			isGrounded = true;
		}
	}

	void OnCollisionExit2D(Collision2D col){
		if (col.gameObject.tag == "Ground") {
			isGrounded = false;
		}
	}

	/*private void ChangeFacingDirection(){
		sr.flipX = facingRight;
	}*/
}
