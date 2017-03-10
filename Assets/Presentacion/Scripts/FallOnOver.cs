using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOnOver : MonoBehaviour {

	// Update is called once per frame
	public void OnOver () {
		GetComponent<Rigidbody2D>().isKinematic=false;
	}
}
