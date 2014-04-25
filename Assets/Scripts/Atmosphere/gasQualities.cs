using UnityEngine;
using System.Collections;

public class gasQualities : MonoBehaviour {

	public float oxygen;
	public float butane;
	
	public bool isOxygenGen;
	
	Color standard = Color.white;
	public Color oxyFull; // = Color.blue;
	
	// Use this for initialization
	void Start () {
		oxygen = .5f;
		butane = 0f;
		
		isOxygenGen = false;
	}
	
	
	public void newColor() {
		renderer.material.color = Color.Lerp(standard, oxyFull, oxygen);
		
		Color color = renderer.material.color;
		//uncomment for transparency
		color.a = ((oxygen ) / 4) + .25f;
		
		renderer.material.color = color;
	}

}
