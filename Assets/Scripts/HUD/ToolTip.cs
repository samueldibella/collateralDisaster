using UnityEngine;
using System.Collections;

public class ToolTip : MonoBehaviour {
	
	Ray ray;
	RaycastHit hit;
	
	void Update () {

		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
		if(Physics.Raycast(ray, out hit)) {
			if(hit.transform.tag == "Building") {
			
				GetComponent<TextMesh>().text = hit.transform.GetComponent<BuildingDisplay>().Info();
			
			}
		} 
	}
}

