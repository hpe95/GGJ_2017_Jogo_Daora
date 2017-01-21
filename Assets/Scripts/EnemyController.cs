using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	private const float EPS = 1e-6f;
	private Transform transform;
	private bool walking = false;
	private bool isFollowing = false;
	private int side;
	private Rigidbody2D rb;
	private SpriteRenderer sr;
	private bool facingRight = false;
	private float movingDirection;
	public float moveSpeed;
	public float radiusOfSight;
	public float radiusOfMovement = 0;
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	public bool isGrounded;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		transform = FindObjectOfType<PlayerController> ().gameObject.transform;

		if (radiusOfMovement == 0) {
			radiusOfMovement = Random.Range (-5f, 5f);
		}
	}

	void FixedUpdate(){
		isGrounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);
	}
	// Update is called once per frame
	void Update () {
		FollowPlayer ();
		if (!walking && !isFollowing) {
			walking = true;
			StartCoroutine (Walking ());
		}
			
	}

	private void Move(float moveDirection){
		if (moveDirection < -EPS)
			facingRight = false;
		else if (moveDirection > EPS)
			facingRight = true;

		//this.ChangeFacingDirection ();
		if (moveDirection != 0) {
			rb.velocity = new Vector2 (moveDirection * moveSpeed, rb.velocity.y);
		}

	}

	private void ChangeFacingDirection(){
		sr.flipX = facingRight;
		print ("oi");
	}

	IEnumerator Walking(){
		float distance = radiusOfMovement;
		side = 1;
		Vector3 currPosition = gameObject.transform.position;

		if (distance < 0) {
			side = -1;
			distance *= -1;
		}

		while (Mathf.Abs(currPosition.x - gameObject.transform.position.x) < distance && isGrounded) {				
			
			Move (moveSpeed * side);
			yield return null;
		}

		float seconds = Random.Range (0.5f, 2f);
		yield return new WaitForSeconds (seconds);

		radiusOfMovement = Random.Range (1f, 5f) * Mathf.Sign (radiusOfMovement * -1f);
		walking = false;
	}


	private void FollowPlayer(){
		Vector3 forward = new Vector3(transform.position.x - gameObject.transform.position.x, 0, 0);
		forward.Normalize ();

		if (!facingRight && forward.x > 0 || facingRight && forward.x < 0) {
			return;
		}

		forward *= radiusOfSight;

		Debug.DrawRay (gameObject.transform.position, forward, Color.green);

		RaycastHit2D[] hit = Physics2D.RaycastAll (gameObject.transform.position, forward, radiusOfSight);
		if (hit.Length > 1) {
			if (hit [1].collider != null && hit [1].collider.tag == "Player") {
				isFollowing = true;
				Move (moveSpeed * side);
			} else {
				isFollowing = false;
				rb.velocity = new Vector2 (0, rb.velocity.y);
			}
		} else {
			isFollowing = false;
			rb.velocity = new Vector2 (0, rb.velocity.y);
		}
	}
}
