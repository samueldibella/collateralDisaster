using UnityEngine;
using System.Collections;

public class BuildingDisplay : MonoBehaviour {

	Color initialColor;
	bool mouseOver = false;

	// Use this for initialization
	void Start() {
	
		initialColor = renderer.material.color;
		
	}
	
	void OnMouseOver() {
		mouseOver = true;
		
		if(Time.timeScale == 0 && disasterGeneration.currentDisaster == disasterGeneration.Disaster.Fire) {
			renderer.material.color = Color.red;
		}

		
	}
	
	void OnMouseExit() {
		mouseOver = false;
		renderer.material.color = initialColor;
		
	}
	
	void Update() {
		if(!mouseOver) {
			renderer.material.color = Color.Lerp(initialColor, Color.red, this.gameObject.GetComponent<BuildingHealth>().fireIntensity / 100);
		}
	}
}
