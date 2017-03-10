using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideOnKeyDown : ShowOnKeyDown {

	override protected void ChangeVisibility() {
		gameObject.SetActive(false);
	}
}
