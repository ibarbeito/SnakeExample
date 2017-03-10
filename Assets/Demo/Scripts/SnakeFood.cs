using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeFood : MonoBehaviour {

	public GameObject FoodPrefab;

	void OnCollisionEnter2D(Collision2D coll)  {
		/*Cuando algo choca contra la comida, se crea una nueva copia en una posicion aleatoria, y se destruye esta*/
		GameObject NewFood = Instantiate(FoodPrefab);
		Bounds GameBounds = Camera.main.OrthographicBounds();
		NewFood.transform.position = Utils.RandomLocation(GameBounds.min, GameBounds.max);
		Destroy(gameObject);
	}

	

	
}
