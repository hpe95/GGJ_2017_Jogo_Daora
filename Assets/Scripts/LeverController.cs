using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour {

	public int x, y;

	private bool ehnois = false;
	private bool bope = false;
	private MagnetController magnetController;


	void Start(){
		
	}

	// Update is called once per frame
	void Update () {



		if (!bope) { 
			Collider2D col = Physics2D.OverlapCircle (new Vector2 (x, y), 1, 1 <<LayerMask.NameToLayer ("Ground"));
			GameObject magnet = col.gameObject;
			magnetController = magnet.GetComponent<MagnetController> ();
			bope = true;
			magnetController.online = ehnois;
		} else {
			Collider2D col = Physics2D.OverlapCircle (transform.position, 2, 1 <<LayerMask.NameToLayer ("Player"));
			if (col != null) {
				if (Input.GetKeyDown (KeyCode.X)) {
					ehnois = true;
					print ("yduasgdsuya");
				}
			}
			magnetController.online = ehnois;
		}
	}
}
