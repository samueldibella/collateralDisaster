using UnityEngine;
using System.Collections;

public class PersonalAtmo : MonoBehaviour {
	//attaches a building to closest gas sector
	
	GameObject personalAtmo;
	
	public bool oxyGenerator;
	
	// Use this for initialization
	void Start () {
		personalAtmo = GetClosestSector();
		oxyGenerator = false;
		
		StartCoroutine( Generator() );
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
	
	IEnumerator Generator() {
		while(true) {
			if(oxyGenerator == true) {
				personalAtmo.GetComponent<gasQualities>().isOxygenGen = true;
			}
			
			yield return new WaitForSeconds(1);
		}
	}
}
