using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	Ray ray;
	RaycastHit hit;
	Rigidbody body;
	//Collider collider;
	
	public float damage = .0000001f;
	Quaternion rotation;
	Vector3 location;
	Vector3 movement;
	public float speed = 110000;
	
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
		
		/*
		rotation = Quaternion.LookRotation(transform.position - location, Vector3.up);
		rotation.x = 0;
		rotation.y = 0;
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 4); */
	
		if(Input.GetMouseButton(0)) {
			transform.GetChild(0).GetComponent<ParticleSystem>().enableEmission = true;

		} else if(Input.GetMouseButtonUp(0)) {
			transform.GetChild(0).GetComponent<ParticleSystem>().enableEmission = false;
		}
		
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
		
		movement.y -= 1;
		
		movement *= (speed * Time.deltaTime);
	
		body.velocity = movement;
		
		if(transform.position.y < 0) {
			Application.LoadLevel("Fall Lose");
		}
	}
}
