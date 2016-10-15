using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {
	public bool grounded = true;
	public float jumpPower = 1000;
	private bool hasJumped = false;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if(!grounded && GetComponent<Rigidbody>().position.y > 0.40 && GetComponent<Rigidbody>().position.y < 0.60) {
			grounded = true;
		}
		if (Input.GetKeyDown(KeyCode.Space) && grounded == true) {
			hasJumped = true;
		}
	}

	void FixedUpdate() {
		if(hasJumped){
			GetComponent<Rigidbody>().AddForce(transform.up*jumpPower);
			grounded = false;
			hasJumped = false;
		}
	}

}
