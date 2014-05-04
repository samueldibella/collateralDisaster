using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	Vector3 start;
	Vector3 active;

	public GameObject player;

	// Use this for initialization
	void Start () {
		
	
		start = transform.position;
		
		StartCoroutine( CameraPosition() );
		StartCoroutine( CameraZoom() );
		
	}
	
	IEnumerator CameraZoom() {
		yield return new WaitForSeconds(.1f);
		player = GameObject.FindGameObjectWithTag("Player");
		
		while(true) {
			
			if(player.transform.position.y < 1) {
				transform.position = Vector3.Lerp(transform.position, active, Time.deltaTime * 4f);
			} else if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) {
				yield return new WaitForSeconds(.05f);
				
				while(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) {
					transform.position = Vector3.Lerp(transform.position, active, Time.deltaTime * 4f);
					
					yield return 0;
				}
				
			} else {
				transform.position = Vector3.Lerp(transform.position, start, Time.deltaTime * .1f);
			}
			
			yield return 0;
		}
	}
	
	IEnumerator CameraPosition() {
		yield return new WaitForSeconds(.2f);
		
		while(true) {
			active = new Vector3(player.transform.position.x, 15f, player.transform.position.z);
			
			yield return 0;
		}
	}
}
