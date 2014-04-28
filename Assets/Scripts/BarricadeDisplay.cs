﻿using UnityEngine;
using System.Collections;

public class BarricadeDisplay : MonoBehaviour {
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
		
	}
	
	void OnMouseOver() {
		mouseOver = true;
		renderer.material.shader = litShader;
		
	}
	
	void OnMouseExit() {
		mouseOver = false;
		renderer.material.shader = initialShader;
		renderer.material.color = initialColor;
		
		
	}
	
	void Update() {
		if(!mouseOver) {
			intermediateColor = Color.Lerp(initialColor, Color.red, this.gameObject.GetComponent<BuildingHealth>().fireIntensity / 100);
			renderer.material.color = Color.Lerp(intermediateColor, Color.grey, (100 - this.gameObject.GetComponent<BuildingHealth>().health) / 100);
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