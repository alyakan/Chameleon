using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaneManager : MonoBehaviour {
	/*
		Prefabrics for each lane with an anchor point to remember where
		to instantiate the next lane without any calculations.
	*/
	public GameObject leftLanePrefab;
	public GameObject currentLeftLane;

	public GameObject midLanePrefab;
	public GameObject currentMidLane;

	public GameObject rightLanePrefab;
	public GameObject currentRightLane;

	public Material redLaneMat;
	public Material blueLaneMat;
	public Material greenLaneMat;
	public Material grayLaneMat;

	public GameObject sphere;

	private float lastZPositionForSphere = 0;
	private int lanesCreated = 20;

	private float destroyTimer = 30.0f;

	// Use this for initialization
	void Start () {
		Spawn40Lanes ();
	}
	
	// Update is called once per frame
	void Update () {
		float currentZPositionForSphere = sphere.GetComponent<Renderer> ().transform.position.z;
		if (currentZPositionForSphere - lastZPositionForSphere > 10.0f) {
			/*
				Spawn Lanes whenever the sphere moves 10 points forward.
				This is to enhance memory space.
			*/
			lastZPositionForSphere = currentZPositionForSphere;
			SpawnLeftLane ();
			SpawnMidLane ();
			SpawnRightLane ();
			lanesCreated ++;
		}
	}

	public void Spawn40Lanes() {
		for (int i = 0; i < 20; i++) {
			SpawnLeftLane ();
			SpawnMidLane ();
			SpawnRightLane ();
		}
	}

	public void SpawnLeftLane() {
		/*
			Spawns Left Lane infront of the last generated Left Lane.
			Destroy lanes each 30 seconds + number of lanes created;
		*/
		Material randomMaterial = GetRandomMaterial ();
		currentLeftLane = (GameObject)Instantiate (leftLanePrefab, currentLeftLane.transform.GetChild (0).transform.GetChild (0).position, Quaternion.identity);
		currentLeftLane.transform.GetChild (0).GetComponent<Renderer> ().material = randomMaterial;
		Destroy (currentLeftLane, destroyTimer + lanesCreated);

		currentLeftLane = (GameObject)Instantiate (leftLanePrefab, currentLeftLane.transform.GetChild (0).transform.GetChild (0).position, Quaternion.identity);
		currentLeftLane.transform.GetChild (0).GetComponent<Renderer> ().material = randomMaterial;
		Destroy (currentLeftLane, destroyTimer + lanesCreated);
	}

	public void SpawnMidLane() {
		Material randomMaterial = GetRandomMaterial ();
		currentMidLane = (GameObject)Instantiate (midLanePrefab, currentMidLane.transform.GetChild (0).transform.GetChild (0).position, Quaternion.identity);
		currentMidLane.transform.GetChild(0).GetComponent<Renderer> ().material = randomMaterial;
		Destroy (currentMidLane, destroyTimer + lanesCreated);

		currentMidLane = (GameObject)Instantiate (midLanePrefab, currentMidLane.transform.GetChild (0).transform.GetChild (0).position, Quaternion.identity);
		currentMidLane.transform.GetChild(0).GetComponent<Renderer> ().material = randomMaterial;
		Destroy (currentMidLane, destroyTimer + lanesCreated);
	}

	public void SpawnRightLane() {
		Material randomMaterial = GetRandomMaterial ();
		currentRightLane = (GameObject)Instantiate (rightLanePrefab, currentRightLane.transform.GetChild (0).transform.GetChild (0).position, Quaternion.identity);
		currentRightLane.transform.GetChild(0).GetComponent<Renderer> ().material = randomMaterial;
		Destroy (currentRightLane, destroyTimer + lanesCreated);

		currentRightLane = (GameObject)Instantiate (rightLanePrefab, currentRightLane.transform.GetChild (0).transform.GetChild (0).position, Quaternion.identity);
		currentRightLane.transform.GetChild(0).GetComponent<Renderer> ().material = randomMaterial;
		Destroy (currentRightLane, destroyTimer + lanesCreated);
	}

	public Material GetRandomMaterial() {
		int x = Random.Range(0, 4);
		if (x == 0)
			return redLaneMat;
		else if (x == 1)
			return blueLaneMat;
		else if (x == 2)
			return greenLaneMat;
		else
			return grayLaneMat;
	}
		
}
