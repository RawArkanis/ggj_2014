using UnityEngine;
using System.Collections;

public class LevelIndicator : MonoBehaviour
{
	private bool fadingOut = false;
	private float Timer = 0.0f;
	
	private GameObject fader;
	// Use this for initialization
	void Start () {
		LoadIndication();
		
		fader = GameObject.Find("Fader");
		fader.renderer.material.color = new Color(
			fader.renderer.material.color.r,
			fader.renderer.material.color.g,
			fader.renderer.material.color.b,
			0.0f
			);
	}
	
	void Update()
	{
		if(fadingOut)
		{
			Timer += Time.deltaTime;
			
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
	}
	
	private void LoadIndication()
	{
		Debug.Log ("LoadLevel >> curLevel: " + LevelManager.curLevel);
		Invoke ("FadeOut",4);
		Invoke ("EndIndication",8);

		GameObject cam = GameObject.Find("Main Camera");
		Camera2DFollow follow = cam.GetComponent<Camera2DFollow>();
		follow.target = GameObject.Find("CameraTracker").transform;

		GetComponent<TextMesh>().text = "Level " + LevelManager.curLevel;
	}
	
	public void FadeOut()
	{
		fadingOut = true;
	}

	public void EndIndication()
	{
		((LevelManager)GameObject.Find("LevelManager").GetComponent<LevelManager>()).LoadNextLevel();
	}
}
