using UnityEngine;
using System.Collections;

public class StreetGeneratorMaker : MonoBehaviour {
	
	public Transform streetMakerPrefab;

	// Use this for initialization
	void Start () {
		for(int i = 0; i < 5; i++){ 
			Instantiate (streetMakerPrefab, new Vector3(transform.position.x, 1F, transform.position.z), Quaternion.identity);
		}
		Destroy(gameObject); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
