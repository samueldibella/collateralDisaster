using UnityEngine;
using System.Collections;

//place barricade and water markers to show where prefab will be generated when player
//clicks on that location
public class FloodStreetMarker : MonoBehaviour {
	//attach to street prefab

	//assign prefab in inspector
	public GameObject waterMarker;
	public GameObject barricadeMarker;
	
	GameObject waterInstance;
	GameObject barricadeInstance;

	Vector3 storageLocation;
	Ray ray;
	RaycastHit hit;
	
	// Use this for initialization
	void Start () {
		storageLocation = new Vector3(-200, 0, -200);
		waterInstance = Instantiate(waterMarker, storageLocation, Quaternion.identity) as GameObject;
		barricadeInstance = Instantiate(barricadeMarker, storageLocation, Quaternion.identity) as GameObject;
	}
	
	void OnMouseOver() {
		//there's a slight bug when switching from play to pause, where the barricade will stay for a short period of time
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	
		Physics.Raycast(ray, out hit);
	
		if(Time.timeScale == 0 && GameStart.currentDisaster == GameStart.Disaster.Flood) {
			waterInstance.transform.position = hit.point;
		} else if (Time.timeScale == 1 ) {
			barricadeInstance.transform.position = hit.point;
		}
	}
	
	void OnMouseExit() {
		waterInstance.transform.position = storageLocation;
		barricadeInstance.transform.position = storageLocation;
	}
}
