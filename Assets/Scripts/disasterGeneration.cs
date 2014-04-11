﻿using UnityEngine;
using System.Collections;

public class disasterGeneration : MonoBehaviour {

	public enum Disaster {Flood, Fire, Gas}

	public float disasterRate = 60f;
	bool gameStart = false;
	float gameTime = 0;
	int randomHolder;
	
	//next disaster to appear
	public Disaster currentDisaster;
	
	// Use this for initialization
	void Start () {
		Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
		//game starts paused
		if(Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 0) {
			Time.timeScale = 1;
			gameStart = true;
		}
		
		//if 
		if(Time.timeScale == 1) {
			gameTime += Time.deltaTime;
			
			if(gameTime % 60 < .03 && gameTime % 60 > 59.85) {
				//pause game
				Time.timeScale = 0;
				
				//generate disaster
				randomHolder = Random.Range(0, 3);
				
				randomHolder = 1;
				
				switch(randomHolder) {
				case 0:
					currentDisaster = Disaster.Fire;
					break;
				case 1:
					currentDisaster = Disaster.Flood;
					break;
				case 2:
					currentDisaster = Disaster.Gas;
					break;
				}
			}	
				
		} else if (Time.timeScale == 0 && gameStart == true) {
			if(Input.GetKeyDown(KeyCode.Mouse0)) {
				//raycast to location, and must be a street, then generate disaster there
			}
		
		}
		
	}
}
