using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveForward : MonoBehaviour {
	public float speed = 4f;
	public bool accelerate = false;
	public LaneManager laneManager;


	public Text scoreText;
	private int score = 100;
	// Use this for initialization
	void Start () {
		StartCoroutine(AccelrationTimer());
		GetComponent<Rigidbody>().AddForce(0f, 0f, 1.5f * speed);
	}

	// Update is called once per frame
	void Update () {
		scoreText.text = "Score: " + score;
		transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
		if (speed < 20.0f && accelerate) {
			speed+=1.0f; // will make the update method pick up
			StartCoroutine(AccelrationTimer());
		}


	}

	void OnTriggerEnter(Collider other) {
		switch (other.gameObject.tag) {
		case "YellowCube":
			other.gameObject.SetActive(false);
			score += 20;
			break;
		case "PurpleCube":
			other.gameObject.SetActive(false);
			laneManager.TurnAllLanesToGray();
			break;
		case "Lane":
			CheckForColorMismatchAndPunish (other.gameObject.transform.GetChild (0).GetComponent<Renderer> ().material);
			break;
		default:
			break;
		}

	}

	private void CheckForColorMismatchAndPunish(Material laneMat) {
		/*
			Check for color mismatch between sphere and lane.
		*/
		Material sphereMat = laneManager.sphere.GetComponent<Renderer> ().material;
		if (laneMat.name != sphereMat.name && laneMat.name != "Gray (Instance)") {
			StartCoroutine (TriggerPunishment (laneMat));
			if (score <= 1) {
				// TODO: Gameover
			}	
		}
	}

	private bool CheckForColorMismatch(Material laneMat) {
		Material sphereMat = laneManager.sphere.GetComponent<Renderer> ().material;
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
