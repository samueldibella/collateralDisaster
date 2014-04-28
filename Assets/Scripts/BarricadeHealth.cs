using UnityEngine;
using System.Collections;

public class BarricadeHealth : MonoBehaviour {

	public float health; 

	//fire stuff
	public bool fireStarted; 
	public bool onFire; 
	public float fireIntensity; 
	public float fireDamage; 
	public float fireRate;

	// Use this for initialization
	void Start () {
		health = 100;
	 	fireStarted = false;
		onFire = false;
		fireIntensity = 0;
	 	fireDamage = 0;
	 	fireRate = 1;

	}
	
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
					if(hitColliders[i].tag == "Building" && hitColliders[i].GetComponent<BuildingHealth>().buildingKey == hitColliders[i].GetComponent<BuildingHealth>().buildingKey &&
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
}
