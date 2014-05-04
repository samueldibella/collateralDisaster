using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	TextMesh mesh;

	// Use this for initialization
	void Start () {
		mesh = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		mesh.text = string.Format("{0:0}\n", Time.time);
		//mesh.text += GameStart.currentDisaster;
		
		if(Time.time == 0) {
			mesh.text += "\nClick For Building for Fire Location, and then Run to the cyan Evac Zone";
		}
	}
}
