using UnityEngine;
using System.Collections;

public class PersonalAtmo : MonoBehaviour {
	//attaches a building to closest gas sector
	
	public GameObject personalAtmo;
	
	// Use this for initialization
	void Start () {
		personalAtmo = GetClosestSector();

	}
	
	//compares all sectors to see which is closest
	GameObject GetClosestSector() {
		GameObject[] sectors = GameObject.FindGameObjectsWithTag("Gas");
		GameObject currentClosest;
		
		currentClosest = sectors[0];
		
		foreach (GameObject obj in sectors) {
			if (Vector3.Distance(transform.position, obj.transform.position) <= Vector3.Distance(transform.position, currentClosest.transform.position)) {
				currentClosest = obj;
			}
		}
		
		return currentClosest;
	}
	

}
