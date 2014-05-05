using UnityEngine;
using System.Collections;

public class RoadDisplay : MonoBehaviour {
	//attach to street prefab
	
	Vector3 storageLocation;
	Ray ray;
	RaycastHit hit;

	public bool fireStarted = false;
	public bool onFire = false;
	public float health = 1;
	public static float fireRateRoad = 2; 
	Color initialColor = Color.white;
	Color intermediate;
	
	void Start() {
		//waterInstance = Instantiate(waterMarker, storageLocation, Quaternion.identity) as GameObject;
	}
	
	void Update() {
		if(fireStarted == true) {
			StartCoroutine( Fire() );
			fireStarted = false;
		}
		
		intermediate = Color.Lerp(initialColor, Color.red, health);
	}
	
	IEnumerator Fire() {
		onFire = true;
		float newY;
	
		while(health > 0) {
			health -= .1f;
		
			yield return new WaitForSeconds(fireRateRoad);
		}
		
		yield return new WaitForSeconds(Random.Range(3f, 5f));
		
		while(transform.position.y > -5) {
			newY = transform.position.y - .05f;
			
			transform.position = new Vector3(transform.position.x, newY, transform.position.z);
			
			yield return new WaitForSeconds(.01f);
		}
		
		Destroy(this.gameObject);
	}
	
	
}
