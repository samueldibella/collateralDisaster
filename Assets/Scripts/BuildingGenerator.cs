using UnityEngine;
using System.Collections;

public class BuildingGenerator : MonoBehaviour {

	public Transform buildingSquare1; 
	public Transform buildingSquare2; 
	public Transform buildingSquare3; 
	public Transform buildingCylinder1; 
	public Transform buildingCylinder2; 
	public Transform buildingCylinder3; 
	public Transform buildingSphere1; 
	public Transform buildingSphere2; 
	public Transform buildingSphere3; 
	
	int buildingDistance = 10; 
	
	// Use this for initialization
	void Start () { 
	
		float currentX = 455; 
		float currentZ = 455;
		
		if(StreetGeneration.streetsDone == true) {
			for(int i = 0; i < 10; i++) {
				for(int j = 0; j < 10; j++) {
					int randomBuildingNumber = Random.Range (0,3);
					float randomBuildingHeight = Random.Range (.5f,2f);
					if(randomBuildingNumber == 0) {
						Instantiate (buildingSquare1, new Vector3(currentX, randomBuildingHeight, currentZ), Quaternion.identity);
					}
					if(randomBuildingNumber == 1) {
						Instantiate (buildingSquare2, new Vector3(currentX, randomBuildingHeight, currentZ), Quaternion.identity);
					}
					if(randomBuildingNumber == 2) {
						Instantiate (buildingSquare3, new Vector3(currentX, randomBuildingHeight, currentZ), Quaternion.identity);
					}
					if(randomBuildingNumber == 3) {
						Instantiate (buildingCylinder1, new Vector3(currentX, randomBuildingHeight, currentZ), Quaternion.identity);
					}
					if(randomBuildingNumber == 4) {
						Instantiate (buildingCylinder2, new Vector3(currentX, randomBuildingHeight, currentZ), Quaternion.identity);
					}
					if(randomBuildingNumber == 5) {
						Instantiate (buildingCylinder3, new Vector3(currentX, randomBuildingHeight, currentZ), Quaternion.identity);
					}
					if(randomBuildingNumber == 6) {
						Instantiate (buildingSphere1, new Vector3(currentX, randomBuildingHeight, currentZ), Quaternion.identity);
					}
					if(randomBuildingNumber == 7) {
						Instantiate (buildingSphere2, new Vector3(currentX, randomBuildingHeight, currentZ), Quaternion.identity);
					}
					if(randomBuildingNumber == 8) {
						Instantiate (buildingSphere3, new Vector3(currentX, randomBuildingHeight, currentZ), Quaternion.identity);
					}
					currentX += buildingDistance; 
				}
				currentX = 455;
				currentZ += buildingDistance; 
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
