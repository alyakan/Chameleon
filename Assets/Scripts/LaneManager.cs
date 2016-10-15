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

	private bool trigger = false;

	// Use this for initialization
	void Start () {
		StartCoroutine(DoTheDance());
	}
	
	// Update is called once per frame
	void Update () {
		if (trigger) {
			SpawnLeftLane ();
			SpawnMidLane ();
			SpawnRightLane ();
			StartCoroutine(DoTheDance());
		}
	}

	public void SpawnLeftLane() {
		currentLeftLane = (GameObject)Instantiate (leftLanePrefab, currentLeftLane.transform.GetChild (0).transform.GetChild (0).position, Quaternion.identity);
		currentLeftLane.transform.GetChild(0).GetComponent<Renderer> ().material = GetRandomMaterial ();
		Destroy (currentLeftLane, 50.0f);
	}

	public void SpawnMidLane() {
		currentMidLane = (GameObject)Instantiate (midLanePrefab, currentMidLane.transform.GetChild (0).transform.GetChild (0).position, Quaternion.identity);
		currentMidLane.transform.GetChild(0).GetComponent<Renderer> ().material = GetRandomMaterial ();
		Destroy (currentMidLane, 50.0f);
	}

	public void SpawnRightLane() {
		currentRightLane = (GameObject)Instantiate (rightLanePrefab, currentRightLane.transform.GetChild (0).transform.GetChild (0).position, Quaternion.identity);
		currentRightLane.transform.GetChild(0).GetComponent<Renderer> ().material = GetRandomMaterial ();
		Destroy (currentRightLane, 50.0f);
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

	private IEnumerator DoTheDance() {
		/*
			Timer for lane spawning that activates for each 0.01 seconds.
		*/
		trigger = false;
		yield return new WaitForSeconds(0.2f); // waits 0.01 seconds
		trigger = true; // will make the update method pick up 
	}
}
