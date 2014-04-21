using UnityEngine;
using System.Collections;

public class gasQualities : MonoBehaviour {

	public float oxygen;
	public float butane;
	
	public bool isOxygenGen;
	public bool isButaneGen;
	
	Color standard = Color.white;
	public Color butaneFull;// = Color.yellow;
	public Color oxyFull;// = Color.blue;
	
	// Use this for initialization
	void Start () {
		oxygen = .5f;
		butane = 0f;
		
		isOxygenGen = false;
		isButaneGen = false;
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

}
