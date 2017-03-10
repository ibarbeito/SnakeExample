using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PresentationEventHandler : MonoBehaviour {

	int currentSlide;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		currentSlide = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(Constants.NEXT_SLIDE_MOUSE_BUTTON)) {
			if (Input.GetKey(KeyCode.LeftControl)) {
				currentSlide--;
			} else {
				currentSlide++;
			}
			SceneManager.LoadScene(currentSlide.ToString());
		}
	}
}
