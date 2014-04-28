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
	
	//bounds  
	int middleBoundX; 
	
	int middleBoundZ;   
	
	public int quadrant; 
	
	//building key 
	public static int keyIncrementer;  
	public int buildingKey; 
	bool gotKey = false; 
	
	public static GameObject[] quad1Array = new GameObject[300]; 
	public static GameObject[] quad2Array = new GameObject[300];
	public static GameObject[] quad3Array = new GameObject[300];
	public static GameObject[] quad4Array = new GameObject[300];
	
	public static int quadIterator1; 
	public static int quadIterator2; 
	public static int quadIterator3; 
	public static int quadIterator4; 
	
	public static bool keyBuilding1Selected = false; 
	public static bool keyBuilding2Selected = false;
	public static bool keyBuilding3Selected = false;
	public static bool keyBuilding4Selected = false; 
	
	public static int keyBuilding1; 
	public static int keyBuilding2;
	public static int keyBuilding3; 
	public static int keyBuilding4; 
	
	void Awake() {
		
		middleBoundX = 218; 
		
		middleBoundZ = 208; 
		
		//assign buiding key 
		assignBuildingKey(); 
		buildingKeyFixer();	
		
		//make 4 arrays of each quadrant
		quadrant = getQuadrant((int)transform.position.x, (int)transform.position.z); 	
		if(quadrant == 1) {
			quad1Array[quadIterator1] = gameObject; 
			quadIterator1++; 
		} 
		if(quadrant == 2) {
			quad2Array[quadIterator2] = gameObject; 
			quadIterator2++; 
		} 
		if(quadrant == 3) {
			quad3Array[quadIterator3] = gameObject; 
			quadIterator3++; 
		} 
		if(quadrant == 4) {
			quad4Array[quadIterator4] = gameObject; 
			quadIterator4++; 
		} 			
	}
	
	// Use this for initialization
	void Start () {
	
		buildingKeyFixer();
		
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
		
		if(keyBuilding1Selected == false) {  
			keyBuilding1 = quad1Array[Random.Range(0, quadIterator1+1)].GetComponent<BuildingHealth>().buildingKey; 
			keyBuilding1Selected = true; 
		}
		if(keyBuilding2Selected == false) {  
			keyBuilding2 = quad2Array[Random.Range(0, quadIterator2+1)].GetComponent<BuildingHealth>().buildingKey; 
			keyBuilding2Selected = true; 
		}
		if(keyBuilding3Selected == false) {  
			keyBuilding3 = quad3Array[Random.Range(0, quadIterator3+1)].GetComponent<BuildingHealth>().buildingKey; 
			keyBuilding3Selected = true; 
		}
		if(keyBuilding4Selected == false) {
			keyBuilding4 = quad4Array[Random.Range(0, quadIterator4+1)].GetComponent<BuildingHealth>().buildingKey; 
			keyBuilding4Selected = true; 
		}
		
		StartCoroutine( Oxygen() );
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
					
					if(hitColliders[i].tag == "Barricade" && hitColliders[i].GetComponent<BarricadeHealth>().onFire == false) {
						hitColliders[i].GetComponent<BarricadeHealth>().fireStarted = true;
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
	
	IEnumerator Oxygen() {
		yield return new WaitForSeconds(1);
	
		if(buildingKey == keyBuilding1 || buildingKey == keyBuilding2 || buildingKey == keyBuilding3|| buildingKey == keyBuilding4) {
			GetComponent<PersonalAtmo>().personalAtmo.GetComponent<gasQualities>().isOxygenGen = true;
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
	int getQuadrant(int x, int z) {
		if( x < middleBoundX && z > middleBoundZ) {
			return 1; 
		}
		if( x > middleBoundX && z > middleBoundZ) {
			return 2;
		}
		if( x < middleBoundX && z < middleBoundZ) {
			return 3;
		}
		if( x > middleBoundX && z < middleBoundZ) {
			return 4;
		}	
		return 0; 
	}
}