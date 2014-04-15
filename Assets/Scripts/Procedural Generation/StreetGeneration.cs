using UnityEngine;
using System.Collections; 

public class StreetGeneration : MonoBehaviour {
	
	//prefabs
	public Transform streetMakerPrefab;
	public Transform streetTilePreFab;
	public Transform streetPlanePreFab; 
	public Transform wallPreFab; 
	public BuildingGenerator2 builder; 
	
	//array for building street
	public static int[,] streetMap = new int[601, 601];
	//int nothing = 0; 
	int street = 1; 
	int wall = 3; 
	
	int streetLength = 12; 
	int streetWidth = 4; 
	
	
	//counters
	int currentStreets = 0; 
	int maxStreets = 5;
	
	static int currentStreetMakers = 1;
	static int madeStreetMakers = 1; 
	static int endedStreetMakers = 0;
	static int maxStreetMakers = 128;
	
	int currentXMin; 
	int currentZMin; 
	
	int currentXMax; 
	int currentZMax;  
	
	static int lowerBoundX = 110-12; 
	static int middleBoundX = 206+12; 
	static int upperBoundX = 326+12; 
	
	static int lowerBoundZ = 160-12; 
	static int middleBoundZ = 196+12; 
	static int upperBoundZ = 256+12; 
	
	public static int gridLengthX = upperBoundX - lowerBoundX; 
	public static int gridLengthZ = upperBoundZ - lowerBoundZ; 
	
	// Use this for initialization
	void Start () {
		//intial x and z 
		int currentX = (int)transform.position.x; 
		int currentZ = (int)transform.position.z;
		
		int quadrant = getQuadrant(currentX,currentZ); 
		if( quadrant == 1) {
			currentXMin = lowerBoundX; 
			currentZMin = middleBoundZ; 
			
			currentXMax = middleBoundX; 
			currentZMax = upperBoundZ; 
		}
		if(quadrant == 2) {
			currentXMin = middleBoundX; 
			currentZMin = middleBoundZ; 
			
			currentXMax = upperBoundX; 
			currentZMax = upperBoundZ; 
		}
		if(quadrant == 3) {
			currentXMin = lowerBoundX; 
			currentZMin = lowerBoundZ; 
			
			currentXMax = middleBoundX; 
			currentZMax = middleBoundZ; 
		}
		if(quadrant == 4) {
			currentXMin = middleBoundX; 
			currentZMin = lowerBoundZ; 
			
			currentXMax = upperBoundX; 
			currentZMax = middleBoundZ; 
		}
		MapFill(middleBoundX, middleBoundZ, 0); 
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
				for(int z = 0; z < gridLengthZ; z++) {
					streetMap[(lowerBoundX+x),(lowerBoundZ+z)] = street; 
				}
			}
			for(int x = 0; x < streetWidth; x++) {
				for(int z = 0; z < gridLengthZ; z++) {
					streetMap[(middleBoundX+x),(lowerBoundZ+z)] = street; 
				}
			}	
			for(int x = 0; x < streetWidth; x++) {
				for(int z = 0; z < gridLengthZ; z++) {
					streetMap[(upperBoundX+x),(lowerBoundZ+z)] = street; 
				}
			}		
			//horizontal preset streets
			for(int x = 0; x < gridLengthX; x++) {
				for(int z = 0; z < streetWidth; z++) {
					streetMap[(lowerBoundX+x),(lowerBoundZ+z)] = street;  
				}
			}		
			for(int x = 0; x < gridLengthX; x++) {
				for(int z = 0; z < streetWidth; z++) {
					streetMap[(lowerBoundX+x),(middleBoundZ+z)] = street;  
				}
			}		
			for(int x = 0; x < gridLengthX; x++) {
				for(int z = 0; z < streetWidth; z++) {
					streetMap[(lowerBoundX+x),(upperBoundZ+z)] = street;  
				}
			}				
			break;	
		//north
		case(1): 
			for(int x = 0; x < streetWidth; x++) {
				for(int z = 0; z < streetLength; z++) {
					streetMap[(xCoordinate+x),(zCoordinate+z)] = street; 
				}
			}		
			break;
		//south
		case(2): 
			for(int x = 0; x < streetWidth; x++) {
				for(int z = 0; z < streetLength; z++) {
					streetMap[(xCoordinate+x),(zCoordinate-z)] = street;  
				}
			}	
			break;
		//west
		case(3): 
			for(int x = 0; x < streetLength; x++) {
				for(int z = 0; z < streetWidth; z++) {
					streetMap[(xCoordinate-x),(zCoordinate+z)] = street;  
				}
			}	
			break;
		//east
		case(4): 
			for(int x = 0; x < streetLength; x++) {
				for(int z = 0; z < streetWidth; z++) {
					streetMap[(xCoordinate+x),(zCoordinate+z)] = street;  
				}
			}	
			break;
		}
	}
	void MakeMap() {
		Instantiate (streetPlanePreFab, new Vector3(220f, 1f, 210f), Quaternion.identity);
//		for(int z = 0; z < 400; z++) {
//			for(int x = 0; x < 400; x++) {
//				if(streetMap[x, z] == street) {
//					Instantiate (streetTilePreFab, new Vector3(((float)x+.5f), 1f, ((float)z)+.5f), Quaternion.identity);
//				}
//			}
//			
//		}
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
























