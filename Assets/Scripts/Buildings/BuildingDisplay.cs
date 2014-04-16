using UnityEngine;
using System.Collections;

public class BuildingDisplay : MonoBehaviour {

	Color initialColor;

	// Use this for initialization
	void Start () {
	
		initialColor = renderer.material.color;
		
	}
	
	void OnMouseOver() {

		renderer.material.color = Color.red;
		
	}
	
	void OnMouseExit() {
	
		renderer.material.color = initialColor;
		
	}
}
