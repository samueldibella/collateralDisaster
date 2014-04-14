using UnityEngine;
using System.Collections;

public class StreetGeneration : MonoBehaviour {
	
	//prefabs
	public Transform streetMakerPrefab;
	public Transform streetTile; 
	
	//array for building street
	public static int[,] streetMap = new int[1000, 1000];
	//int nothing = 0; 
	int street = 1; 
	
	int streetLength = 10; 
	int streetWidth = 1; 
	
	//counters
	int currentStreets = 0; 
	int maxStreets = 5;
	
	static int currentStreetMakers = 1;
	static int madeStreetMakers = 1; 
	static int endedStreetMakers = 0;
	static int maxStreetMakers = 64;
	
	int currentXMin; 
	int currentZMin; 
	
	int currentXMax; 
	int currentZMax;  
	
	public static bool streetsDone = false; 
	 
	// Use this for initialization
	void Start () {
		//intial x and z 
		int currentX = (int)transform.position.x; 
		int currentZ = (int)transform.position.z;
		
		int quadrant = getQuadrant(currentX,currentZ); 
		if( quadrant == 1) {
			currentXMin = 450; 
			currentZMin = 500; 
				
			currentXMax = 500; 
			currentZMax = 550; 
			}
		if(quadrant == 2) {
			currentXMin = 500; 
			currentZMin = 500; 
			
			currentXMax = 550; 
			currentZMax = 550; 
			}
		if(quadrant == 3) {
			currentXMin = 450; 
			currentZMin = 450; 
			
			currentXMax = 500; 
			currentZMax = 500; 
			}
		if(quadrant == 4) {
			currentXMin = 500; 
			currentZMin = 450; 
			
			currentXMax = 550; 
			currentZMax = 500; 
			}
		MapFill(500, 500, 0); 
		while(currentStreets < maxStreets) { 
			int randomStreetNumber = Random.Range (0,101);
			//north
			if(randomStreetNumber >= 0 && randomStreetNumber < 25 && currentZ < currentZMax) {
				MapFill(currentX, currentZ, 1); 
				currentZ += streetLength; 
				currentStreets++;
			}
			//south
			if(randomStreetNumber >= 25 && randomStreetNumber < 50 && currentZ > currentZMin) {
				MapFill(currentX, currentZ, 2);
				currentZ -= streetLength; 
				currentStreets++;
			}
			//west
			if(randomStreetNumber >= 50 && randomStreetNumber < 75 & currentX > currentXMin) {
				MapFill(currentX, currentZ, 3); 
				currentX -= streetLength;
				currentStreets++;
			}
			//east
			if(randomStreetNumber >= 75 && randomStreetNumber <= 100 && currentX < currentXMax) {
				MapFill(currentX, currentZ, 4); 
				currentX += streetLength;
				currentStreets++;
			}			
		}
		
		if(currentStreets == maxStreets && endedStreetMakers != maxStreetMakers ) {
			endedStreetMakers++;   
			madeStreetMakers++; 
			currentStreetMakers--; 
			Destroy(gameObject);
			}	
		if(currentStreets == maxStreets && endedStreetMakers == maxStreetMakers) { 
			MakeMap(); 
			streetsDone = true; 
			Destroy(gameObject); 			
		}						
	}
	// Update is called once per frame
	void Update () {
		
	}
	
	void MapFill (int xCoordinate, int zCoordinate, int k) {
		
		switch(k) {	
		case(0): 
			//vertical preset streets
			for(int x = 0; x < streetWidth; x++) {
				for(int z = 0; z < 100; z++) {
					streetMap[450+x,450+z] = street; 
				}
			}
			for(int x = 0; x < streetWidth; x++) {
				for(int z = 0; z < 100; z++) {
					streetMap[500+x,450+z] = street; 
				}
			}	
			for(int x = 0; x < streetWidth; x++) {
				for(int z = 0; z < 100; z++) {
					streetMap[550+x,450+z] = street; 
				}
			}		
			//horizontal preset streets
			for(int x = 0; x < 100; x++) {
				for(int z = 0; z < streetWidth; z++) {
					streetMap[450+x,450+z] = street;  
				}
			}		
			for(int x = 0; x < 100; x++) {
				for(int z = 0; z < streetWidth; z++) {
					streetMap[450+x,500+z] = street;  
				}
			}		
			for(int x = 0; x < 100; x++) {
				for(int z = 0; z < streetWidth; z++) {
					streetMap[450+x,550+z] = street;  
				}
			}				
			break;	
		case(1): 
			for(int x = 0; x < streetWidth; x++) {
				for(int z = 0; z < streetLength; z++) {
					streetMap[xCoordinate+x,zCoordinate+z] = street; 
				}
			}		
			break;
		case(2): 
			for(int x = 0; x < streetWidth; x++) {
				for(int z = 0; z < streetLength; z++) {
					streetMap[xCoordinate+x,zCoordinate-z] = street;  
				}
			}	
			break;
		case(3): 
			for(int x = 0; x < streetLength; x++) {
				for(int z = 0; z < streetWidth; z++) {
					streetMap[xCoordinate-x,zCoordinate+z] = street;  
				}
			}	
			break;
		case(4): 
			for(int x = 0; x < streetLength; x++) {
				for(int z = 0; z < streetWidth; z++) {
					streetMap[xCoordinate+x,zCoordinate+z] = street;  
				}
			}	
			break;
		}
	}
	void MakeMap() {
		for(int z = 0; z < 1000; z++) {
			for(int x = 0; x < 1000; x++) {
				if(streetMap[x, z] == street) {
					Instantiate (streetTile, new Vector3((float)x, 1f, (float)z), Quaternion.identity);
				}
			}
			
		}
	}
	int getQuadrant(int x, int z) {
		if( x < 500 && z > 500) {
			return 1; 
		}
		if( x > 500 && z > 500) {
			return 2;
		}
		if( x < 500 && z < 500) {
			return 3;
		}
		if( x > 500 && z < 500) {
			return 4;
		}	
		return 0; 
	}
}












