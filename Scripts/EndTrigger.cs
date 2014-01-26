using UnityEngine;
using System.Collections;

public class EndTrigger : MonoBehaviour {
	private bool used = false;
	// Use this for initialization
	void OnTriggerEnter2D (Collider2D coll)
	{
		if(coll.gameObject.tag == "Player")
		{
			if(used) return;

			used = true;

			Debug.Log("Terminei a fase!");
			((LevelManager)GameObject.Find("LevelManager").GetComponent<LevelManager>()).LoadNextLevel();
		}
	}
}
