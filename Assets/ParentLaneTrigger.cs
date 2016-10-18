using UnityEngine;
using System.Collections;

public class ParentLaneTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit(Collider other) {
		print ("Out");
	}
}
