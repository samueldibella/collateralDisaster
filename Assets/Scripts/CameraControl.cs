using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	Vector3 start;
	Vector3 active;

	public GameObject player;

	// Use this for initialization
	void Start () {
		start = transform.position;
		
		StartCoroutine( ZoomIn() );
		
		StartCoroutine( ZoomOut() );
	}
	
	// Update is called once per frame
	void Update () {
		active = new Vector3(player.transform.position.x, 15f, player.transform.position.z);
	}
	
	IEnumerator ZoomIn() {
		while(true) {
			while(player.GetComponent<Rigidbody>().velocity != Vector3.zero) {			
				transform.position = Vector3.Lerp(transform.position, active, Time.deltaTime);
				
				yield return 0;
			}
			
			yield return 0;
		}
	}
	
	IEnumerator ZoomOut() {
		while(true) {
			while(player.GetComponent<Rigidbody>().velocity == Vector3.zero) {
				transform.position = Vector3.Lerp(transform.position, start, Time.deltaTime);
			}
			
			yield return 0;
		}
	}
}
