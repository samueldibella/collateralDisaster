using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	Ray ray;
	RaycastHit hit;
	Rigidbody body;
	//Collider collider;
	
	float rotSpeed = 10;
	public float damage = .0000001f;
	Quaternion rotation;
	Quaternion start;
	Vector3 location;
	Vector3 movement;
	Color transit;
	public float speed = 1500000000;
	
	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody>();
		//collider = GerComponent<Collider>();
		
		transform.GetChild(0).GetComponent<ParticleSystem>().enableEmission = false;
		
		body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
	}
	
	// Update is called once per frame
	void Update () {

 		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		Physics.Raycast(ray, out hit);
		
		location = new Vector3(hit.point.x, 2, hit.point.z);
		
		start = transform.rotation;
		rotation = Quaternion.LookRotation(transform.position - location, Vector3.up);

		transform.rotation = Quaternion.Lerp(start, rotation, Time.deltaTime * rotSpeed);
		transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
	
		if(Input.GetMouseButton(0)) {
			transform.GetChild(0).GetComponent<ParticleSystem>().enableEmission = true;

		} else if(Input.GetMouseButtonUp(0)) {
			transform.GetChild(0).GetComponent<ParticleSystem>().enableEmission = false;
		}
		
		movement = Vector3.zero;
		
		if(transform.position.y < 0 ) {
			movement.x = 0;
			movement.z = 0;
		} else {
			if(Input.GetKey(KeyCode.W)) {
				movement.z += 40;
			} 
			
			if(Input.GetKey(KeyCode.S)) {
				movement.z -= 40;
			}
			
			if(Input.GetKey(KeyCode.A)) {
				movement.x -= 40;
			}
			
			if(Input.GetKey(KeyCode.D)) {
				movement.x += 40;
			}
		} 
		
		movement.y -= 10;
		
		movement *= (speed * Time.deltaTime);

		body.velocity = movement;
		
		if(transform.position.y < -10) {
			Application.LoadLevel("Fall Lose");
		}
	}
}
