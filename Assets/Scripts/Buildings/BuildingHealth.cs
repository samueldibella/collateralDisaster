using UnityEngine;
using System.Collections;

public class BuildingHealth : MonoBehaviour {

	//relevant game object 
	public GameObject building; 
	
	//total score
	public static float totalScore;
	
	//building values
	float monetaryValue; 
	public float health; 
	
	//fire stuff
	public bool fireStarted; 
	public bool onFire; 
	public float fireIntensity; 
	public float fireDamage; 
	
	//water stuff
	public bool firstFlood; 
	public bool floodStarted; 
	public bool waterLogged; 
	public float waterLoggedPercent; 
	public float waterDamage; 
	
	//building key 
	public static int buildKeysAssigned; 
	public static int keyIncrementer;  
	public int buildingKey; 
	public int buildingKeyHeight; 
	int recursiveLimit = 1; 
	bool gotKey = false; 
	
	// Use this for initialization
	void Start () {
		//building values 
		health = 100; 
		monetaryValue = 100; 
		totalScore += monetaryValue;
		
		//fire stuff 
		fireStarted = false;
		onFire = false; 		
		fireIntensity = 0; 
		fireDamage = 0; 
		
		//water stuff
		firstFlood = false; 
		floodStarted = false; 
		waterLogged = false; 
		waterLoggedPercent = 0; 
		waterDamage = 0; 
		
		//assign buiding key 
		assignBuildingKey();
		buildingKeyFixer(); 
		transform.position = new Vector3 (transform.position.x, buildingKeyHeight, transform.position.z);
		
		//coroutine calls 
//		StartCoroutine("waterCheckCoroutine"); 
//		StartCoroutine("fireCheckCoroutine"); 
	}
	
	
	// Update is called once per frame
	void Update () {
		//building destroy when health = 0 and subtracts score 
		if(health <= 0) {
			totalScore -= monetaryValue;
			Destroy(gameObject); 		
		}
		//fire methods  
		if(fireStarted == true) { 
			renderer.material.color = Color.red;
			burning(); 
			onFire = true; 
			fireStarted = false; 
		}
		//updates health if on fire and spread fire
		if(onFire == true) {
			//this will spread fire 	
			if(fireIntensity >= 50) {
				MasterArray.cityArray[(int)transform.position.x,(int)transform.position.z] = 4; 
			}	
		}
		//if it stopes being on fire it resets fire Intensity and stops the corutines from runnning. 
		if(onFire == false) {
			renderer.material.color = Color.green;
			StopCoroutine("fireIntensityIncreaser"); 
			StopCoroutine("fireDamageIncreaser"); 
			fireIntensity = 0;	 		
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
			if(waterLoggedPercent >= 70) {
				MasterArray.cityArray[(int)transform.position.x, (int)transform.position.z] = 5; 
			}	
		}
		if(waterLogged == false) {
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
		if(fireIntensity >= 5) {
			//StartCoroutine("fireSpread"); 
		}	
		StartCoroutine( "fireDamageIncreaser" );		
	}
	//flooding method that conrols the corutines that conrtol flooding 
	void flooding(){
		if(waterLoggedPercent < 100) {
			StartCoroutine( "waterIntensityIncreaser");
		}	
		StartCoroutine( "waterDamageIncreaser" );		
	}
	bool fireCheck() {
		for(int z = -8; z <= 8; z++) {
			for(int x = -8; x <= 8; x++) {
				if(MasterArray.cityArray[(int)transform.position.x + x, (int)transform.position.z + z] == 4) {
					return true; 
				}
			}
		}
		return false; 
	}
	bool waterCheck() {
		for(int z = -4; z <= 4; z++) {
			for(int x = -4; x <= 4; x++) {
				if(MasterArray.cityArray[(int)transform.position.x + x, (int)transform.position.z + z] == 5 ||
				   MasterArray.cityArray[(int)transform.position.x + x, (int)transform.position.z + z] == 6) {
				 	return true;  
				 }
			}
		}
		return false; 
	}
	//Couroutines
	//coroutine that increase fireintesity until it gets to 100 
	IEnumerator fireIntensityIncreaser() {		
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
	IEnumerator waterCheckCoroutine() {		
		while(true) {
			if(waterCheck() == true && firstFlood == false) {
				firstFlood = true;
				floodStarted = true; 
			} else if(waterCheck() == false) {
				waterLogged = false; 
				firstFlood = false;
			} 
			yield return new WaitForSeconds(1);
			
		}
	}
	IEnumerator fireCheckCoroutine() {		
		while(true) {
			if(fireCheck() == true) {
				fireStarted = true;
			} 
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
				buildingKeyHeight = checkColliders[i].GetComponent<BuildingHealth>().buildingKeyHeight;
				gotKey = true; 
				buildKeysAssigned++; 
			}
		}  
		if(gotKey == false) {
			keyIncrementer++; 
			buildingKey = keyIncrementer; 
			buildingKeyHeight = Random.Range(0,1); 
			buildKeysAssigned++;
		}
	}
	void buildingKeyFixer() {
		Collider[] checkColliders2 = Physics.OverlapSphere(transform.position, 3f);
		for(int j = 0; j < checkColliders2.Length; j++) {	
			if(checkColliders2[j].tag.Equals("Building") == true && checkColliders2[j].transform.position != transform.position) {
				if(checkColliders2[j].GetComponent<BuildingHealth>().buildingKey > buildingKey) {
					buildingKey = checkColliders2[j].GetComponent<BuildingHealth>().buildingKey; 
					buildingKeyHeight = checkColliders2[j].GetComponent<BuildingHealth>().buildingKeyHeight; 
				} 
				if(checkColliders2[j].GetComponent<BuildingHealth>().buildingKey < buildingKey) {
					checkColliders2[j].GetComponent<BuildingHealth>().buildingKeyFixer(); 
				} 
			}	
		}	
	}
}









