using UnityEngine;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {
	//score
	string score;
	//building
	public GameObject building;
	
	// Update is called once per frame
	void Update () {
		score = "";
		score += Infrastructure.totalStructure;
		GetComponent<TextMesh>().text = score;
	}
}
