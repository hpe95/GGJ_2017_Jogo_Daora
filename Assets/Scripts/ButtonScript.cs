using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {

	public void Next(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
