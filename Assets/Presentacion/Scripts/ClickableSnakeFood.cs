using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickableSnakeFood : MonoBehaviour {

	public GameObject FoodPrefab;

	void OnMouseDown()  {
		GameObject NewFood = Instantiate(FoodPrefab);
		Bounds GameBounds = Camera.main.OrthographicBounds();
		NewFood.transform.position = Utils.RandomLocation(GameBounds.min, GameBounds.max);
		GameObject.Find("Score").GetComponent<Text>().text = (int.Parse(GameObject.Find("Score").GetComponent<Text>().text)+1).ToString();
		Destroy(gameObject);
	}



	

	
}
