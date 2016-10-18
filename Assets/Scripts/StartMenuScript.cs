﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class StartMenuScript : MonoBehaviour {

	public Button startGameBtn;
	public Button howToPlayBtn;
	public Button creditsBtn;
	public Button quitGameBtn;

	// Use this for initialization
	void Start () {
		startGameBtn.onClick.AddListener(() => StartGame());
		quitGameBtn.onClick.AddListener(() => QuitGame());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void StartGame()
	{
		transform.GetChild (2).GetComponent<AudioSource> ().Play ();
		StartCoroutine (WaitForSound (transform.GetChild (2).GetComponent<AudioSource> ().clip.length));
	}

	IEnumerator WaitForSound(float time)
	{
		yield return new WaitForSeconds (time);
		SceneManager.LoadScene ("GameWorld");
	}

	void HowToPlay() 
	{
		
	}

	void Credits()
	{
		
	}

	void QuitGame()
	{
		print ("QUIT");
		Application.Quit ();
		
	}
}