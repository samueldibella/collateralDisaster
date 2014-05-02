using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
// actual player placement of barricades

	Ray ray;
	RaycastHit hit;
	Rigidbody body;

	Quaternion rotation;
	Vector3 mouse;
	Vector3 movement;
	public float speed = 100000;
	
	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody>();
		
		body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
	}
	
	// Update is called once per frame
	void Update () {
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		Physics.Raycast(ray, out hit);
		
		rotation = Quaternion.LookRotation(transform.position - hit.point, Vector3.forward);
		rotation.x = 0;
		rotation.y = 0;
		//transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 4);
	
		movement = Vector3.zero;
		
		if(Input.GetKey(KeyCode.W)) {
			movement.z += 10;
		}
		
		if(Input.GetKey(KeyCode.S)) {
			movement.z -= 10;
		}
		
		if(Input.GetKey(KeyCode.A)) {
			movement.x -= 10;
		}
		
		if(Input.GetKey(KeyCode.D)) {
			movement.x += 10;
		}
		
		movement *= (speed * Time.deltaTime);
	
		body.velocity = movement;
		
		if(transform.position.y < 0) {
			//Application.LoadLevel("Fall Lose");
		}
	}
}
