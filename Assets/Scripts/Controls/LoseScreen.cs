using UnityEngine;
using System.Collections;

public class LoseScreen : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.R)) {
			BuildingHealth.quad4Array = new GameObject[1000];
			BuildingHealth.keyIncrementer = 0; 
			BuildingHealth.quadIterator4 = 0; 
			BuildingHealth.keyBuilding4Selected = false; 
			BuildingHealth.keyBuilding4 = -1; 
			StreetGeneration.streetMap = new int[601, 601]; 
			StreetGeneration.currentStreetMakers = 1;
			StreetGeneration.madeStreetMakers = 1; 
			StreetGeneration.endedStreetMakers = 0;
			Spawn.spawnSelected = false; 
			Infrastructure.totalStructure = 1; 
			
			//vairbles that will change to make the game harder 
			BuildingHealth.fireRate += 1; 
			RngBuilding.buildingSpawnRate = 11; 
			Application.LoadLevel("FullBuild");
		}
		
	}
}
