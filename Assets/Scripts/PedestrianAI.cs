using UnityEngine;
using System.Collections;

public class PedestrianAI : MonoBehaviour {

	CharacterController controller; 
	int speed = 5;
	
	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
		//direction *= Time.deltaTime;
		
		controller.Move(direction);
	}
}
