using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeManager : MonoBehaviour {
	public GameObject yellowCube;
	public GameObject purpleCube;
	public GameObject sphere;
	private Stack<GameObject> yellowCubes = new Stack<GameObject> ();
	private bool trigger = false;
	private bool purpleCubeTrigger = false;

	// Use this for initialization
	void Start () {
		StartCoroutine(CubeTimer ());
		StartCoroutine(PurpleCubeTimer ());
	}
	
	// Update is called once per frame
	void Update () {

		if (trigger) {
			SpawnYellowCubeLine ();
			StartCoroutine(CubeTimer ());
		}

		if (purpleCubeTrigger) {
			SpawnPurpleCube ();
			StartCoroutine(PurpleCubeTimer ());
		}

	}

	void CreateYellowCubeLine () {
		for (int i = 0; i < 3; i++) {
			yellowCubes.Push ((GameObject)Instantiate (yellowCube));
			yellowCubes.Peek ().SetActive (false);
		}

	}

	void SpawnPurpleCube() {
		Vector3 position = new Vector3 ();

		GameObject cube = Instantiate<GameObject> (purpleCube);
		int lane = Random.Range (0,3);
		position.z = sphere.transform.position.z + 150;
		position.y = 0.5f;

		switch (lane) {
		case 0:
			position.x = -1.5f;
			break;
		case 1:
			position.x = 0;
			break;
		default:
			position.x = 1.5f;
			break;
		}

		cube.transform.position = position;

	}

	void SpawnYellowCubeLine() {
		if (yellowCubes.Count == 0) {
			CreateYellowCubeLine ();
		}

		Vector3 position = new Vector3 ();

		position.y = 0.5f;
		GameObject tmp = yellowCubes.Pop ();
		int lane = Random.Range (0,3);
		tmp.SetActive (true);
		position.z = sphere.transform.position.z + 150;

		switch (lane) {
		case 0:
			position.x = -1.5f;
			break;
		case 1:
			position.x = 0;
			break;
		default:
			position.x = 1.5f;
			break;
		}
		tmp.transform.position = position;

		Vector3 position1 = new Vector3 (position.x, position.y, position.z + 2.0f);
		tmp = yellowCubes.Pop ();
		tmp.transform.position = position1;
		tmp.SetActive (true);

		Vector3 position2 = new Vector3 (position.x, position.y, position.z + 4.0f);
		tmp = yellowCubes.Pop ();
		tmp.transform.position = position2;
		tmp.SetActive (true);

	}

	public IEnumerator CubeTimer(){
		trigger = false;
		yield return new WaitForSeconds (4.0f);
		trigger = true;
	}

	public IEnumerator PurpleCubeTimer(){
		purpleCubeTrigger = false;
		yield return new WaitForSeconds (15.0f);
		purpleCubeTrigger = true;
	}
}
