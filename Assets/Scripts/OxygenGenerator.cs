using UnityEngine;
using System.Collections;

public class OxygenGenerator : MonoBehaviour {

	public bool isFunctioning;
	public bool isOn;
	
	//corresponding gas cube
	GameObject atmosphere;

	// Use this for initialization
	void Start () {
		isFunctioning = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(isFunctioning && isOn) {
			atmosphere.GetComponent<gasQualities>().isOxygenGen = true;
		}
		
		/*if(building health or power generator is down) {
			isFunctioning = false;
		}
		*/
	}
	
	void OnTriggerEnter(Collider other) {
		if(other.tag == "Gas") {
			atmosphere = other.gameObject;
		}
	}
}
