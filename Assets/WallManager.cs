using UnityEngine;
using System.Collections;

public class WallManager : MonoBehaviour {
	public GameObject rightWallPrefab;
	public GameObject currentRightWall;

	public GameObject leftWallPrefab;
	public GameObject currentLeftWall;

	public GameObject leftWallLightPrefab;
	public GameObject currentLeftWallLight;

	public GameObject rightWallLightPrefab;
	public GameObject currentRightWallLight;

	public GameObject lightColor;

	public Material blue;
	public Material red;
	public Material green;

	private bool trigger = false;

	// Use this for initialization
	void Start () {
		StartCoroutine(DoTheDance());
	}
	
	// Update is called once per frame
	void Update () {
		ChangeLightColor ();
		if (trigger) {
			SpawnRightWall ();
			SpawnLeftWall ();
			StartCoroutine(DoTheDance());
		}

	}

	void ChangeLightColor() {
		GameObject[] lighting = GameObject.FindGameObjectsWithTag("Wall Light");

		if (Input.GetKeyDown ("q"))
			foreach(GameObject light in lighting) {
				lightColor.GetComponent<Light> ().color = red.color;
				light.GetComponent<Light> ().color = red.color;
			}		
		if (Input.GetKeyDown("w"))
			foreach(GameObject light in lighting) {
				lightColor.GetComponent<Light> ().color = blue.color;
				light.GetComponent<Light> ().color = blue.color;
			}
		if (Input.GetKeyDown("e"))
			foreach(GameObject light in lighting) {
				lightColor.GetComponent<Light> ().color = green.color;
				light.GetComponent<Light> ().color = green.color;
			}
	}

	void SpawnRightWall() {
		currentRightWall = (GameObject)Instantiate (
			rightWallPrefab,
			currentRightWall.transform.GetChild (0).transform.GetChild (0).position,
			Quaternion.identity);

		Vector3 lightPosition = new Vector3(
			currentRightWallLight.transform.position.x,
			currentRightWallLight.transform.position.y,
			currentRightWall.transform.GetChild (0).transform.GetChild (0).position.z);
		currentRightWallLight = (GameObject)Instantiate (
			rightWallLightPrefab,
			lightPosition,
			Quaternion.identity);
		currentRightWallLight.tag = "Wall Light";
		currentRightWallLight.GetComponent<Light> ().color = lightColor.GetComponent<Light> ().color;
	}

	void SpawnLeftWall() {
		currentLeftWall = (GameObject)Instantiate (
			leftWallPrefab,
			currentLeftWall.transform.GetChild (0).transform.GetChild (0).position,
			Quaternion.identity);
		
		Vector3 lightPosition = new Vector3(
			currentLeftWallLight.transform.position.x,
			currentLeftWallLight.transform.position.y,
			currentLeftWall.transform.GetChild (0).transform.GetChild (0).position.z);
		currentLeftWallLight = (GameObject)Instantiate (
			leftWallLightPrefab,
			lightPosition,
			Quaternion.identity);
		currentLeftWallLight.tag = "Wall Light";
		currentLeftWallLight.GetComponent<Light> ().color = lightColor.GetComponent<Light> ().color;
	}

	private IEnumerator DoTheDance() {
		/*
			Timer for lane spawning that activates for each 0.01 seconds.
		*/
		trigger = false;
		yield return new WaitForSeconds(0.01f); // waits 0.01 seconds
		trigger = true; // will make the update method pick up 
	}
}
