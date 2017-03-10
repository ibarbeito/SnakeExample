using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithKeys : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		transform.position+= new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0)*0.5f;
	}
}
