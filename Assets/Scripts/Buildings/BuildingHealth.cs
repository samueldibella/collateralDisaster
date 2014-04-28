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
	public bool isShielded;
	
	//fire stuff
	public bool fireStarted; 
	public bool onFire; 
	public float fireIntensity; 
	public float fireDamage; 
	public float fireRate;
	
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
		isShielded = false;
		
		//fire stuff 
		fireStarted = false;
		onFire = false; 		
		fireIntensity = 0; 
		fireDamage = 0; 
		fireRate = 1;
		
		//water stuff
		firstFlood = false; 
		floodStarted = false; 
		waterLogged = false; 
		waterLoggedPercent = 0; 
		waterDamage = 0; 
		
		//assign buiding key 
		assignBuildingKey(); 
		buildingKeyFixer();
		
		//Corutines 
		StartCoroutine("waterCheck"); 
	}
	
	// Update is called once per frame
	void Update () {
		//building destroy when health = 0 and subtracts score 
		if(health <= 0) {
			Infrastructure.totalStructure -= infrastructureValue;
			Destroy(gameObject); 		
		}
		
		//fire methods  
		if(fireStarted == true) {
			StartCoroutine( Fire() );
			fireStarted = false; 
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
	}
	
	
	//methods
	//burning method controls the rate of burning and fire intensity 
	IEnumerator Fire() {
		int maxOxygenIndex;
		bool fireSpread = false;
		onFire = true;
		

		
		while(onFire) {

			fireIntensity += 5;
			fireSpread = false;
			
			
			if(fireIntensity > 50) {
				health -= fireIntensity / 10;
				Collider[] hitColliders = Physics.OverlapSphere(transform.position, 6f); 
				maxOxygenIndex = -1;
				
				for(int i = 0; i < hitColliders.Length; i++) {
					if(hitColliders[i].tag == "Building" && hitColliders[i].GetComponent<BuildingHealth>().buildingKey == buildingKey &&
					   hitColliders[i].GetComponent<BuildingHealth>().onFire == false) {
						hitColliders[i].GetComponent<BuildingHealth>().fireStarted = true;
						fireSpread = true;
					}
					
					if(hitColliders[i].tag == "Barricade" && hitColliders[i].GetComponent<BuildingHealth>().onFire == false) {
						hitColliders[i].GetComponent<BuildingHealth>().fireStarted = true;
						fireSpread = true;
					}
				}	
				
							
				if(!fireSpread) {
					for(int i = 0; i < hitColliders.Length; i++) {
						//special case for beginning
						if(hitColliders[i].tag == "Building") {
							if(maxOxygenIndex == -1) {
								maxOxygenIndex = i;
								
								//compares to all other buildings found, and must be on fire to count
							} else if(hitColliders[i].GetComponent<PersonalAtmo>().personalAtmo.GetComponent<gasQualities>().oxygen >= 
							          hitColliders[maxOxygenIndex].GetComponent<PersonalAtmo>().personalAtmo.GetComponent<gasQualities>().oxygen &&
							          hitColliders[i].GetComponent<BuildingHealth>().onFire == false) {
								maxOxygenIndex = i;
							}
						}	
					}
						
					if(maxOxygenIndex != -1) {
						hitColliders[maxOxygenIndex].GetComponent<BuildingHealth>().fireStarted = true;
					}
				}		
			}
			
			yield return new WaitForSeconds(fireRate);
		}
	}
	
	//flooding method that conrols the corutines that conrtol flooding 
	void flooding(){
		if(waterLoggedPercent < 100) {
			StartCoroutine( "waterIntensityIncreaser");
		}
			
		StartCoroutine( "waterDamageIncreaser" );		
	}
	
	//Couroutines
	
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
	
	IEnumerator waterCheck() {		
		Collider[] hitCollidersWater = Physics.OverlapSphere(transform.position, 4f); 
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
				waterLogged = false; 
			}
			
			j++;
		}
		
		yield return new WaitForSeconds(1);	
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
					//buildingKeyHeight = checkColliders2[j].GetComponent<BuildingHealth>().buildingKeyHeight; 
				} 
				
				if(checkColliders2[j].GetComponent<BuildingHealth>().buildingKey < buildingKey) {
					checkColliders2[j].GetComponent<BuildingHealth>().buildingKeyFixer(); 
				} 
			}	
		}	
	}
}