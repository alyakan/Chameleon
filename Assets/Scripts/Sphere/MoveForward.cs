using UnityEngine;
using System.Collections;

public class MoveForward : MonoBehaviour {
	public float speed = 4f;
	public bool accelerate = false;
	// Use this for initialization
	void Start () {
		StartCoroutine(DoTheDance());
		GetComponent<Rigidbody>().AddForce(0f, 0f, 1.5f * speed);
	}

	// Update is called once per frame
	void Update () {
		
		transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
		if (speed < 20.0f && accelerate) {
			speed+=1.0f; // will make the update method pick up
			StartCoroutine(DoTheDance());
		}


	}

	private IEnumerator DoTheDance() {
		/*
			Timer for lane spawning that activates for each 0.01 seconds.
		*/
		accelerate = false;
		yield return new WaitForSeconds(5f); // waits 0.01 seconds
		accelerate = true;
	}
}
