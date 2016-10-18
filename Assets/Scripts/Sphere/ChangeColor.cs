using UnityEngine;
using System.Collections;

public class ChangeColor : MonoBehaviour {
	public Material blue;
	public Material red;
	public Material green;
	public Renderer character;
	public GameObject lightSwitch;

	// Use this for initialization
	void Start () {
		character = GetComponent<Renderer>();
		character.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("q") || Input.GetKeyDown("w") || Input.GetKeyDown("e"))
			lightSwitch.GetComponent<AudioSource> ().Play ();
		if (Input.GetKeyDown ("q"))
			character.material = red;		
		if (Input.GetKeyDown("w"))
			character.material = blue;
		if (Input.GetKeyDown("e"))
			character.material = green;
	}
}
