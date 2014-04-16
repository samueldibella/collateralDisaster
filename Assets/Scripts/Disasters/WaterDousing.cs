using UnityEngine;
using System.Collections;

public class WaterDousing : MonoBehaviour {
	//call on water prefab

	public int douseRate = 2;
	
	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag == "Building" && collision.gameObject.GetComponent<BuildingHealth>().onFire) {
			collision.gameObject.GetComponent<BuildingHealth>().fireIntensity -= douseRate;
			Destroy(this.gameObject, 2);
		}
	}

	
}
