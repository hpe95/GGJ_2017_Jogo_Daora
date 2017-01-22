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
	private float distance = 0f;
	public bool isOnMagneticField = false;
	public float timeForRespawn;
	public Transform checkpoint;
	private SpriteRenderer sr;
	private float moveDirection = 0f;
	public float jumpSpeed = 0f;
	private bool facingRight = true;
	public bool isGrounded;
	public float shootForce = 0f;
	public bool IsGrounded {
		get {
			return isGrounded;

		}
		set {
			if (isGrounded == false && value == true) {
				GetComponent<Animator> ().SetTrigger ("FallTrigger");
			}
			isGrounded = value;
		}
	}

	public bool coolDown = false;
	public bool isDead = false;
	private bool canJump = false;
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	public Animator anim;

	private float timeToShoot = 0f;
	// Use this for initialization
	private ObjectController oc;
	void Start () {
		pickups = new List<AllPossiblePickups.Pickups> ();
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
		oc = FindObjectOfType<ObjectController> ();
	}

	void FixedUpdate(){
		IsGrounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Y)){ // Só pra testar
			Kill ();
		}
			
		if (!coolDown)
			StartCoroutine (WaitForShoot ());

		print (timeToShoot/10);
		anim.SetFloat ("TimeToShoot", timeToShoot);
		RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.right, 1000000, 1 << LayerMask.NameToLayer("Box"));
		print (hit.collider.gameObject.transform.position);
		Debug.DrawLine(transform.position, hit.collider.gameObject.transform.position);
		if (hit.collider != null && hit.collider.tag == "AssassinObjects") {
			distance = Mathf.Abs (hit.point.x - transform.position.x);
			print ("entrou " + distance);
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

	IEnumerator WaitForShoot(){
		ShootForce ();
		yield return new WaitForSeconds (1);
		var teste = oc.GetComponent<Transform> ().position;
		teste.x = teste.x - 0.000001f;
		oc.GetComponent<Transform> ().position = teste;
		shootForce = 0f;
		coolDown = false;
	}

	private void Move(float moveDirection){
		if (moveDirection < -EPS)
			facingRight = false;
		else if (moveDirection > EPS)
			facingRight = true;

		if (moveDirection != 0) {
			rb.velocity = new Vector2 (moveDirection * moveSpeed, rb.velocity.y);
		}
		if (isGrounded && Mathf.Abs (moveDirection) < EPS)
			rb.velocity = new Vector2 (0, rb.velocity.y);

	}

	private void ShootForce(){
		timeToShoot = 0;
		while (Input.GetKey (KeyCode.Z)) {
			if (timeToShoot == 10) {
				break;
			}
			shootForce += timeToShoot*5;
			timeToShoot++;
		}
		anim.SetTrigger ("ShootingTrigger");
		coolDown = true;

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

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.layer == LayerMask.NameToLayer("Killable Objects")) {
			Kill ();
		}
	}
}
