using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {
	private float speed = 3f;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
		transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
		StartCoroutine(DoTheDance());
	}

	private IEnumerator DoTheDance() {
		/*
			Timer for lane spawning that activates for each 0.01 seconds.
		*/
		yield return new WaitForSeconds(15f); // waits 0.01 seconds
		if (speed < 8.0f)
			speed+=0.05f; // will make the update method pick up 
	}
}
