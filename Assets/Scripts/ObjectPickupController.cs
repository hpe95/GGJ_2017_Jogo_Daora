using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickupController : MonoBehaviour {

	public AllPossiblePickups.Pickups whatPickup;

	private PlayerController player;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController> ();
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.layer == LayerMask.NameToLayer("Player")) {
			player.addPickup (whatPickup);
			Destroy (gameObject);
		}
	}

}
