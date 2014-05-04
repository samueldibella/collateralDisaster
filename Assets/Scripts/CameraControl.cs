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
			
			if(player.GetComponent<Rigidbody>().velocity.y < 0) {
				transform.position = Vector3.Lerp(transform.position, active, Time.deltaTime * 4f);
			} else if(player.GetComponent<Rigidbody>().velocity == Vector3.zero) {
				yield return new WaitForSeconds(.05f);
				
				while(player.GetComponent<Rigidbody>().velocity == Vector3.zero) {
					transform.position = Vector3.Lerp(transform.position, start, Time.deltaTime * .25f);
					
					yield return 0;
				}
				
			} else {
				transform.position = Vector3.Lerp(transform.position, active, Time.deltaTime * 4f);
			}
			
			yield return 0;
		}
	}
}
