using UnityEngine;
using System.Collections;

public class MasterArray : MonoBehaviour {
	//master array that will be used to check if water/buildings are next to eachother 
	public static int [,] cityArray = StreetGeneration.streetMap;
	
	//legend 
	int nothing = 0; 
	int blankStreet = 1;
	int building = 2;  
	int buildingSpace = 3; 	
	int burningBuilding = 4; 
	int floodedBuilding = 5; 
	int water = 6; 
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
