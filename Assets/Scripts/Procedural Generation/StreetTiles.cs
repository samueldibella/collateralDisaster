using UnityEngine;
using System.Collections;

public class StreetTiles : MonoBehaviour {

	public GameObject streetTilePrefab;

	int xSize = 60;
	int zSize = 30;
	int scale = 4;
	
	void Start() {
		for(int x = 0; x < xSize; x++) {
			for(int z = 0; z < zSize; z++) {
				Instantiate(streetTilePrefab, new Vector3(transform.position.x + (scale * x), 0, transform.position.z + (scale * z)), Quaternion.identity);
			}
		}
	}
}
