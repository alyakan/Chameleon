using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {
	public bool grounded = true;
	public float jumpPower = 750;
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
		if(hasJumped){
			GetComponent<Rigidbody>().AddForce(Vector3.up*jumpPower);
			// transform.Translate(Vector3.up * Time.deltaTime * 5);
			grounded = false;
			hasJumped = false;
		}
	}

	void FixedUpdate() {
		
	}

}
