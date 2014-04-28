using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour {

	public enum Disaster {Flood, Fire, Gas, None}
	
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
		
		currentDisaster = Disaster.None;
		//secondaryColor = new Color(170, 17, 186);
		//primaryColor = new Color(96, 8, 105);
		
		StartCoroutine( Beginning() );
	}
	
	IEnumerator Beginning() {
		bool fireStarted = false;
		bool floodStarted = false;
		int secondaryImports = 0;
		//int primaryImports = 0;
		
		while(secondaryImports < 1) {
			if( Input.GetKeyDown(KeyCode.Mouse0)) {
				
				ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				
				if(Physics.Raycast(ray, out rayHit) && rayHit.transform.tag == "Building") {
					rayHit.transform.GetComponent<BuildingHealth>().infrastructureValue = 5;
					rayHit.transform.GetComponent<BuildingDisplay>().initialColor = Color.cyan;
					
					secondaryImports++;
				}			
			}
			
			yield return 0;
		}
		
		//initial disaster placements
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
		
		currentDisaster = Disaster.None;
		Time.timeScale = 1;
	}
}
