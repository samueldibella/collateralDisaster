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
		mesh.text = "";
		
		if(Time.time == 0) {
			mesh.text += "Click Building for Fire Location, \nRun to the cyan Evac Zone";
			mesh.text += "\nLeft Click to Arc Weld\n";
		}
		
		//mesh.text += string.Format("{0:0}\n", Time.time);
		
		
	}
}
