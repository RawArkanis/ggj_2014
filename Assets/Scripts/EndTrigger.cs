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

			GameObject player = GameObject.FindWithTag("Player");
			player.GetComponent<PlatformerCharacter2D>().Lock();

			Debug.Log("Terminei a fase!");

			Invoke ("EndLevel",4);
			GameObject.Find ("LevelManager").GetComponent<LevelManager>().PlayFXAudio("win");
			GameObject.Find ("LevelManager").GetComponent<LevelManager>().FadeOutlevelAudio();
			GameObject.Find ("LevelLoader").GetComponent<LevelLoader>().FadeOut();
		}
	}

	public void EndLevel()
	{
//		((LevelManager)GameObject.Find("LevelManager").GetComponent<LevelManager>()).LoadNextLevel();
		((LevelManager)GameObject.Find("LevelManager").GetComponent<LevelManager>()).LoadNextLevelIndication();
	}
}
