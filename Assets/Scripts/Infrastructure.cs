using UnityEngine;
using System.Collections;

public class Infrastructure : MonoBehaviour {

	public static int totalStructure = 10;
	
	// Update is called once per frame
	void Update () {
		if(totalStructure == 0) {
			Application.LoadLevel("Lose");
		}
	}
}
