using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
	private bool fadingOut = false;
	private bool fadingIn = false;
	private float Timer = 0.0f;

	private GameObject fader;
	// Use this for initialization
	void Start () {
		LoadLevel();

		fader = GameObject.Find("Fader");

		FadeIn();
	}

	void Update()
	{
		if(!fadingOut && !fadingIn)
		{
			if(Input.GetKeyUp(KeyCode.R) || Input.GetButtonUp("Fire1"))
			{
				GameObject.Find ("LevelManager").GetComponent<LevelManager>().Reset ();
			}
		}
		else
		{
			Timer += Time.deltaTime;
			if(fadingOut)
			{
				fader.renderer.material.color = new Color(
					fader.renderer.material.color.r,
					fader.renderer.material.color.g,
					fader.renderer.material.color.b,
					Mathf.Lerp(0.0f,1.0f,Timer * 4.0f)
				);

				if(fader.renderer.material.color.a == 1.0f)
				{
					Timer = 0.0f;
					fadingOut = false;
				}
			}
			else if(fadingIn)
			{
				fader.renderer.material.color = new Color(
					fader.renderer.material.color.r,
					fader.renderer.material.color.g,
					fader.renderer.material.color.b,
					Mathf.Lerp(1.0f,0.0f,Timer * 4.0f)
					);
				
				if(fader.renderer.material.color.a == 0.0f)
				{
					Timer = 0.0f;
					fadingIn = false;
				}
			}
		}
	}
	
	private void LoadLevel()
	{
		Debug.Log ("LoadLevel >> curLevel: " + LevelManager.curLevel);

		GameObject cam = GameObject.Find("Main Camera");
		cam.SetActive(false);

		MapMasterScript map = GameObject.Find("LevelManager").GetComponent<MapMasterScript>();

		if(LevelManager.curLevel == 0)
		{
			map.Load("intro.json");
			GameObject.Find ("LevelManager").GetComponent<LevelManager>().PlayLevelAudio("intro");
		}
		else
		{
			Debug.Log("level"+LevelManager.curLevel+".json");
			map.Load("level"+LevelManager.curLevel+".json");
			GameObject.Find ("LevelManager").GetComponent<LevelManager>().PlayLevelAudio("level");
		}
		
		cam.SetActive(true);
		Camera2DFollow follow = cam.GetComponent<Camera2DFollow>();
		GameObject player = GameObject.FindWithTag("Player");
//		follow.target = player.transform;
		follow.target = GameObject.Find("CameraTracker").transform;
		cam.transform.position = new Vector3(player.transform.position.x, player.transform.position.y,-10);
	}

	public void FadeOut()
	{
		fadingOut = true;
	}
	
	public void FadeIn()
	{
		fadingIn = true;
	}
}
