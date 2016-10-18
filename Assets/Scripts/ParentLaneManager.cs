using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParentLaneManager : MonoBehaviour {

	public GameObject parentLanePrefab;
	public GameObject currentParentLane;

	public GameObject leftLanePrefab;
	public GameObject midLanePrefab;
	public GameObject rightLanePrefab;

	public Material redLaneMat;
	public Material blueLaneMat;
	public Material greenLaneMat;
	public Material grayLaneMat;

	public GameObject sphere;

	public Stack<GameObject> parentLanes = new Stack<GameObject> ();

	// Use this for initialization
	void Start () {
		CreateParentLane (30);
		Material[] materials = new Material[3];

		for (int i = 0; i < 15; i++) 
		{
			materials [0] = GetRandomMaterial ();
			materials [1] = GetRandomMaterial ();
			materials [2] = GetRandomMaterial ();
			SpawnParentLane (materials);
			SpawnParentLane (materials);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CreateParentLane(int amount)
	{
		/*
			Instantiate a ParentLane and push it into a stack.
			Will be used for spawning.
		*/
		for (int i = 0; i < amount; i++) 
		{
			parentLanes.Push ((GameObject) Instantiate(parentLanePrefab));

			/* Disable these tiles until spawned. */
			parentLanes.Peek ().SetActive (false);

		}
		print (parentLanes.Count);
	}

	public void SpawnParentLane(Material [] materials)
	{
		GameObject tmp = parentLanes.Pop ();
		tmp.SetActive (true);
		tmp.transform.position = currentParentLane.transform.GetChild (0).transform.GetChild (3).position;
		currentParentLane = tmp;

		SetLeftLaneMaterial (materials[0]);
		SetRightLaneMaterial (materials[1]);
		SetMidLaneMaterial (materials[2]);

	}

	void SetLeftLaneMaterial(Material material)
	{
		currentParentLane.transform.GetChild (0).transform.GetChild(2).transform.GetChild(0).GetComponent<Renderer> ().material = material;	
	}

	void SetMidLaneMaterial(Material material)
	{
		currentParentLane.transform.GetChild (0).transform.GetChild(1).transform.GetChild(0).GetComponent<Renderer> ().material = material;	
	}

	void SetRightLaneMaterial(Material material)
	{
		currentParentLane.transform.GetChild (0).transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer> ().material = material;	
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

	public void TurnToGray() {
		print ("Ok");
		GameObject[] lanes = GameObject.FindGameObjectsWithTag("Lane");
		foreach(GameObject lane in lanes)
		{
			print (lane
				.transform.GetChild (0)
				.GetComponent<Renderer> ().material.name);
			lane
				.transform.GetChild (0)
				.GetComponent<Renderer> ().material = grayLaneMat;
//			lane
//				.transform.GetChild (0)
//				.GetComponent<Renderer> ().material = grayLaneMat;
//			lane
//				.transform.GetChild (0)
//				.GetComponent<Renderer> ().material = grayLaneMat;
		}
	}

}
