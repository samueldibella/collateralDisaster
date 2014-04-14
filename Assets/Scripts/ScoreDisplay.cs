using UnityEngine;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {
	//score
	string score;
	//building
	public GameObject building;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		score = "";
		score += BuildingHealth.totalScore;
		GetComponent<TextMesh>().text = score;
	}
}
