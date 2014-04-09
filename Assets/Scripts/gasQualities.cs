using UnityEngine;
using System.Collections;

public class gasQualities : MonoBehaviour {

	public float oxygen;
	public float butane;
	
	public bool isOxygenGen;
	public bool isButaneGen;
	
	public float depleteRate = 3;
	
	Color standard = Color.white;
	public Color butaneFull;// = Color.yellow;
	public Color oxyFull;// = Color.blue;
	
	// Use this for initialization
	void Start () {
		oxygen = .5f;
		butane = 0f;
		
		isOxygenGen = false;
		isButaneGen = false;
		
		StartCoroutine( Deplete() );
	}
	
	
	void Update() {
		//color update
		renderer.material.color = Color.Lerp(standard, oxyFull, oxygen);
		renderer.material.color = Color.Lerp(renderer.material.color, butaneFull, butane);
		
		Color color = renderer.material.color;
		//uncomment for transparency
		color.a = ((oxygen + butane) / 4) + .25f;
		
		renderer.material.color = color;
	}
	
	
	IEnumerator Deplete() {
		while(true) {
			if(oxygen > 0.05f) {
				oxygen -= .05f;
			}
			
			if(butane > 0.05f) {
				butane -= .05f;
			}
			
			yield return new WaitForSeconds(depleteRate);
		}
	}
}
