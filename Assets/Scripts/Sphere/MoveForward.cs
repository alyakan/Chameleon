using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveForward : MonoBehaviour {
	public float speed = 4f;
	public bool accelerate = false;
	private float spawnLaneTimer = 3.0f;
	public bool isDead = false;
	private int highestScore = 100;
	private float lastZPosition = 0;
	private float currentZPosition = 0;
	private bool shouldRecyclePlane = false;

	public Text scoreText;
	public Button pauseGame;
	public Button retry;
	public Text pauseButtonText;
	public Canvas canvas;
	public Animator gameOverAnim;
	public ParentLaneManager parentLaneManager;
	public Jump jumpScript;
	public Text menuScoreText;
	public Text menuBestText;

	private Collider laneToBeSpawned;

	private int score = 100;
	private bool paused = false;
	// Use this for initialization
	void Start () {
		StartCoroutine(AccelrationTimer());
		GetComponent<Rigidbody>().AddForce(0f, 0f, 1.5f * speed);
		pauseGame.onClick.AddListener(() => PauseGame());
		retry.onClick.AddListener(() => Retry());
	}

	// Update is called once per frame
	void Update () {

		if (score == 1) {
			isDead = true;
			gameOverAnim.SetTrigger ("GameOver");
			menuScoreText.text = highestScore + "";
		}

		scoreText.text = "Score: " + score;
		transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);

		if (speed < 20.0f && accelerate) {
			speed+=1.0f; // will make the update method pick up
			StartCoroutine(AccelrationTimer());
		}
			
	}

	void FixedUpdate() {
		currentZPosition = transform.position.z;

		if (currentZPosition - lastZPosition >= 4.5) {
			/*
				Only recycle plane when sphere moves forward by 4.5 points
				to prevent recycling when switching between planes.
			*/
			print ("Should Recycle");
			shouldRecyclePlane = true;
			lastZPosition = currentZPosition;
		}
	}

	void PauseGame() {
		if (!paused) {
			Time.timeScale = 0.0f;
			pauseButtonText.text = "Resume";
			paused = true;
		} else {
			Time.timeScale = 1.0f;
			pauseButtonText.text = "Pause";
			paused = false;
		}

	}

	void Retry() {
		Application.LoadLevel (Application.loadedLevel);
	}

	void OnTriggerEnter(Collider other) {
		switch (other.gameObject.tag) {
		case "YellowCube":
			other.gameObject.SetActive (false);
			score += 20;
			if (score > highestScore)
				highestScore = score;
			break;
		case "PurpleCube":
			other.gameObject.SetActive(false);
			parentLaneManager.TurnToGray();
			break;
		case "Lane":
			CheckForColorMismatchAndPunish (other.gameObject.transform.GetChild (0).GetComponent<Renderer> ().material);
			break;
		default:
			break;
		}

	}

	void OnTriggerExit(Collider other) {
		switch (other.gameObject.tag) {
		case "Lane":
			if (shouldRecyclePlane) {
				StartCoroutine (LaneSpawnTimer (other));
				shouldRecyclePlane = false;
			}
			break;
		default:
			break;
		}

	}

	private void SpawnNewLane(Collider other)
	{
		if (parentLaneManager.parentLanes.Count == 0)
			parentLaneManager.CreateParentLane (1);

		other.gameObject.transform.parent.gameObject.transform.parent.gameObject.SetActive (false);
		parentLaneManager.parentLanes.Push (other.gameObject.transform.parent.gameObject.transform.parent.gameObject);

		Material[] materials = new Material[3];

		materials [0] = parentLaneManager.GetRandomMaterial ();
		materials [1] = parentLaneManager.GetRandomMaterial ();
		materials [2] = parentLaneManager.GetRandomMaterial ();

		parentLaneManager.SpawnParentLane (materials);

		if (parentLaneManager.parentLanes.Count == 0)
			parentLaneManager.CreateParentLane (1);
		parentLaneManager.SpawnParentLane (materials);
			
	}

	private IEnumerator LaneSpawnTimer(Collider other) {
		/*
			Timer to allow user changing lanes before decreasing score.
		*/
		yield return new WaitForSeconds(spawnLaneTimer);
		SpawnNewLane (other);
		if (spawnLaneTimer > 0.8f)
			spawnLaneTimer -= 0.05f;
	}
		
	private void CheckForColorMismatchAndPunish(Material laneMat) {
		/*
			Check for color mismatch between sphere and lane.
		*/
		Material sphereMat = gameObject.GetComponent<Renderer> ().material;
		if (laneMat.name != sphereMat.name && laneMat.name != "Gray (Instance)") {
			StartCoroutine (TriggerPunishment (laneMat));
		}
	}

	private bool CheckForColorMismatch(Material laneMat) {
		Material sphereMat = gameObject.GetComponent<Renderer> ().material;
		return (laneMat.name != sphereMat.name && laneMat.name != "Gray (Instance)");
	}

	private IEnumerator TriggerPunishment(Material laneMat) {
		/*
			Timer to allow user changing lanes before decreasing score.
		*/
		yield return new WaitForSeconds(0.4f);
		if (CheckForColorMismatch(laneMat))
			score -= score / 2;
	}
	private IEnumerator AccelrationTimer() {
		/*
			Timer for accelrating sphere.
		*/
		accelerate = false;
		yield return new WaitForSeconds(5f);
		accelerate = true;
	}
}
