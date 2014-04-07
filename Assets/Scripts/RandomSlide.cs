using UnityEngine;
using System.Collections;

public class RandomSlide : MonoBehaviour {
	private float lastTurned;
	public float turnDelay;
	public float speed;
	public int genMoving;

	// Use this for initialization
	void Start () {
		lastTurned = Time.time;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.GetComponent<BoxSpread> ().gen <= genMoving) {
			transform.position += transform.forward * speed;
			if(Time.time>=lastTurned+turnDelay){
				lastTurned=Time.time;
				switch (Random.Range(0,2)){
				case 0:
					transform.Rotate(new Vector3(0,90,0));
					break;
				
				case 1:
					transform.Rotate(new Vector3(0,-90,0));
					break;
				

				}

			}
		}
	
	}
}
