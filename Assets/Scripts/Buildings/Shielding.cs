using UnityEngine;
using System.Collections;

public class Shielding : MonoBehaviour {

	public bool isFunctioning;
	public bool isOn;
	
	//corresponding gas cube
	GameObject atmosphere;
	
	Ray ray;
	RaycastHit hit;
	
	// Use this for initialization
	void Start () {
		isFunctioning = true;
		isOn = true;
	}
	
	
	IEnumerator Shield() {
		bool selected = false;
		
		while(!selected) {
			if(Input.GetMouseButtonDown(0)) {
				ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				
				if(Physics.Raycast(ray, out hit)) {
					if(hit.transform.tag = "Building") {
						hit.transform.GetComponent<BuildingHealth>().isShielded = true;
						selected = true;
						isOn = false;
					}
				}
			}
			
			yield return 0;
		}
		
		yield return new WaitForSeconds(3);
		
		hit.transform.GetComponent<BuildingHealth>().isShielded = false;
		
		StartCoroutine( Charge() );
	}
	
	IEnumerator Charge() {
		yield return new WaitForSeconds(7);
		isOn = true;
	}
	
	void OnMouseOver() {
		if(Input.GetMouseButtonDown(0) && isOn) {
			StartCoroutine( Shield() );	
		}
	}
}
