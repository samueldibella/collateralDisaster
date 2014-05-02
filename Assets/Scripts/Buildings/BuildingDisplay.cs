using UnityEngine;
using System.Collections;

public class BuildingDisplay : MonoBehaviour {
//script for building color and appearance, and building tool tip

	public Color initialColor;
	
	//after accounting for fire and water presence
	Color intermediateColor;
//	Color importantColor;
	
	bool mouseOver = false;

	Shader initialShader;
	public Shader litShader; 

	// Use this for initialization
	void Start() {
		initialShader = renderer.material.shader;
		initialColor = renderer.material.color;
		
		StartCoroutine( ColorUpdate() );
	}
	
	void OnMouseOver() {
		mouseOver = true;
		
		if(Time.timeScale == 0 && GameStart.currentDisaster == GameStart.Disaster.Fire) {
			renderer.material.color = Color.red;
		} else {
			renderer.material.shader = litShader;
		}

	}
	
	void OnMouseExit() {
		mouseOver = false;
		renderer.material.shader = initialShader;
		renderer.material.color = initialColor;
		
		
	}
	
	IEnumerator ColorUpdate() {
		if(GetComponent<BuildingHealth>().buildingKey == BuildingHealth.keyBuilding1 || 
		   GetComponent<BuildingHealth>().buildingKey == BuildingHealth.keyBuilding2 || 
		   GetComponent<BuildingHealth>().buildingKey == BuildingHealth.keyBuilding3 || 
		   GetComponent<BuildingHealth>().buildingKey == BuildingHealth.keyBuilding4) {
				initialColor = Color.cyan;
				GetComponent<BuildingHealth>().infrastructureValue = 10; 
		}
		
		while(true) {
			if(!mouseOver) {
				intermediateColor = Color.Lerp(initialColor, Color.red, this.gameObject.GetComponent<BuildingHealth>().fireIntensity / 100);
				renderer.material.color = Color.Lerp(intermediateColor, Color.grey, (100 - this.gameObject.GetComponent<BuildingHealth>().health) / 100);
			}

			yield return new WaitForSeconds(1);
		}

	}
	
	//information for tool tip
	public string Info() {
	
		string output = "";
		//health, fire, 
		output += "Health: " + this.gameObject.GetComponent<BuildingHealth>().health;
		
		if(this.gameObject.GetComponent<BuildingHealth>().onFire) {
			output += "\nFire Intensity: " + this.gameObject.GetComponent<BuildingHealth>().fireIntensity;
		}
		
		return output;
	}
}
