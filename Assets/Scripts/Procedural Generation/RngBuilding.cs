using UnityEngine;
using System.Collections;

public class RngBuilding : MonoBehaviour {


	public Transform buildingTile;
	public bool occupied = false; 
	
	bool runOnce = false; 
	public static int buildingSpawnRate = 11; 
	
	// Use this for initialization
	void Update() {
		if(runOnce == false) {
			Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f);
			for(int i = 0; i < hitColliders.Length; i++) {
				if(hitColliders[i].tag == "Building") { 
					occupied = true; 
				}
			}
			if(occupied == false && transform.position.x > 110) {
				Vector3 filler = new Vector3(0,1,0); 
				if(Random.Range(0,buildingSpawnRate) == 0) {
					Instantiate (buildingTile, (transform.position + filler), Quaternion.identity);
				}
			}
			runOnce = true; 
		} 		
	}
}
