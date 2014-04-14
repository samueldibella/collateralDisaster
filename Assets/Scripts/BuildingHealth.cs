using UnityEngine;
using System.Collections;

public class BuildingHealth : MonoBehaviour {

	//relevant game object 
	public GameObject building; 
	public Material buildingGreen;
	public Material fireRed;
	
	//total score
	public static float totalScore;
	
	//building values
	float monetaryValue; 
	public float health; 
	
	//fire stuff
	public bool fireStarted; 
	public bool onFire; 
	public float fireIntensity; 
	public float fireDamage; 
	
	// Use this for initialization
	void Start () {
		//building values 
		health = 100; 
		monetaryValue = 100; 
		totalScore += monetaryValue;
		
		//fire stuff 
		fireStarted = false;
		onFire = false; 		
		fireIntensity = 0; 
		fireDamage = 0; 
	}
	
	// Update is called once per frame
	void Update () {
		//building destroy when health = 0 and subtracts score 
		if(health <= 0) {
			totalScore -= monetaryValue;
			Destroy(gameObject); 		
		}
		//fire methods  
		if(fireStarted == true) {
			renderer.material.color = Color.red;
			burning(); 
			onFire = true; 
			fireStarted = false; 
		}
		//updates health if on fire 
		if(onFire == true) {
			health = 100 - fireDamage;	
			if(fireIntensity >= 50) {
				Collider[] hitColliders = Physics.OverlapSphere(transform.position, 6f); 
				int i = 0;
				while (i < hitColliders.Length) {
					if(hitColliders[i].tag.Equals("Building") == true && hitColliders[i].GetComponent<BuildingHealth>().onFire == false) {
						hitColliders[i].GetComponent<BuildingHealth>().fireStarted = true;
					}
					i++;
				}
			}	
		}
		//if it stopes being on fire it resets fire Intensity and stops the corutines from runnning. 
		if(onFire == false) {
			renderer.material.color = Color.green;
			StopCoroutine("fireIntensityIncreaser"); 
			StopCoroutine("fireDamageIncreaser"); 
			StopCoroutine("fireSpread"); 
			fireIntensity = 0;	 		
		}
	
	}
	//burning method controls the rate of burning and fire intensity 
	void burning() {
		if(fireIntensity < 100) {
			StartCoroutine( "fireIntensityIncreaser");
		}
		if(fireIntensity >= 50) {
			StartCoroutine("fireSpread"); 
		}	
		StartCoroutine( "fireDamageIncreaser" );		
	}
	void fireSpread() {
		StartCoroutine("fireSpread"); 
	
	}
	//coroutine that increase fireintesity until it gets to 100 
	IEnumerator fireIntensityIncreaser() {		
		while(true) {
			if( fireIntensity >= 100) {
				yield break; 
			} else {
				fireIntensity += 5f; 
				yield return new WaitForSeconds(1);
			}
		}
	}
	//coroutine that increase the damage the fire is doing based on the intensity of the fire. 
	IEnumerator fireDamageIncreaser() {		
		while(true) {
			fireDamage += (.05f * fireIntensity); 
			yield return new WaitForSeconds(1);
		}
	}
	IEnumerator fireSpreadIncreaser() {		
		print ("df"); 
		while(true) {
			print ("df");
			Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10f); 
			int i = 0;
			while (i < hitColliders.Length) {
				if(hitColliders[i].tag.Equals("Building") == true && hitColliders[i].GetComponent<BuildingHealth>().onFire == false) {
					hitColliders[i].GetComponent<BuildingHealth>().fireStarted = true;
					i++;
				}
			}
			yield return new WaitForSeconds(1);
		}
	}
}









