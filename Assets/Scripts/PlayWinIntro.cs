using UnityEngine;
using System.Collections;

public class PlayWinIntro : MonoBehaviour
{
	private bool played = false;
	void OnTriggerEnter2D (Collider2D coll)
	{
		if(coll.gameObject.tag == "Player")
		{
			if(played) return;
			played = true;
			GameObject.Find ("LevelManager").GetComponent<LevelManager>().PlayFXAudio("win");
		}
	}
}
