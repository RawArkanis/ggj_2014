using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public static int curLevel = 0;
//	public static int curLevel = 9;
	public static int maxLevel = 10;

	public AudioClip intro;
	public AudioClip level;
	public AudioClip win;
	public AudioClip danger;

	private AudioSource levelAudio;
	private AudioSource fxAudio;

	private bool fadingLevelAudio = false;
	private float Timer = 0.0f;

	void Awake()
	{
		DontDestroyOnLoad(this);
	}
	
	void Start()
	{
		levelAudio = GameObject.Find("AudioLevel").GetComponent<AudioSource>();
		fxAudio = GameObject.Find("AudioFX").GetComponent<AudioSource>();

		Application.LoadLevel("levelIntro");
	}

	void Update()
	{
		if(fadingLevelAudio)
		{
			Timer += Time.deltaTime;

			levelAudio.volume = Mathf.Lerp(1.0f,0.0f,Timer * 4.0f);

			if(levelAudio.volume == 0.0f)
			{
				fadingLevelAudio = false;
				Timer = 0.0f;
				levelAudio.clip = null;
				levelAudio.volume = 1.0f;
			}
		}
	}

	public void Reset()
	{
		Application.LoadLevel("levelBase");
	}
	
	public void LoadNextLevelIndication()
	{
		fxAudio.clip = null;
		LevelManager.curLevel++;
		if(LevelManager.curLevel <= LevelManager.maxLevel)
			Application.LoadLevel("levelIndicator");
		else
			Application.Quit();

	}

	public void LoadNextLevel()
	{
		Application.LoadLevel("levelBase");
	}

	public void PlayLevelAudio(string clipType)
	{
		switch(clipType)
		{
		case "intro":
			levelAudio.clip = intro;
			break;
		case "level":
			if(levelAudio.clip == level) return;
			levelAudio.clip = level;
			break;
		}

		levelAudio.Play ();
	}
	
	public void PlayFXAudio(string clipType)
	{
		switch(clipType)
		{
		case "win":
			fxAudio.clip = win;
			break;
		case "danger":
			fxAudio.clip = danger;
			break;
		}
		
		fxAudio.Play ();
	}

	public void FadeOutlevelAudio()
	{
		if(LevelManager.curLevel == 0) fadingLevelAudio = true;
	}
}
