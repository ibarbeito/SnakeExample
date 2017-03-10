using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate3D : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
		float ran = Random.value*0.5f;
		float ran2 = Random.value*0.5f;
		transform.Rotate(new Vector3(ran, ran2, 1f-(ran+ran2)));
	}
}
