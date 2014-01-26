using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

//	public static int curLevel = 0;
	public static int curLevel = 3;

	void Awake()
	{
		DontDestroyOnLoad(this);
	}
	
	void Start()
	{
		Application.LoadLevel("levelBase");
	}

	public void LoadNextLevel()
	{
		LevelManager.curLevel++;
		Application.LoadLevel("levelBase");
	}
}
