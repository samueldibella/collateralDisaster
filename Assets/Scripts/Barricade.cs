using UnityEngine;
using System.Collections;

public class Barricade : MonoBehaviour {
	
	Vector3 startPosition;
	bool isLifted = false;
	public int health = 100;
	
	void Start() {
		startPosition = transform.position;
	}
	
	void Update() {
		if(Input.GetMouseButton(0) && isLifted) {
			transform.position = Input.mousePosition;
		}
		
		if(Input.GetMouseButtonUp(0) && isLifted) {
			transform.GetComponent<Rigidbody>().useGravity = true;
			isLifted = false;
		}
		
		if(health <= 0) {
			GameObject.FindGameObjectWithTag("Factory").GetComponent<Factory>().BarricadeRefill(startPosition);
			Destroy(this.gameObject, 1);
		}
	}
	
	void OnMouseOver() {
		if(Input.GetMouseButtonDown(0)) {
			transform.position = new Vector3(this.transform.position.x, 10, this.transform.position.z);
			transform.GetComponent<Rigidbody>().useGravity = false;
			isLifted = true;
		}
	}
}
