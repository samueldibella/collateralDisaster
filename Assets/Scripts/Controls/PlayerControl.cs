using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	Ray ray;
	RaycastHit hit;
	Rigidbody body;
	//Collider collider;
	
	float rotSpeed = 10;
	Quaternion rotation;
	Quaternion start;
	Vector3 location;
	Vector3 movement;
	Color transit;
	
	public GameObject laser;
	public double damage = 5;
	
	//lateral movement
	public float speed = 1500000000;
	
	//inair variables
	bool jumpAir = false;
	float maxJumpHeight = 6;
	float jumpSpeed = 100;
	
	
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
	
		//normal loop (SHOULD BE CHANGED TO CAMERA OR SEPARATE SCRIPT)
		if(Input.GetMouseButtonDown(0) && Time.timeScale == 1) {
			GetComponent<AudioSource>().Play();
		}
		
		//damage is handled in BuildingHealth script under particle collison
		if(Input.GetMouseButton(0)) {
			//print("hl");
			laser.GetComponent<ParticleSystem>().enableEmission = true;

		} else if(Input.GetMouseButtonUp(0)) {
			laser.GetComponent<ParticleSystem>().enableEmission = false;
			//transform.GetChild(0).GetComponent<ParticleSystem>().Clear();
		}
		
		movement = Vector3.zero;
		
		if(transform.position.y < 0 ) {
			movement.x = 0;
			movement.z = 0;
		} else {
		//WASD controls
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
		
		movement.x *= speed * Time.deltaTime;
		movement.z *= speed * Time.deltaTime;
		movement.y = body.velocity.y;

		body.velocity = movement;
		
		if(transform.position.y < -10) {
			Application.LoadLevel("Fall Lose");
		}
	}
	
	//jump trigger
	void OnCollisionStay(Collision other) {
		if(Input.GetKeyDown(KeyCode.Space) && other.collider.tag == "Road") {
			GetComponent<Rigidbody>().AddForce(transform.up * jumpSpeed);
		}
	}
	
}
