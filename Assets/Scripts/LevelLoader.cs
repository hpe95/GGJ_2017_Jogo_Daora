using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ColorToPrefab{
	public Color32 color;
	public GameObject prefab;
}


public class LevelLoader : MonoBehaviour {
	public string levelName;
	public ColorToPrefab[] colorToPrefab;

	private Dictionary<Color32, GameObject> colorToPrefabForReal;
	private Texture2D levelMap;

	// Use this for initialization
	void Start () {
		levelMap = Resources.Load (levelName) as Texture2D;
		loadDictionary ();
		LoadMap ();
	}

	void loadDictionary(){
		colorToPrefabForReal = new Dictionary<Color32, GameObject> ();
		foreach (ColorToPrefab ctp in colorToPrefab) {
			colorToPrefabForReal.Add(ctp.color, ctp.prefab);
		}
	}

	void EmptyMap(){
		// Destroy all children, cleaning the level.

		while(transform.childCount > 0){
			Transform c = transform.GetChild (0);
			c.SetParent (null);
			Destroy (c.gameObject);
		}
	}

	void LoadMap(){
		EmptyMap ();

		Color32[] allPixels = levelMap.GetPixels32 ();
		int width = levelMap.width;
		int height = levelMap.height;

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				int index = x + (y * width);

				spawnTileAt (allPixels [index], x, y);
			}
		}
	}

	void spawnTileAt(Color32 c, int x, int y){
		if (c.a <= 0) {
			return;
		}

		try{
			GameObject prefab = colorToPrefabForReal[c];
			GameObject go = Instantiate (prefab, new Vector3 (x, y, 0), prefab.transform.rotation);
			go.transform.SetParent(transform);
		}
		catch(System.Exception e){
			print (e);
			print ("Could not find this color in the hash table: " + c.ToString() + ".");
		}
	}
}
