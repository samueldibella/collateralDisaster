using UnityEngine;
using System.Collections;

public class disasterGeneration : MonoBehaviour {

	public enum Disaster {Flood, Fire, Gas}

	//next disaster to appear
	public static Disaster currentDisaster;
	
	//prefabs for disaster placement
	public GameObject floodMaker;

	public float disasterRate = 20f;
	
	int randomHolder;

	RaycastHit rayHit;
	Ray ray;
	
	// Use this for initialization
	void Start () {
		Time.timeScale = 0;
		
		StartCoroutine( DisasterManager() );
	}
	
	IEnumerator DisasterManager() {
		while(true) {
			if(Time.timeScale == 1) {
				yield return new WaitForSeconds(disasterRate);
				
				Time.timeScale = 0;
				
				//generate disaster
				randomHolder = Random.Range(0, 2);
				
				switch(randomHolder) {
				case 0:
					currentDisaster = Disaster.Fire;
					break;
				case 1:
					currentDisaster = Disaster.Flood;
					break;
				case 2:
					currentDisaster = Disaster.Gas;
					break;
				}
			} else {
				if( Input.GetKeyDown(KeyCode.Mouse0)) {
					
					ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					
					if(Physics.Raycast(ray, out rayHit)) {
						switch(currentDisaster) {
						case Disaster.Fire:
							if(rayHit.transform.tag == "Building" ) {
								rayHit.transform.GetComponent<BuildingHealth>().fireStarted = true;
								Time.timeScale = 1;	
							}
							break;
						case Disaster.Gas:
							if(rayHit.transform.tag == "Building" ) {
								//code to apply current disaster to clicked building
								//when possible add to if statement so that you can't
								//reapply burning/leak to a burning or wet building
								rayHit.transform.GetComponent<BuildingHealth>().fireStarted = true;
								Time.timeScale = 1;	
							}
							break;
						case Disaster.Flood:
							if(rayHit.transform.tag == "Road") {
								Vector3 location = rayHit.point;
								location.y += .5f;
								
								Instantiate(floodMaker, location, Quaternion.identity);
								Time.timeScale = 1;						
							}
							break;
						}
						
					}
					
				}
				
				yield return 0;
			}
		}
	}
}
