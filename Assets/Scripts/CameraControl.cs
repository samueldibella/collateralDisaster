using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	Vector3 start;
	Vector3 active;

	public GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	
		start = transform.position;
		
		StartCoroutine( CameraZoom() );
	}
	
	// Update is called once per frame
	void Update () {
		active = new Vector3(player.transform.position.x, 15f, player.transform.position.z);
	}
	
	IEnumerator CameraZoom() {
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
				transform.position = Vector3.Lerp(transform.position, start, Time.deltaTime * .25f);
			}
			
			yield return 0;
		}
	}
}
