using UnityEngine;
using System.Collections;

public class ToolTip : MonoBehaviour {
	
	Ray ray;
	RaycastHit hit;

	Vector3 location;
	Vector3 storage;
	
	void Start() {
	//	storage = new Vector3()
	}
	
	void Update () {

		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
		if(Physics.Raycast(ray, out hit)) {
			if(hit.transform.tag == "Building") {
				location = hit.point;
				location.y += 20;
					
				transform.position = location;
				GetComponent<TextMesh>().text = hit.transform.GetComponent<BuildingDisplay>().Info();
			} else {
				//transform.position = new Vec
			}
		} 
	}
}

