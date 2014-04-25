using UnityEngine;
using System.Collections;

public class Barricade : MonoBehaviour {
	
	Vector3 startPosition;
	bool isLifted = false;
	public int health = 100;
	
	Ray ray;
	RaycastHit hit;
	
	GameObject[] sectors;
	
	void Start() {
		startPosition = transform.position;
	}
	
	void Update() {
		//when player is holding barricade
		if(Input.GetMouseButton(0) && isLifted) {
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			if(Physics.Raycast(ray, out hit)) {
				if(hit.transform.tag == "Building" || hit.transform.tag == "Road") {
					transform.position = new Vector3(hit.transform.position.x, 10f, hit.transform.position.z);
				} else {
					transform.position = new Vector3(hit.point.x, 10f, hit.point.z);
				}
				
			}
			
		}
		
		//when player drops barricade
		if(Input.GetMouseButtonUp(0) && isLifted) {
			transform.GetComponent<Rigidbody>().useGravity = true;
			isLifted = false;
			
			//switch from ignore raycast to default layer
			this.gameObject.layer = 0;
		}
		
		if(health <= 0) {
			GameObject.FindGameObjectWithTag("Factory").GetComponent<Factory>().BarricadeRefill(startPosition);
			Destroy(this.gameObject);
		}
	}
	
	void OnMouseOver() {
		if(Input.GetMouseButtonDown(0) && Time.timeScale == 1) {
			transform.GetComponent<Rigidbody>().useGravity = false;
			isLifted = true;
			
			//set to ignore raycast layer on barricade
			this.gameObject.layer = 2;
		}
	}
}
