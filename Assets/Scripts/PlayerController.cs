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

	private List<AllPossiblePickups.Pickups> pickups;
	private const float EPS = 1e-6f;	
	public bool isOnMagneticField = false;
	public float timeForRespawn;
	public Transform checkpoint;
	private SpriteRenderer sr;
	private float moveDirection = 0f;
	public float jumpSpeed = 0f;
	private bool facingRight = true;
	public bool isGrounded;

	public bool IsGrounded {
		get {
			print ("ola");
			return isGrounded;

		}
		set {
			print ("oi");
			if (isGrounded == false && value == true) {
				GetComponent<Animator> ().SetTrigger ("FallTrigger");
			}
			isGrounded = value;
		}
	}

	public bool isDead = false;
	private bool canJump = false;
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	public Animator anim;
	// Use this for initialization
	void Start () {
		pickups = new List<AllPossiblePickups.Pickups> ();
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
	}

	void FixedUpdate(){
		IsGrounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Y)){ // Só pra testar
			Kill ();
		}
			
		if (!isDead) {
			if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
				rb.velocity = new Vector2 (rb.velocity.x, jumpSpeed);

			anim.SetBool ("Grounded", IsGrounded);
			anim.SetFloat("JumpSpeed",rb.velocity.y);
			ChangeFacingDirection ();
			moveDirection = Input.GetAxis ("Horizontal");
			this.Move (moveDirection);
		}
		anim.SetFloat ("Speed", Mathf.Abs(moveDirection));
	}

	private void Kill(){
		if (!isDead) {
			rb.velocity = Vector2.zero;
			isDead = true;
			sr.enabled = false;
			StartCoroutine (Respawn ());
		}
	}

	private IEnumerator Respawn(){
		yield return new WaitForSeconds (timeForRespawn);
		isDead = false;
		transform.position = checkpoint.position;
		rb.velocity = Vector2.zero;
		sr.enabled = true;
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
		if (isGrounded && Mathf.Abs (moveDirection) < EPS)
			rb.velocity = new Vector2 (0, rb.velocity.y);

	}


	private void ChangeFacingDirection(){
		sr.flipX = facingRight;
	}

	public void setCheckpoint(GameObject go){
		checkpoint = go.transform;
	}

	public void addPickup(AllPossiblePickups.Pickups pickup){
		pickups.Add (pickup);
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.layer == LayerMask.NameToLayer("Activatable Objects")) {
			ActivatableObjects obj = col.gameObject.GetComponent<ActivatableObjects> ();
			obj.TryToUse (ref pickups);
		}
	}
}
