using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransformableImage : MonoBehaviour {

	bool Inputable = false;
	Vector3 LastMousePosition;
	Vector3 PivotDelta;

	void Start() {
		GetComponent<BoxCollider2D>().size = GetComponent<RectTransform>().rect.size;
		PivotDelta = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		LastMousePosition = Input.mousePosition;
		if (Input.GetMouseButton(Constants.INTERACTION_MOUSE_BUTTON) && Inputable) {
			Follow(LastMousePosition);
			Scale();
		} 
		
		if (!Input.GetMouseButton(Constants.INTERACTION_MOUSE_BUTTON)) {
			PivotDelta = Vector3.zero;
		}
	}

	void Follow(Vector3 LastMousePosition) {
		Vector2 pos;
     	RectTransformUtility.ScreenPointToLocalPointInRectangle(gameObject.transform as RectTransform, Input.mousePosition, Camera.main, out pos);
		if (PivotDelta == Vector3.zero) {
			PivotDelta = gameObject.transform.TransformPoint(pos) - transform.position;
		}
     	transform.position = gameObject.transform.TransformPoint(pos)-PivotDelta;
	}

	void Scale() {
		float scale = Input.GetAxis("Mouse ScrollWheel");
		transform.localScale += Vector3.one*scale;
	}

	void OnMouseOver() {
		Inputable=true;
	}

	void OnMouseExit() {
		Inputable=false;
	}
}
