using UnityEngine;
using System.Collections;

public class ChangeColor : MonoBehaviour {
	public Material blue;
	public Material red;
	public Material green;
	public Renderer character;

	// Use this for initialization
	void Start () {
		character = GetComponent<Renderer>();
		character.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown ("q"))
			character.material = red;		
		if (Input.GetKeyDown("w"))
			character.material = blue;
		if (Input.GetKeyDown("e"))
			character.material = green;
	}
}
