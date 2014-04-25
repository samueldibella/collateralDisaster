using UnityEngine;
using System.Collections;

public class RoadDisplay : MonoBehaviour {
	//attach to street prefab
	
	//assign prefab in inspector
	//public GameObject waterMarker;
	
	GameObject waterInstance;
	
	Vector3 storageLocation;
	Ray ray;
	RaycastHit hit;


	Color initialColor = Color.white;
	
	void Start() {
		storageLocation = new Vector3(-200, 0, -200);
		//waterInstance = Instantiate(waterMarker, storageLocation, Quaternion.identity) as GameObject;
	}
	
	void OnMouseOver() {
		if(Time.timeScale == 1 && Input.GetMouseButton(0)) {
			renderer.material.color = Color.yellow;
		}
		
		if(Time.timeScale == 0 && GameStart.currentDisaster == GameStart.Disaster.Flood) {
			renderer.material.color = Color.blue;
		} 
	}
	
	void OnMouseExit() {
		renderer.material.color = initialColor;
	}
}
