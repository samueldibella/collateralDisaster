using UnityEngine;
using System.Collections;

public class PowerGenerator : MonoBehaviour {

	public bool isFunctioning;
	public bool isOn;
	
	GameObject generators;
	
	// Use this for initialization
	void Start () {
		isFunctioning = true;
		isOn = true;
	
		generators = GameObject.FindGameObjectsWithTag("OxyGenerators");
	}
	
	// Update is called once per frame
	void Update () {
		if(isFunctioning && isOn) {
			foreach (GameObject generator in generators) {
				generator.GetComponent<OxygenGenerator>().isFunctioning = false;
			}
		} else {
			foreach (GameObject generator in generators) {
				generator.GetComponent<OxygenGenerator>().isFunctioning = true;
			}
		}
	}
}
