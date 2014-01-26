using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
		LoadLevel();
	}
	
	public void LoadLevel()
	{
		Debug.Log ("LoadLevel >> curLevel: " + LevelManager.curLevel);

		GameObject cam = GameObject.Find("Main Camera");
		cam.SetActive(false);

		MapMasterScript map = GameObject.Find("LevelManager").GetComponent<MapMasterScript>();

		if(LevelManager.curLevel == 0)
			map.Load("intro.json");
		else
		{
			Debug.Log("level"+LevelManager.curLevel+".json");
			map.Load("level"+LevelManager.curLevel+".json");
		}
		
		cam.SetActive(true);
		Camera2DFollow follow = cam.GetComponent<Camera2DFollow>();
		GameObject player = GameObject.FindWithTag("Player");
		follow.target = player.transform;
		cam.transform.position = new Vector3(player.transform.position.x, player.transform.position.y,-10);
	}
}
