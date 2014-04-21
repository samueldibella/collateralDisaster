﻿using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
// actual player placement of barricades

	Ray ray;
	RaycastHit hit;

	//barricade prefab
	public GameObject barricade;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0) && Time.timeScale == 1 && GameObject.FindGameObjectWithTag("Factory").GetComponent<Factory>().isFunctioning) {
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			if(Physics.Raycast(ray, out hit)) {
				if(hit.transform.tag == "Road") {
					Instantiate(barricade, hit.point, Quaternion.identity);
				}
			}
		}
	}
}
