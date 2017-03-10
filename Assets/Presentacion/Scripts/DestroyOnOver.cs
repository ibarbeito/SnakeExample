using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DestroyOnOver : MonoBehaviour , IPointerEnterHandler {
     
     

	void Start() {
		if (GetComponent<BoxCollider2D>() == null) {
			BoxCollider2D boxCollider = gameObject.AddComponent<BoxCollider2D>();
			boxCollider.size = GetComponent<RectTransform>().rect.size;
		}
			
	}

	// Update is called once per frame
	public void OnPointerEnter(PointerEventData eventData) {
		if (Input.GetMouseButton(Constants.INTERACTION_MOUSE_BUTTON))
			Destroy(gameObject);
	}
}
