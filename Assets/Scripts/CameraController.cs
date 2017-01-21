using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private bool startedCutscene = false;
	public float smoothTime;
	public float smoothSize;
	private float originalSize;
	private float originalTime;
	private PlayerController player;
	private Vector3 velocity = Vector3.zero;
	private bool cutsceneEnded = false;

	public bool cutscene;


	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController> ();
		originalSize = Camera.main.orthographicSize;
	}
	
	// Update is called once per frame
	void Update () {
		if (cutscene && !startedCutscene) {
			startedCutscene = false;
			StartCoroutine (SmoothFollow ());
		}

		if (cutsceneEnded) {

		}
	}


	IEnumerator SmoothFollow(){
		velocity = Vector3.zero;

		while (cutscene) {
			float posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, smoothTime);
			float posY = Mathf.SmoothDamp (transform.position.y, player.transform.position.y, ref velocity.y, smoothTime);
			float posSize = Mathf.SmoothDamp (Camera.main.orthographicSize, smoothSize, ref velocity.z, smoothTime);

			transform.position = new Vector3 (posX, posY, transform.position.z);
			Camera.main.orthographicSize = posSize;

			if (Mathf.Abs(posSize - smoothSize) < 1e-3) {
				cutscene = false;
				cutsceneEnded = true;
			}


			yield return null;
		}
	}

	IEnumerator SmoothFollow(float x, float y, float size){
		while (CheckDistance(x, y)) {
			float posX = Mathf.SmoothDamp (transform.position.x, x, ref velocity.x, smoothTime/5);
			float posY = Mathf.SmoothDamp (transform.position.y, y, ref velocity.y, smoothTime/5);
			float posSize = Mathf.SmoothDamp (Camera.main.orthographicSize, size, ref velocity.z, smoothTime/5);;

			transform.position = new Vector3 (posX, posY, transform.position.z);
			Camera.main.orthographicSize = posSize;

			if (Mathf.Abs(posSize - smoothSize) < 1e-3) {
				cutscene = false;
				cutsceneEnded = true;
			}

			yield return null;
		}
		transform.position = new Vector3 (x, y, transform.position.z);
		Camera.main.orthographicSize = size;
	}

	bool CheckDistance(float x, float y){
		if (Mathf.Abs (x - transform.position.x) < 0.05)
			return false;
		else if (Mathf.Abs (y - transform.position.y) < 0.05)
			return false;
		else
			return true;
	}
}
