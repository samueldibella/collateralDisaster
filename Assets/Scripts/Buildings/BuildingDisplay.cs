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
		if(GetComponent<BuildingHealth>().buildingKey == BuildingHealth.keyBuilding4) {
			initialColor = Color.cyan; 
			GetComponent<BuildingHealth>().infrastructureValue += 1; 
			Infrastructure.totalStructure += 1; 
			transform.position = new Vector3(transform.position.x, transform.position.y - 3, transform.position.z); 
			StartCoroutine("playerCheck"); 
		}
		
		while(true) {
			if(!mouseOver) {
				intermediateColor = Color.Lerp(initialColor, Color.red, this.gameObject.GetComponent<BuildingHealth>().fireIntensity / 100);
				renderer.material.color = Color.Lerp(intermediateColor, Color.grey, (100 - this.gameObject.GetComponent<BuildingHealth>().health) / 100);
			}

			yield return new WaitForSeconds(1);
		}

	}
	IEnumerator playerCheck() {
		while(true) {
			Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5f); 
			for(int i = 0; i < hitColliders.Length; i++) {
				if(hitColliders[i].tag.Equals("Player")) {
					print("you win!"); 
				}
			}
			yield return new WaitForSeconds(5);			
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
