using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Side { Right, Left, Up, Down }
public enum Polarity {Positive, Negative}

public class MagnetController : MonoBehaviour {
	public Side side;
	public Polarity polarity;
	public LayerMask playerLayerMask;
	public float magneticSpeed;
	public float sizeOfEffect;

	private const float EPS = 0.1f;
	private float originalGravityScale;

	private PlayerController player;

	Dictionary<Side, Vector2> vectorSide = new Dictionary<Side, Vector2>(){
		{Side.Right, Vector2.right},
		{Side.Left, Vector2.left},
		{Side.Up, Vector2.up},
		{Side.Down, Vector2.down}
	};


	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController> ();

	}

	// Update is called once per frame
	void Update () {
		if (player.Rb.gravityScale != 0) {
			originalGravityScale = player.Rb.gravityScale;
		}
		detectPlayer ();
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
			if(player.Rb.gravityScale != 0){
				player.Rb.gravityScale = 0;
				player.Rb.velocity = new Vector2 (0, 0);
			}
			if (Mathf.Abs (player.transform.position.y - transform.position.y) < EPS) {
				player.Rb.velocity = new Vector2 (player.Rb.velocity.x, 0);
			} else if(side == Side.Left || side == Side.Right) {
				Vector2 dir;
				if (player.transform.position.y > transform.position.y) {
					dir = Vector2.down;
				} else {
					dir = Vector2.up;
				}

				player.Rb.velocity = new Vector2 (player.Rb.velocity.x, dir.y);
			}

			if (Mathf.Abs (player.transform.position.x - transform.position.x) < EPS) {
				print ("ola");
				player.Rb.velocity = new Vector2 (0, player.Rb.velocity.y);
			} else if(side == Side.Up || side == Side.Down) {
				print ("oi");
				Vector2 dir;
				if (player.transform.position.x > transform.position.x) {
					dir = Vector2.left;
				} else {
					dir = Vector2.right;
				}
					
				player.Rb.velocity = new Vector2 (dir.x, player.Rb.velocity.y);
			}

			if (player.Polarity == polarity) {
				player.Rb.AddForce (vectorSide [side] * magneticSpeed);
			} else {
				player.Rb.AddForce (-vectorSide [side] * magneticSpeed);
			}
		} else {
			player.Rb.gravityScale = originalGravityScale;
		}
			
		Debug.DrawLine (pointA, pointB);
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
