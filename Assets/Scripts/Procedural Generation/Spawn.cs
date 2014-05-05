using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	public Transform player; 
	
	bool runOnce = false; 
	public static bool spawnSelected = false; 
	bool occupied = false; 
	
	// Update is called once per frame
	void Update () {
		if(runOnce == false && transform.position.x < 110) {
			Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f);
			for(int i = 0; i < hitColliders.Length; i++) {
				if(hitColliders[i].tag == "Building" || hitColliders[i].tag == "Building RNG") { 
					occupied = true; 
				}
			}
			if(occupied == false && spawnSelected == false) {
				if(Random.Range(0,4) == 0) {			
					Vector3 spawn = transform.position + new Vector3(0,2,0); 
					Instantiate (player, spawn, Quaternion.identity); 
					spawnSelected = true; 
				}
			}
			runOnce = true; 
		} 	
	}
}




