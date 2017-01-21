using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Side { Right, Left, Up, Down }
public enum Polarity {Positive, Negative}

public class MagnetController : MonoBehaviour {
	public Side side;
	public Polarity polarity;
	public LayerMask playerLayerMask;

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
		detectPlayer ();
	}

	private void detectPlayer(){
		Vector2 pointA = transform.position;
		pointA += vectorSide [side]/2; // Só ta funcionando pq a sprite ta como um quadrado, arrumar isso aqui é bom
		pointA += Rotate(vectorSide [side]/2, 90);

		Vector2 pointB = transform.position;
		pointB += vectorSide [side] * 5;
		pointB += Rotate(-vectorSide [side]/2, 90);

		Collider2D maybePlayer = Physics2D.OverlapArea (pointA, pointB, 1 << LayerMask.NameToLayer("Player"));
		if (maybePlayer != null) {
			if (player.Polarity == polarity) {

			}
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
	
}
