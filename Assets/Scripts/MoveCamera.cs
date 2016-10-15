using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {
	public GameObject Sphere;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		/* Keeps following the sphere with a difference of 9.0 points in the z-position */
		transform.position = (new Vector3(transform.position.x, transform.position.y, (Sphere.transform.position.z - 9.0f)));
	}

}
