using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour {

	public float showTime = 1f;
	float timeWithoutChange = 0f;

	// Update is called once per frame
	void Update () {
		if (timeWithoutChange > showTime) {
			timeWithoutChange = 0f;
			gameObject.GetComponent<Text>().enabled = !gameObject.GetComponent<Text>().enabled;
		} else {
			timeWithoutChange+=Time.deltaTime;
		}
		
	}
}
