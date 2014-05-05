using UnityEngine;
using System.Collections;

public class Infrastructure : MonoBehaviour {

	public static int totalStructure = 10000000;
	
	// Update is called once per frame
	void Update () {
		totalStructure = 10000000;
		if(totalStructure == 0) {
			Application.LoadLevel("Evac Lose");
		}
	}
}
