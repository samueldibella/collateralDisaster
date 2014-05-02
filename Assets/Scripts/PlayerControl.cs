using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
// actual player placement of barricades

	Ray ray;
	RaycastHit hit;
	Rigidbody body;

	Vector3 movement;
	public float speed = 10;
	
	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		movement = Vector3.zero;
		
		if(Input.GetKey(KeyCode.W)) {
			movement.z += 1;
		}
		
		if(Input.GetKey(KeyCode.A)) {
			movement.z -= 1;
		}
		
		if(Input.GetKey(KeyCode.S)) {
			movement.x -= 1;
		}
		
		if(Input.GetKey(KeyCode.D)) {
			movement.x += 1;
		}
		
		movement *= speed * Time.deltaTime;
	
		
	
		body.AddForce( movement );
	}
}
