using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour {

	public enum Disaster {Flood, Fire, Gas}
	
	//next disaster to appear
	public static Disaster currentDisaster;
	
	//prefabs for disaster placement
	public GameObject floodMaker;
	
	public float disasterRate = 20f;
	
	RaycastHit rayHit;
	Ray ray;
	
	// Use this for initialization
	void Start () {
		Time.timeScale = 0;
		
		StartCoroutine( DisasterManager() );
	}
	
	IEnumerator DisasterManager() {
		bool fireStarted = false;
		bool floodStarted = false;
		
		currentDisaster = Disaster.Fire;
		while(!fireStarted) {
			if( Input.GetKeyDown(KeyCode.Mouse0)) {
				
				ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				
				if(Physics.Raycast(ray, out rayHit) && rayHit.transform.tag == "Building") {
					rayHit.transform.GetComponent<BuildingHealth>().fireStarted = true;
					fireStarted = true;
				}			
			}
			
			yield return 0;
		}
		
		currentDisaster = Disaster.Flood;
		while(!floodStarted) {
			if( Input.GetKeyDown(KeyCode.Mouse0)) {
				
				ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				
				if(Physics.Raycast(ray, out rayHit) && rayHit.transform.tag == "Road") {
					Vector3 location = rayHit.point;
					location.y += .5f;
					
					Instantiate(floodMaker, location, Quaternion.identity);
					floodStarted = true;			
				}
			}
			
			yield return 0;
		}
		
		Time.timeScale = 1;
	}
}
