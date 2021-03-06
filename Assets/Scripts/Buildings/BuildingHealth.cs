﻿using UnityEngine;
using System.Collections;

public class BuildingHealth : MonoBehaviour {

	//relevant game object 
	public GameObject building; 
	
	GameObject player;
	GameObject camera;
	
	//building values
	public int infrastructureValue; 
	public float health; 
	public bool isShielded;
	bool cutting = false;
	
	//fire stuff
	public bool fireStarted; 
	public bool onFire; 
	public float fireIntensity; 
	public float fireDamage; 
	public static float fireRate = 2f;
	
	//bounds  
	int middleBoundX; 
	int middleBoundZ;   
	
	public int quadrant; 
	
	//building key 
	public static int keyIncrementer;  
	public int buildingKey; 
	bool gotKey = false; 
	
	public static GameObject[] quad4Array = new GameObject[1000];
	
	public static int quadIterator4; 
	
	public static bool keyBuilding4Selected = false; 
	
	public static int keyBuilding4 = -1; 
	
	void Awake() {
		
		middleBoundX = 218; 
		
		middleBoundZ = 208; 
		
		//assign buiding key 
		if(gameObject.tag.Equals("Building") == true) {  
			assignBuildingKey(); 
			buildingKeyFixer();	
			quadrant = getQuadrant((int)transform.position.x, (int)transform.position.z); 
		}	
		if(quadrant == 4) {
			quad4Array[quadIterator4] = gameObject; 
			quadIterator4++; 
		} 			
	}
	
	// Use this for initialization
	void Start () {
		if(gameObject.tag.Equals("Building") == true) { 
			buildingKeyFixer();
			StartCoroutine( CutThrough() );
		}
		
		camera = GameObject.FindGameObjectWithTag("MainCamera");
		
		//building values 
		health = 100; 
		infrastructureValue = 0; 
		isShielded = false;
		
		//fire stuff 
		fireStarted = false;
		onFire = false; 		
		fireIntensity = 0; 
		fireDamage = 0;
		
		if(keyBuilding4Selected == false) {
			keyBuilding4 = quad4Array[Random.Range(0, quadIterator4+1)].GetComponent<BuildingHealth>().buildingKey; 
			keyBuilding4Selected = true; 
		}
		
		
	}
	
	// Update is called once per frame
	void Update () {

		//building destroy when health = 0 and subtracts score 
		if(health <= 0) {
			Infrastructure.totalStructure -= infrastructureValue;
			camera.GetComponent<CameraControl>().shakeAndBake = true;
			GetComponent<AudioSource>().Play();
			Destroy(gameObject); 		
		}
		
		//fire methods  
		if(fireStarted == true) {
			StartCoroutine( Fire() );
			fireStarted = false; 
		} 
	}
	
	
	//methods
	//burning method controls the rate of burning and fire intensity 
	IEnumerator Fire() {
		int randomBuilding;
		onFire = true;

		while(onFire) {

			fireIntensity += Random.Range(4, 10);			
			
			if(fireIntensity > 10) {
				health -= fireIntensity / 10;
				Collider[] hitColliders = Physics.OverlapSphere(transform.position, 8f); 
				
				for(int i = 0; i < hitColliders.Length; i++) {
					randomBuilding = Random.Range(0, 10);
				
					if(hitColliders[i].tag == "Road" && hitColliders[i].GetComponent<RoadDisplay>().onFire == false) {
						hitColliders[i].GetComponent<RoadDisplay>().fireStarted = true;
					}
				
					if(hitColliders[i].tag == "Building" && hitColliders[i].GetComponent<BuildingHealth>().buildingKey == buildingKey &&
					   				hitColliders[i].GetComponent<BuildingHealth>().onFire == false) {
						randomBuilding = Random.Range(0,10);
						
						if(randomBuilding < 6) {
							hitColliders[i].GetComponent<BuildingHealth>().fireStarted = true;
							
						}
						
					} else if (hitColliders[i].tag == "Building" && hitColliders[i].GetComponent<BuildingHealth>().onFire == false) {
						if(randomBuilding < 2) {
							hitColliders[i].GetComponent<BuildingHealth>().fireStarted = true;
						}
					} else if (hitColliders[i].tag == "BuildingRNG" && hitColliders[i].GetComponent<BuildingHealth>().onFire == false) {
						if(randomBuilding < 5) {
							hitColliders[i].GetComponent<BuildingHealth>().fireStarted = true;
						}
					}
					
				}	
			}

			yield return new WaitForSeconds(fireRate);
			
		}				
	}	
	
	//damage
	void OnParticleCollision(GameObject other) {
		cutting = true;
		//health -= (float) player.GetComponent<PlayerControl>().damage;
	}
	
	IEnumerator CutThrough() {
		yield return 0;
		
		player = GameObject.FindGameObjectWithTag("Player");
		
		while(true) {
			yield return new WaitForSeconds(.1f);
			
			if(cutting) {
				health -= (float) player.GetComponent<PlayerControl>().damage;
				cutting = false;
			}
		}
	}
	
	//This script determines which building tiles make up one building 
	//this is determined at start and will be denoted at by a building key 
	//some building keys will be reserved for unique building e.g hospital = 100
	void assignBuildingKey() {
		Vector3 check = new Vector3 (2,0,2);	
		Collider[] checkColliders = Physics.OverlapSphere(transform.position - check, 2f);
		for(int i = 0; i < checkColliders.Length; i++) {	
			if(checkColliders[i].tag.Equals("Building") == true && checkColliders[i].transform.position != transform.position) {
				buildingKey = checkColliders[i].GetComponent<BuildingHealth>().buildingKey;
				gotKey = true; 
			}
		}  
		
		if(gotKey == false){
			keyIncrementer++; 
			buildingKey = keyIncrementer; 
		}
	}
	
	void buildingKeyFixer() {
		Collider[] checkColliders2 = Physics.OverlapSphere(transform.position, 3f);
		for(int j = 0; j < checkColliders2.Length; j++) {	
			if(checkColliders2[j].tag.Equals("Building") == true && checkColliders2[j].transform.position != transform.position) {
				if(checkColliders2[j].GetComponent<BuildingHealth>().buildingKey > buildingKey) {
					buildingKey = checkColliders2[j].GetComponent<BuildingHealth>().buildingKey; 
				} 
				
				if(checkColliders2[j].GetComponent<BuildingHealth>().buildingKey < buildingKey) {
					checkColliders2[j].GetComponent<BuildingHealth>().buildingKeyFixer(); 
				} 
			}	
		}	
	}
	
	int getQuadrant(int x, int z) {
		if( x > 328) {
			return 4;
		}	
		return 0; 
	}
}