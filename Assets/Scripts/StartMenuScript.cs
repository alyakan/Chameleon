using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class StartMenuScript : MonoBehaviour {

	public Button startGameBtn;
	public Button howToPlayBtn;
	public Button creditsBtn;
	public Button quitGameBtn;
	public Button doneInstructions;

	public Animator instructionsAnim;

	// Use this for initialization
	void Start () {
		startGameBtn.onClick.AddListener(() => StartGame());
		howToPlayBtn.onClick.AddListener(() => HowToPlay());
		quitGameBtn.onClick.AddListener(() => QuitGame());
		doneInstructions.onClick.AddListener(() => DoneInstructions());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void StartGame()
	{
		transform.GetChild (0).transform.GetChild(1).GetComponent<AudioSource> ().Play ();
		StartCoroutine (WaitForSound (transform.GetChild (0).transform.GetChild(1).GetComponent<AudioSource> ().clip.length));
	}

	IEnumerator WaitForSound(float time)
	{
		yield return new WaitForSeconds (time);
		SceneManager.LoadScene ("GameWorld");
	}

	void HowToPlay() 
	{
		instructionsAnim.SetTrigger ("ViewInstructions");
	}

	void Credits()
	{
		
	}

	void QuitGame()
	{
		print ("QUIT");
		Application.Quit ();
		
	}

	void DoneInstructions()
	{
		instructionsAnim.SetTrigger ("DismissInstructions");
	}
}
