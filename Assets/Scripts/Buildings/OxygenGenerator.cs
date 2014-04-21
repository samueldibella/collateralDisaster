using UnityEngine;
using System.Collections;

public class OxygenGenerator : MonoBehaviour {

	public bool isFunctioning;
	public bool isOn;
	

	// Use this for initialization
	void Start () {
		isFunctioning = true;
		isOn = true;
		
		StartCoroutine( Generator() );
	}
	
	IEnumerator Generator() {
		while(true) {
			if(isFunctioning && isOn) {
				GetComponent<PersonalAtmo>().personalAtmo.GetComponent<gasQualities>().isOxygenGen = true;
			} else {
				GetComponent<PersonalAtmo>().personalAtmo.GetComponent<gasQualities>().isOxygenGen = false;
			}
			
			yield return new WaitForSeconds(1);
		}
	}
	
	//turn generator on or off
	void OnMouseOver() {
		if(Input.GetMouseButtonDown(0)) {
			if(isOn) {
				isOn = false;
			} else {
				isOn = true;
			}
		}
	}
}
