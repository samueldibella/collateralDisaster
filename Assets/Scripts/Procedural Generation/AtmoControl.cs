using UnityEngine;
using System.Collections;

public class AtmoControl : MonoBehaviour {
	//TODO Once new city generator is merged, tie gas sectors
	//to city blocks, to allow for easier alteration of atmosphere
	//based on code of buildings (disaster scripts)

	//assign in inspector
	public GameObject gas;

	//size of array
	public static int xSize = 30;
	public static int zSize = 15;
	
	//scale of cubes, set to match
	public int scale = 4;
	
	//array of gasSectors
	public static GameObject[,] zones; 
	
	//iteratorRate is for AtmoSpread coroutine
	//diffusion rate is how quickly, per step, gas spreads, check individualDelta function
	public float iteratorSeconds = .1f;
	public float diffusionRate = .12f;

	// Use this for initialization
	void Start () {
		zones = new GameObject[zSize, xSize];
	
		for(int j = 0; j < zSize; j++) {
			for(int i = 0; i < xSize; i++) {
				Vector3 generation = new Vector3(transform.position.x + (i * scale * 2), transform.position.y, transform.position.z + (j * scale * 2));

				zones[j, i] = Instantiate(gas, generation, Quaternion.identity) as GameObject;
				zones[j, i].transform.Rotate(new Vector3(90, 0, 0));
			}
		}

		StartCoroutine( AtmoSpread() );
	}
	
	IEnumerator AtmoSpread() {
		//to store shifting gas
		float[,] oDeltaHolder = new float[zSize, xSize];
		float[,] bDeltaHolder = new float[zSize, xSize];
		
		while(true) {
			
			//stores new value change in temp array to prevent changing values from altering midstep
			for(int j = 0; j < zSize; j++) {
				for(int i = 0; i < xSize; i++) {
					oDeltaHolder[j, i] = individualDelta(j, i, true);
					bDeltaHolder[j, i] = individualDelta(j, i, false);
				}
			}
			
			for(int j = 0; j < zSize; j++) {
				for(int i = 0; i < xSize; i++) {
					//alter gas levels
					zones[j, i].GetComponent<gasQualities>().oxygen += oDeltaHolder[j, i];
					zones[j, i].GetComponent<gasQualities>().butane += bDeltaHolder[j, i];
					
					
					//increases gas in generators
					if(zones[j, i].GetComponent<gasQualities>().isButaneGen == true) {
						zones[j, i].GetComponent<gasQualities>().butane += .45f;
					} 
					
					if (zones[j, i].GetComponent<gasQualities>().isOxygenGen == true) {
						zones[j, i].GetComponent<gasQualities>().oxygen += .45f;
					}
					
					//floor and ceiling, so that levels doesn't go below zero or above 1
					if(zones[j, i].GetComponent<gasQualities>().oxygen < 0) {
						zones[j, i].GetComponent<gasQualities>().oxygen = 0;
					}
					
					if (zones[j, i].GetComponent<gasQualities>().oxygen > 1) {
						zones[j, i].GetComponent<gasQualities>().oxygen = 1;
					}
					
					if(zones[j, i].GetComponent<gasQualities>().butane < 0) {
						zones[j, i].GetComponent<gasQualities>().butane = 0;
					} 
					
					if (zones[j, i].GetComponent<gasQualities>().butane > 1) {
						zones[j, i].GetComponent<gasQualities>().butane = 1;
					}	
				}
			}
			
			yield return new WaitForSeconds(iteratorSeconds);
		}
		
	}
	
	//calculates individual cells change in gas level, either oxygen or butane
	float individualDelta(int y, int x, bool isOxygen) {
		float delta = 0;
		float deltaTemp = 0;
		
		float bAmount = zones[y, x].GetComponent<gasQualities>().butane;
		float oAmount = zones[y, x].GetComponent<gasQualities>().oxygen;
		
		//switch ++ to += for only cardinal direction spread
		//alters delta based on 8 adjacent cell gas levels
		for(int j = -1; j < 2; j ++) {
			for(int i = -1; i < 2; i++) {
				
				//doesn't call on itself or non-existent squares
				if(!(i == 0 && j == 0) && isInGrid(y + j, x + i)) {

					if(isOxygen) {
						deltaTemp = zones[y + j, x + i].GetComponent<gasQualities>().oxygen - oAmount;
						
						//make corners diffuse less
						if((i == -1 && j == -1) || (i == -1 && j == 1) 
						|| (i == 1 && j == -1) && (i == 1 && j == 1)) {
							deltaTemp *= .75f;
						}

					} else {
						deltaTemp = zones[y + j, x + i].GetComponent<gasQualities>().butane - bAmount;
						
						//make corners diffuse less
						if((i == -1 && j == -1) || (i == -1 && j == 1) 
						   || (i == 1 && j == -1) && (i == 1 && j == 1)) {
							deltaTemp *= .75f;
						}
					}
					
					delta += deltaTemp;
					deltaTemp = 0;
				} 
			}
		}
		
		delta *= diffusionRate;
		
		return delta;
	}
	
	//checks if a given coordinate -
	bool isInGrid(int y, int x) {
		if(y > 0 && y < zSize && x > 0 && x < xSize) {
			return true;
		} else {
			return false;
		}
	}
}
