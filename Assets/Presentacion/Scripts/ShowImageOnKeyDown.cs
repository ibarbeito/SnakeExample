using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowImageOnKeyDown : ShowOnKeyDown {

	Image image;
	void Start() {
		image = GetComponent<Image>();
	}

	override protected void ChangeVisibility() {
		image.enabled = !image.enabled;
	}
}
