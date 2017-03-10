using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ShowOnKeyDown : MonoBehaviour {

	public KeyCode key;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(key)) {
			ChangeVisibility();
		}
	}

	abstract protected void ChangeVisibility();
}
