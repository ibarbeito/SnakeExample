using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public List<SpawnContainer> Spawnables;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		foreach (SpawnContainer sc in Spawnables) {
			if (Input.GetKeyDown(sc.Key)) {
				Spawn(sc.Prefab);
			}
		}
	}

	void Spawn(GameObject Prefab) {
		Instantiate(Prefab);
	}
}
