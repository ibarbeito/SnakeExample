using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnClick : MonoBehaviour {

	public GameObject Target;
	public bool InitialState = false;

	// Use this for initialization
	void Start () {
		Target.SetActive(InitialState);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(Constants.INTERACTION_MOUSE_BUTTON)) {
			Target.SetActive(!Target.activeSelf);
		}
		
	}
}
