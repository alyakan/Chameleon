using UnityEngine;
using System.Collections;

public class ChangeLanes : MonoBehaviour {
	private bool moving = false;
	private float finalPos;
	public Rigidbody rigidBody;
	private DIR currentDir;

	private bool trigger = true;
	enum DIR {
		L, R
	};
	// Use this for initialization
	void Start () {
		finalPos = rigidBody.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		checkForCorrectPositionContinuous ();
		if (!moving) {
			rigidBody.velocity.Set (rigidBody.velocity.x, 0, rigidBody.velocity.z);
		}
		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) && finalPos > -1.5 && trigger) {
			/* Check that the sphere is not on the leftmost plane (x > -1.5).
			 * Note: The sphere can move left twice if its on the right most plane.
			 */
			currentDir = DIR.L;
			finalPos = (float)(finalPos - 1.5);
			moving = true;
			trigger = false;
			StartCoroutine(ChangineLanesTimer()); // Wait for 0.3 seconds
		}

		if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && finalPos < 1.5 && trigger) {
			/* Check that the sphere is not on the leftmost plane (x > -1.5).
			 * Note: The sphere can move left twice if its on the right most plane.
			 */
			currentDir = DIR.R;
			finalPos = (float)(finalPos + 1.5);
			moving = true;
			trigger = false;
			StartCoroutine(ChangineLanesTimer()); // Wait for 0.3 seconds before taking any input
		}
	}

	void FixedUpdate() {
		if (rigidBody.position.x > (finalPos - 0.05) && rigidBody.position.x < (finalPos + 0.05)) {
			/* Check if: finalPos - 0.05 < sphere.x < finalPos + 0.05 , if yes then fix final position. */
			setFinalPosition ();
			moving = false;
		}
		if (moving) {
			switch (currentDir) {
			case DIR.L:
				moveLeft ();
				break;
			case DIR.R:
				moveRight ();
				break;
			}
		}
	}

	void checkForCorrectPositionContinuous() {
		float pos = rigidBody.position.x;
		if (!moving) {
			if (pos != finalPos) {
				setFinalPosition ();
			}
		} else {
			if (pos > 1.5f || pos < -1.5f) {
				setFinalPosition ();
			}
		}
	}

	void setFinalPosition() {
		/*
		 * Fixes the final position after finishing movement to prevent the
		 * sphere from rolling to the sides.
		*/
		float ypos = (float)GetComponent<Rigidbody> ().position.y;
		float zpos = (float)GetComponent<Rigidbody> ().position.z;
		GetComponent<Rigidbody> ().position = new Vector3(finalPos, ypos, zpos);
	}

	void moveLeft() {
		/* Moves the sphere by small delta to make the translation smooth. */
		if (rigidBody.position.x > finalPos) {
			transform.Translate(Vector3.left * Time.deltaTime * 5);
		}
	}

	void moveRight() {
		/* Moves the sphere by small delta to make the translation smooth. */
		if (rigidBody.position.x < finalPos) {
			transform.Translate(Vector3.right * Time.deltaTime * 5);
		}
	}

	private IEnumerator ChangineLanesTimer() {

		yield return new WaitForSeconds(0.3f); // waits 3 seconds
		trigger = true; // will make the update method pick up 
	}
}
