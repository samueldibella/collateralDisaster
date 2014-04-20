using UnityEngine;
using System.Collections;

public class BuildingHealth : MonoBehaviour {

	//relevant game object 
	public GameObject building; 
	
	//total score
	public static float totalScore;
	
	//building values
	public int infrastructureValue; 
	public float health; 
	
	//fire stuff
	public bool fireStarted; 
	public bool onFire; 
	public float fireIntensity; 
	public float fireDamage; 
	bool fireIncreasing;
	
	//water stuff
	public bool firstFlood; 
	public bool floodStarted; 
	public bool waterLogged; 
	public float waterLoggedPercent; 
	public float waterDamage; 
	
	//building key 
	public static int keyIncrementer;  
	public int buildingKey; 
	bool gotKey = false; 
	
	// Use this for initialization
	void Start () {
		//building values 
		health = 100; 
		infrastructureValue = 0; 
		totalScore += infrastructureValue;
		
		//fire stuff 
		fireStarted = false;
		onFire = false; 		
		fireIntensity = 0; 
		fireDamage = 0; 
		fireIncreasing = false;
		
		//water stuff
		firstFlood = false; 
		floodStarted = false; 
		waterLogged = false; 
		waterLoggedPercent = 0; 
		waterDamage = 0; 
		
		//assign buiding key 
		assignBuildingKey(); 
	}
	
	// Update is called once per frame
	void Update () {
		//building destroy when health = 0 and subtracts score 
		if(health <= 0) {
			Camera.main.GetComponent<Infrastructure>().totalStructure -= infrastructureValue;
			Destroy(gameObject); 		
		}
		
		//fire methods  
		if(fireStarted == true) {
			burning(); 
			onFire = true; 
			fireStarted = false; 
		}
		
		//updates health if on fire and spread fire
		if(onFire == true) {
			//this will spread fire 	
			if(fireIntensity >= 50) {
				Collider[] hitColliders = Physics.OverlapSphere(transform.position, 6f); 
				int i = 0;
				while (i < hitColliders.Length) {
					if(hitColliders[i].tag.Equals("Building") == true && hitColliders[i].GetComponent<BuildingHealth>().onFire == false) {
						hitColliders[i].GetComponent<BuildingHealth>().fireStarted = true;
					}
					i++;
				}
			}	
		} else if (onFire == false && fireIncreasing == true) {
			//if it stopes being on fire it resets fire Intensity and stops the corutines from runnning. 
			StopCoroutine("fireIntensityIncreaser"); 
			StopCoroutine("fireDamageIncreaser"); 
			StopCoroutine("fireSpread"); 
			fireIntensity = 0;
			fireIncreasing = false;
		} 
		
		//detects for water
		Collider[] hitCollidersWater = Physics.OverlapSphere(transform.position, 5f); 
		int j = 0; 
		while (j < hitCollidersWater.Length) {
			if(hitCollidersWater[j].tag.Equals("Water") == true && firstFlood == false ) {	
				firstFlood = true;
				floodStarted = true; 	
			} if(hitCollidersWater[j].tag.Equals("Building") == true && hitCollidersWater[j].GetComponent<BuildingHealth>().waterLogged == true 
			     && hitCollidersWater[j].GetComponent<BuildingHealth>().waterLoggedPercent >= 30 && firstFlood == false) {
				firstFlood = true;
				floodStarted = true;
			}
			else {
				//waterLogged = false; 
			}
			j++;
		}
		
		//water controls
		if(floodStarted == true){
			waterLogged = true; 
			flooding(); 
			floodStarted = false;  
		}
		
		if(waterLogged == true) {
			//will put our fires if water logged
			if(waterLoggedPercent >= 30) {
				onFire = false;
			}
		} else if(waterLoggedPercent > 0 && waterLogged == false) {
			StartCoroutine("waterIntensityDecreaser"); 
			StopCoroutine("waterIntensityIncreaser"); 
			StopCoroutine("waterDamageIncreaser"); 
		}
		
		//health update 
		health = 100 - (waterDamage + fireDamage); 
	}
	
	
	//methods
	//burning method controls the rate of burning and fire intensity 
	void burning() {
		if(fireIntensity < 100) {
			StartCoroutine( "fireIntensityIncreaser");
		}
		
		if(fireIntensity >= 50) {
			StartCoroutine("fireSpread"); 
		}	
		
		StartCoroutine( "fireDamageIncreaser" );		
	}
	//firespread method 
	void fireSpread() {
		StartCoroutine("fireSpread"); 	
	}
	
	//flooding method that conrols the corutines that conrtol flooding 
	void flooding(){
		if(waterLoggedPercent < 100) {
			StartCoroutine( "waterIntensityIncreaser");
		}
			
		StartCoroutine( "waterDamageIncreaser" );		
	}
	
	//Couroutines
	//coroutine that increase fireintesity until it gets to 100 
	IEnumerator fireIntensityIncreaser() {		
		fireIncreasing = true;
		
		while(true) {
			if( fireIntensity >= 100) {
				yield break; 
			} else {
				fireIntensity += 5f; 
				yield return new WaitForSeconds(1);
			}
		}
	}
	
	//coroutine that increase the damage the fire is doing based on the intensity of the fire. 
	IEnumerator fireDamageIncreaser() {		
		
		while(true) {
			fireDamage += (.05f * fireIntensity); 
			yield return new WaitForSeconds(1);
		}
	}
	
	//water logged percent increase 
	IEnumerator waterIntensityIncreaser() {		
		while(true) {
			if( waterLoggedPercent >= 100) {
				yield break; 
			} else {
				waterLoggedPercent += 5f; 
				yield return new WaitForSeconds(1);
			}
		}
	}
	
	IEnumerator waterIntensityDecreaser() {		
		while(true) {
			if( waterLoggedPercent <= 0) {
				yield break; 
			} else {
				waterLoggedPercent -= 5f; 
				yield return new WaitForSeconds(1);
			}
		}
	}
	
	//water corutine that increase water damage based on water logged percent 
	IEnumerator waterDamageIncreaser() {		
		while(true) {
			waterDamage +=  (.05f * waterLoggedPercent);
			yield return new WaitForSeconds(1);
		}
	}
	 
	//This script determines which building tiles make up one building 
	//this is determined at start and will be denoted at by a building key 
	//some building keys will be reserved for unique building e.g hosptial = 100
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
	
	//not using this now but to optimize I might need to use this so Im keeping it around for now 
//	IEnumerator fireSpreadIncreaser() {		
//		print ("df"); 
//		while(true) {
//			Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10f); 
//			int i = 0;
//			while (i < hitColliders.Length) {
//				if(hitColliders[i].tag.Equals("Building") == true && hitColliders[i].GetComponent<BuildingHealth>().onFire == false) {
//					hitColliders[i].GetComponent<BuildingHealth>().fireStarted = true;
//					i++;
//				}
//			}
//			yield return new WaitForSeconds(1);
//		}
//	}
}