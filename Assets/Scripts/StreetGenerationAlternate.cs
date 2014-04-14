using UnityEngine;
using System.Collections;

public class StreetGenerationAlternate : MonoBehaviour {
	
	//prefabs
	public Transform streetMakerPrefab;
	public Transform streetTilePreFab;
	public Transform wallPreFab; 
	public BuildingGenerator2 builder; 
	
	//array for building street
	public static int[,] streetMap = new int[1000, 1000];
	//int nothing = 0; 
	int street = 1; 
	int constantStreets = 3; 
	
	int streetLength = 10; 
	int streetWidth = 2; 
	
	int cityLength = 100; 
	
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
	
	int lowerBound = 150; 
	int middleBound = 200; 
	int upperBound = 250; 
	
	// Use this for initialization
	void Start () {
		//intial x and z 
		int currentX = (int)transform.position.x*2; 
		int currentZ = (int)transform.position.z*2;
		
		int quadrant = getQuadrant(currentX,currentZ); 
		if( quadrant == 1) {
			currentXMin = lowerBound*2; 
			currentZMin = middleBound*2; 
			
			currentXMax = middleBound*2; 
			currentZMax = upperBound*2; 
		}
		if(quadrant == 2) {
			currentXMin = middleBound*2; 
			currentZMin = middleBound*2; 
			
			currentXMax = upperBound*2; 
			currentZMax = upperBound*2; 
		}
		if(quadrant == 3) {
			currentXMin = lowerBound*2; 
			currentZMin = lowerBound*2; 
			
			currentXMax = middleBound*2; 
			currentZMax = middleBound*2; 
		}
		if(quadrant == 4) {
			currentXMin = middleBound*2; 
			currentZMin = lowerBound*2; 
			
			currentXMax = upperBound*2; 
			currentZMax = middleBound*2; 
		}
		MapFill(middleBound, middleBound, 0); 
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
			print ("df"); 
			MakeMap(); 
			builder.buildingGenerator();  
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
				for(int z = 0; z < cityLength; z++) {
					streetMap[lowerBound+x,lowerBound+z] = constantStreets; 
				}
			}
			for(int x = 0; x < streetWidth; x++) {
				for(int z = 0; z < cityLength; z++) {
					streetMap[middleBound+x,lowerBound+z] = constantStreets; 
				}
			}	
			for(int x = 0; x < streetWidth; x++) {
				for(int z = 0; z < cityLength; z++) {
					streetMap[upperBound+x,lowerBound+z] = constantStreets; 
				}
			}		
			//horizontal preset streets
			for(int x = 0; x < cityLength; x++) {
				for(int z = 0; z < streetWidth; z++) {
					streetMap[lowerBound+x,lowerBound+z] = constantStreets;  
				}
			}		
			for(int x = 0; x < cityLength; x++) {
				for(int z = 0; z < streetWidth; z++) {
					streetMap[lowerBound+x,middleBound+z] = constantStreets;  
				}
			}		
			for(int x = 0; x < cityLength; x++) {
				for(int z = 0; z < streetWidth; z++) {
					streetMap[lowerBound+x,upperBound+z] = constantStreets;  
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
					Instantiate (streetTilePreFab, new Vector3((float)x/2, 1f, (float)z/2), Quaternion.identity);
				}
				if(streetMap[x, z] == constantStreets) {
					Instantiate (streetTilePreFab, new Vector3((float)x*2, 1f, (float)z*2), Quaternion.identity);
				}
			}
			
		}
	}
	int getQuadrant(int x, int z) {
		if( x < middleBound && z > middleBound) {
			return 1; 
		}
		if( x > middleBound && z > middleBound) {
			return 2;
		}
		if( x < middleBound && z < middleBound) {
			return 3;
		}
		if( x > middleBound && z < middleBound) {
			return 4;
		}	
		return 0; 
	}
}

