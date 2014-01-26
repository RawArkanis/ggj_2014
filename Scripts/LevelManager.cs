using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public static int curLevel = 0;
//	public static int curLevel = 2;
	public static int maxLevel = 4;

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
		if(LevelManager.curLevel <= LevelManager.maxLevel)
			Application.LoadLevel("levelBase");
		else
			Application.Quit();
	}
}
