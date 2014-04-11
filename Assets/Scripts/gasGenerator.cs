using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gasGenerator : MonoBehaviour {
	//this should be called on a gas prefab object
	
	//gasPrefab
	public GameObject gas;
	GameObject current;
	
	//should be half of prefab's width
	public int scale = 1;
	
	float startX;
	float startZ;
	
	//seconds before each cube is added
	int progressionTime = 5;
	
	int[,] gasSpread = new int[51, 51];
	
	
	void Start() {
		//where script will begin generating gas;
		startX = transform.position.x;
		startZ = transform.position.z;
		
		//Vector3 startingLocation = new Vector3(startX, 0, startZ);
	
		for(int j = 0; j < 51; j++) {
			for(int i = 0; i < 51; i++) {
				gasSpread[j, i] = 0;
			}
		}
		
		gasSpread[25, 25] = 1;
		
		StartCoroutine(Gas());
	}
	
	IEnumerator Gas() {	
		while(true) {

			gasIterate();
			
			//really inefficient right now, look here for performance improvement
			gasInstantiation();
			
			yield return new WaitForSeconds(progressionTime);
		}
	}
	
	void gasIterate() {
		int[,] temp = new int[51 ,51];
		
		for(int z = 0; z < 51; z++) {
			for(int i = 0; i < 51; i++) {
				if(gasSpread[z, i] == 1) {
					temp[z, i] = 1;
				} else if (checkNeighbors(i, z)) {
					temp[z, i] = 1;
				} else {
					temp[z, i] = 0;
				}
			}
		}
		
		gasSpread = temp;
	}
	
	/**Tests cardinal neighbors of a space
	*/
	bool checkNeighbors(int x, int z) {
		bool gasNeighbors = false;
		
		for(int j = -1; j < 2; j++) {
			for(int i = -1; j < 2; j++) {
				if(z + j > 0 && z + j < 51 &&
				   x + i > 0 && x + i < 51 &&
				   gasSpread[z + j, x + i] > 0) {
				   	gasNeighbors = true;
				   }
			}
		}
		
		//Debug.Log(gasNeighbors);
		return gasNeighbors;
	}
	
	void gasInstantiation() {
		float arrayX = startX - 25 * scale;
		float arrayZ = startZ - 25 * scale;
		

		
		for(int z = 0; z < 51; z++) {
			for(int i = 0; i < 51; i++) {
				if(gasSpread[z, i] == 1) {
					Instantiate(gas, new Vector3(arrayX, 0, arrayZ), Quaternion.identity);	
				}
				
				//Debug.Log(arrayX);
				//Debug.Log(arrayZ);
				
				arrayX += scale;
				
			}
			
			arrayX = startX - 25 * scale;
			arrayZ += scale;
		}
	}
	
}
