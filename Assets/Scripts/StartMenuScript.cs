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
	public Button doneCredits;

	public Animator instructionsAnim;
	public Animator creditsAnim;

	// Use this for initialization
	void Start () {
		startGameBtn.onClick.AddListener(() => StartGame());
		howToPlayBtn.onClick.AddListener(() => HowToPlay());
		creditsBtn.onClick.AddListener(() => Credits());
		quitGameBtn.onClick.AddListener(() => QuitGame());
		doneInstructions.onClick.AddListener(() => DismissInstructions());
		doneCredits.onClick.AddListener(() => DismissCredits());
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
		creditsAnim.SetTrigger ("ViewCredits");
	}

	void QuitGame()
	{
		print ("QUIT");
		Application.Quit ();
		
	}

	void DismissInstructions()
	{
		instructionsAnim.SetTrigger ("DismissInstructions");
	}

	void DismissCredits()
	{
		creditsAnim.SetTrigger ("DismissCredits");
	}
}
