using UnityEngine;
using System.Collections;

public class BuildingGenerator2 : MonoBehaviour {
	
	public Transform buildingTile;
	int street = 1; 
	int building = 2;
	int buildingSpace = 4; 
	int coordinateXx = 104; 
	int coordinateZx = 154;  
				   
	// Method that makes buildings 
	   public void buildingGenerator() {  
		for(int i = 0; i < StreetGeneration.gridLengthZ; i++) {
			for(int j = 0; j < StreetGeneration.gridLengthX; j++) {
				if(MapCheck(coordinateXx + j,coordinateZx + i) == true) {
					Mapfill(coordinateXx + j,coordinateZx + i); 
					StreetGeneration.streetMap[coordinateXx+j,coordinateZx+i] = building;
				}
			}
		}
		MakeMap(); 
	}
	
	void MakeMap() {
		for(int z = 0; z < 400; z++) {
			for(int x = 0; x < 400; x++) {
				if(StreetGeneration.streetMap[x, z] == building) {
					Instantiate (buildingTile, new Vector3((float)x, 1f, ((float)z)), Quaternion.identity);
				}
			}		
		}
	}
	bool MapCheck(int x, int z) { 
		for(int i = -2; i < 2; i++) {
			for(int j = -2; j < 2; j++) {
				if(StreetGeneration.streetMap[x+j, z+i] == street || StreetGeneration.streetMap[x+j, z+i] == building
				   || StreetGeneration.streetMap[x+j, z+i] == buildingSpace) {
					return false; 
				}
			}
		}
		return true; 
	}
	void Mapfill(int x, int z){
		for(int i = -2; i < 2; i++) {
			for(int j = -2; j < 2; j++) {
				if(StreetGeneration.streetMap[x+j, z+i] != street || StreetGeneration.streetMap[x+j, z+i] != building){
					StreetGeneration.streetMap[x+j, z+i] = buildingSpace;
				}
			}
		}
	}
}






