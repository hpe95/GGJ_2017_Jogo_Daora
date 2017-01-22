using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Side { Right, Up, Left, Down }
public enum Polarity {Positive, Negative}

public class MagnetController : MonoBehaviour {
	public Side side;
	public Polarity polarity;

	public Polarity Polarity {
		get {
			return polarity;
		}
		set {
			
			polarity = value;
		}
	}

	public float magneticSpeed;
	public float sizeOfEffect;
	public bool isRotational;
	public float secondsToWait;

	public bool online = true;
	private const float EPS = 0.1f;
	private float originalGravityScale;
	private bool isRotating = false;
	private PlayerController player;
	private SpriteRenderer sr;
	private Animator anim;
	private bool isBlue = false;
	private bool transistion;
	Dictionary<Side, Vector2> vectorSide = new Dictionary<Side, Vector2>(){
		{Side.Right, Vector2.right},
		{Side.Left, Vector2.left},
		{Side.Up, Vector2.up},
		{Side.Down, Vector2.down}
	};


	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController> ();
		sr = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();



		if (polarity == Polarity.Positive) {
			isBlue = false;
		} else {
			isBlue = true;
		}
	}

	// Update is called once per frame
	void Update () {
		if (online) {
			if (polarity == Polarity.Positive) {
				isBlue = false;
			} else {
				isBlue = true;
			}
			anim.SetBool ("isOrange", polarity == Polarity.Positive);
			bool qlqrcoisa = polarity == Polarity.Negative;
			if (player.Rb.gravityScale != 0) {
				originalGravityScale = player.Rb.gravityScale;
			}
			if (!player.isDead) {
				detectPlayer ();
			}
			if (isRotational && !isRotating) {
				isRotating = true;
				StartCoroutine (Rotate ());
			}
			anim.SetBool ("transistion", isRotating == !qlqrcoisa);
		}
	}

	private IEnumerator Rotate(){
		yield return new WaitForSeconds (secondsToWait);
		polarity = (Polarity) (((int)polarity + 1) % 2);
		isRotating = false;
	}

	private void detectPlayer(){
		Vector2 pointA = transform.position;
		pointA += vectorSide [side]/2; // Só ta funcionando pq a sprite ta como um quadrado, arrumar isso aqui é bom
		pointA += Rotate(vectorSide [side]/2, 90);

		Vector2 pointB = transform.position;
		pointB += vectorSide [side] * sizeOfEffect;
		pointB += Rotate(-vectorSide [side]/2, 90);

		Collider2D maybePlayer = Physics2D.OverlapArea (pointA, pointB, 1 << LayerMask.NameToLayer("Player"));
		if (maybePlayer != null) {
			player.isOnMagneticField = true;

			if(player.Rb.gravityScale != 0){
				//player.Rb.gravityScale = 0;
				//player.Rb.velocity = new Vector2 (0, 0);
			}

			if (player.Polarity == polarity) {
				player.Rb.AddForce (vectorSide [side] * magneticSpeed);
			} else {
				player.Rb.AddForce (-vectorSide [side] * magneticSpeed);
			}

		} else {
			// player.Rb.gravityScale = originalGravityScale;
			if(player.isOnMagneticField){
				Vector2 distV = player.transform.position - transform.position;
				if (Mathf.Abs (distV.magnitude - sizeOfEffect) < 0.6f) {
					StartCoroutine (disableForAInstant ());
				}
			}
			player.isOnMagneticField = false;
		}
	}

	private IEnumerator disableForAInstant(){
		online = false;
		yield return new WaitForSeconds (0.5f);
		online = true;
	}

	private Vector2 Rotate(Vector2 v, float degrees){
		float sin = Mathf.Sin (degrees * Mathf.Deg2Rad);
		float cos = Mathf.Cos (degrees * Mathf.Deg2Rad);

		float tx = v.x;
		float ty = v.y;

		v.x = (cos * tx) - (sin * ty);
		v.y = (sin * tx) + (cos * ty);

		return v;
	}

	private float easeInOutSine(float t, float b, float c, float d){
		return -c / 2 * (Mathf.Cos (Mathf.PI * t / d) - 1) + b;
	}
	
}