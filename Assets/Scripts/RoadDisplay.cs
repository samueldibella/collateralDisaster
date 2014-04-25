using UnityEngine;
using System.Collections;

public class RoadDisplay : MonoBehaviour {

	Color initialColor = Color.white;
	
	void Start() {
		
	}
	
	void OnMouseOver() {
		if(Time.timeScale == 1 && Input.GetMouseButton(0)) {
			renderer.material.color = Color.yellow;
		}
	}
	
	void OnMouseExit() {
		renderer.material.color = initialColor;
	}
}
