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

	public GameObject sphere;

	private float lastZPositionForSphere = 0;
	private int wallsCreated = 20;
	private float destroyTimer = 30f;

	// Use this for initialization
	void Start () {
		Spawn30Walls ();
	}
	
	// Update is called once per frame
	void Update () {
		ChangeLightColor ();
		float currentZPositionForSphere = sphere.GetComponent<Renderer> ().transform.position.z;
		if (currentZPositionForSphere - lastZPositionForSphere > 10.0f) {
			/*
				Spawn Walls whenever the sphere moves 10 points forward.
				This is to enhance memory space.
			*/
			lastZPositionForSphere = currentZPositionForSphere;
			SpawnRightWall ();
			SpawnLeftWall ();
		}
		if (trigger) {
			SpawnRightWall ();
			SpawnLeftWall ();
		}

	}

	public void Spawn30Walls() {
		for (int i = 0; i < 30; i++) {
			SpawnLeftWall ();
			SpawnRightWall ();
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
		wallsCreated += 1;
		Destroy (currentRightWall, destroyTimer + wallsCreated);

		Vector3 lightPosition = new Vector3(
			currentRightWallLight.transform.position.x,
			currentRightWallLight.transform.position.y,
			currentRightWall.transform.GetChild (0).transform.GetChild (0).position.z);
		currentRightWallLight = (GameObject)Instantiate (
			rightWallLightPrefab,
			lightPosition,
			Quaternion.identity);
		Destroy (currentRightWallLight, destroyTimer + wallsCreated);
		currentRightWallLight.tag = "Wall Light";
		currentRightWallLight.GetComponent<Light> ().color = lightColor.GetComponent<Light> ().color;
	}

	void SpawnLeftWall() {
		currentLeftWall = (GameObject)Instantiate (
			leftWallPrefab,
			currentLeftWall.transform.GetChild (0).transform.GetChild (0).position,
			Quaternion.identity);
		wallsCreated += 1;
		Destroy (currentLeftWall, destroyTimer + wallsCreated);
		
		Vector3 lightPosition = new Vector3(
			currentLeftWallLight.transform.position.x,
			currentLeftWallLight.transform.position.y,
			currentLeftWall.transform.GetChild (0).transform.GetChild (0).position.z);
		currentLeftWallLight = (GameObject)Instantiate (
			leftWallLightPrefab,
			lightPosition,
			Quaternion.identity);
		Destroy (currentLeftWallLight, destroyTimer + wallsCreated);
		currentLeftWallLight.tag = "Wall Light";
		currentLeftWallLight.GetComponent<Light> ().color = lightColor.GetComponent<Light> ().color;
	}

}
