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

	private Stack<GameObject> leftLanes = new Stack<GameObject> ();
	private Stack<GameObject> midLanes = new Stack<GameObject> ();
	private Stack<GameObject> rightLanes = new Stack<GameObject> ();

	// Use this for initialization
	void Start () {
		CreateLanes (60);
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
			Material randomMaterial = GetRandomMaterial ();
			SpawnLeftLane (randomMaterial);
			SpawnLeftLane (randomMaterial);
			randomMaterial = GetRandomMaterial ();
			SpawnMidLane (randomMaterial);
			SpawnMidLane (randomMaterial);
			randomMaterial = GetRandomMaterial ();
			SpawnRightLane (randomMaterial);
			SpawnRightLane (randomMaterial);
//			lanesCreated ++;
		}
	}

	public void Spawn40Lanes() {
		for (int i = 0; i < 20; i++) {
			Material randomMaterial = GetRandomMaterial ();
			SpawnLeftLane (randomMaterial);
			SpawnLeftLane (randomMaterial);
			randomMaterial = GetRandomMaterial ();
			SpawnMidLane (randomMaterial);
			SpawnMidLane (randomMaterial);
			randomMaterial = GetRandomMaterial ();
			SpawnRightLane (randomMaterial);
			SpawnRightLane (randomMaterial);
		}
	}

	public void CreateLanes(int amount) {
		/* Creates Lanes and pushes them on the stack */
		for (int i = 0; i < amount; i++) {
			leftLanes.Push ((GameObject)Instantiate (leftLanePrefab));
			midLanes.Push ((GameObject)Instantiate (midLanePrefab));
			rightLanes.Push ((GameObject)Instantiate (rightLanePrefab));

			/* Disable these tiles until spawned. */
			leftLanes.Peek ().SetActive (false);
			midLanes.Peek ().SetActive (false);
			rightLanes.Peek ().SetActive (false);
		}
	}

	public void SpawnLeftLane(Material material) {
		/*
			Spawns Left Lane infront of the last generated Left Lane.
			Destroy lanes each 30 seconds + number of lanes created;
		*/
		if (leftLanes.Count == 0) {
			CreateLanes (2);
		}

		GameObject tmp = leftLanes.Pop ();
		tmp.SetActive (true);
		tmp.transform.position = currentLeftLane.transform.GetChild (0).transform.GetChild (0).position;
		currentLeftLane = tmp;
		currentLeftLane.transform.GetChild (0).GetComponent<Renderer> ().material = material;

		PushToLeftLaneStack (destroyTimer + lanesCreated, currentLeftLane);
	}

	public void SpawnMidLane(Material material) {
		if (midLanes.Count == 0) {
			CreateLanes (2);
		}

		GameObject tmp = midLanes.Pop ();
		tmp.SetActive (true);
		tmp.transform.position = currentMidLane.transform.GetChild (0).transform.GetChild (0).position;
		currentMidLane = tmp;
		currentMidLane.transform.GetChild (0).GetComponent<Renderer> ().material = material;

		PushToMidLaneStack (destroyTimer + lanesCreated, currentMidLane);
	}

	public void SpawnRightLane(Material material) {
		if (rightLanes.Count == 0) {
			CreateLanes (2);
		}

		GameObject tmp = rightLanes.Pop ();
		tmp.SetActive (true);
		tmp.transform.position = currentRightLane.transform.GetChild (0).transform.GetChild (0).position;
		currentRightLane = tmp;
		currentRightLane.transform.GetChild (0).GetComponent<Renderer> ().material = material;

		PushToRightLaneStack (destroyTimer + lanesCreated, currentRightLane);
	}

	private IEnumerator PushToLeftLaneStack(float timer, GameObject obj) {
		/*
			For recycling lanes.
		*/
		yield return new WaitForSeconds(timer);
		obj.SetActive (false);
		leftLanes.Push (obj);
	}

	private IEnumerator PushToMidLaneStack(float timer, GameObject obj) {
		yield return new WaitForSeconds(timer);
		obj.SetActive (false);
		midLanes.Push (obj);
	}

	private IEnumerator PushToRightLaneStack(float timer, GameObject obj) {
		yield return new WaitForSeconds(timer);
		obj.SetActive (false);
		rightLanes.Push (obj);
	}

	public void TurnAllLanesToGray() {
		GameObject[] lanes = GameObject.FindGameObjectsWithTag("Lane");
		Material[]  materials = Resources.FindObjectsOfTypeAll<Material> ();
		StartCoroutine(TurnToGray(materials, lanes));

	}

	public IEnumerator TurnToGray(Material[] materials, GameObject[] lanes) {
		yield return new WaitForSeconds (0.5f);
		foreach (Material mat in materials) {
			int counter = 0;
			foreach(GameObject lane in lanes) {
				//			print (lane.transform.GetChild (0).GetComponent<Renderer> ().material.name);
				print(mat.name);

				if (mat.name == "Gray" && counter < 30)
				if (lane.transform.GetChild (0).GetComponent<Renderer> ().transform.position.z >= sphere.transform.position.z) {
					lane.transform.GetChild (0).GetComponent<Renderer> ().sharedMaterial = mat;
					counter++;
				}
					
			}
			if (mat.name == "Gray") {
				break;
			}
		}
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
