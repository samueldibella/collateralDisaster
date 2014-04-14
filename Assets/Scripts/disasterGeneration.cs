using UnityEngine;
using System.Collections;

public class disasterGeneration : MonoBehaviour {

	public enum Disaster {Flood, Fire, Gas}

	//next disaster to appear
	public Disaster currentDisaster;
	
	//prefabs for disaster placement
	public GameObject floodMaker;

	public float disasterRate = 20f;
	
	bool gameStart = false;
	float gameTime = 0;
	int randomHolder;

	RaycastHit rayHit;
	Ray ray;
	
	// Use this for initialization
	void Start () {
		Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
		//game starts paused
		if( Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 0 ) {
			Time.timeScale = 1;
			gameStart = true;
		}
		
		if(Time.timeScale == 1) {
			gameTime += Time.deltaTime;
			
			if( gameTime % disasterRate < .01 || gameTime % disasterRate > disasterRate - .01f ) {
				//pause game
				Time.timeScale = 0;
				
				//generate disaster
				randomHolder = Random.Range(0, 3);
				
				randomHolder = 1;
				
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
			}	
			
		//when game has paused, waiting for player to place a disaster
		} else if ( Time.timeScale == 0 && gameStart == true ) {
			if( Input.GetKeyDown(KeyCode.Mouse0)) {

				ray = Camera.main.ScreenPointToRay(Input.mousePosition);

				if(Physics.Raycast(ray, out rayHit)) {
					switch(currentDisaster) {
					case Disaster.Fire:
					case Disaster.Gas:
						if(rayHit.transform.tag == "Building" ) {
							//code to apply current disaster to clicked building
							//when possible add to if statement so that you can't
							//reapply burning/leak to a burning or wet building
							
						}
						break;
					case Disaster.Flood:
						if(rayHit.transform.tag == "Road") {
							Instantiate(floodMaker, rayHit.point, Quaternion.identity);
							Time.timeScale = 1;						
						}
						break;
					}

				}
				
			}
		
		}
		
	}
}
