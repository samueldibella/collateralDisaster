using UnityEngine;
using System.Collections;

public class Factory : MonoBehaviour {

	public bool isFunctioning;
	public GameObject barricadePrefab;
	public int refillRate;
	
	Vector3 storage1;
	Vector3 storage2;
	Vector3 storage3;
	Vector3 storage4;

	// Use this for initialization
	void Start () {
		isFunctioning = true;
		refillRate = 5;
		
		storage1 = new Vector3(87, 4, 210);
		storage2 = new Vector3(82, 4, 210);
		storage3 = new Vector3(82, 4, 215);
		storage4 = new Vector3(87, 4, 215);
				
		Instantiate(barricadePrefab, storage1, Quaternion.identity);
		Instantiate(barricadePrefab, storage2, Quaternion.identity);
		Instantiate(barricadePrefab, storage3, Quaternion.identity);
		Instantiate(barricadePrefab, storage4, Quaternion.identity);		
	}

	public IEnumerator BarricadeRefill(Vector3 start) {
		print("call");
		yield return new WaitForSeconds(refillRate);
		
		Instantiate(barricadePrefab, start, Quaternion.identity);
	}	
}
