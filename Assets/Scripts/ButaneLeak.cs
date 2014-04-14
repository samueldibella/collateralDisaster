using UnityEngine;
using System.Collections;

public class ButaneLeak : MonoBehaviour {

	//corresponding gas cube
	GameObject atmosphere;

	// Use this for initialization
	void Start () {	
		StartCoroutine( Cleanup() );
	}
	
	void OnTriggerEnter(Collider other) {
		if(other.tag == "Gas") {
			atmosphere = other.gameObject;
		}
	}
	
	IEnumerator Cleanup() {
		atmosphere.GetComponent<gasQualities>().isButaneGen = true;
		
		Destroy(this, 21);
		
		yield return new WaitForSeconds(20);
		
		atmosphere.GetComponent<gasQualities>().isButaneGen = false;
		
		
	}
}
