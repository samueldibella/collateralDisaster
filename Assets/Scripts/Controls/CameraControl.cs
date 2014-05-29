using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	Vector3 start;
	Vector3 active;
	public bool shakeAndBake = false;
	public AudioClip songLoop;
	bool songSwitch = true;
	public GameObject player;

	int startOrthoSize = 80;
	int activeOrthoSize = 5;

	// Use this for initialization
	void Start () {
		
	
		start = transform.position;
		
		StartCoroutine( CameraPosition() );
		StartCoroutine( CameraZoom() );
	}
	
	void Update() {
		if(Time.timeScale == 1 && songSwitch) {
			GetComponent<AudioSource>().clip = songLoop;
			songSwitch = false;
			GetComponent<AudioSource>().Play();
		}
		
	}
	
	IEnumerator CameraZoom() {
		yield return new WaitForSeconds(.1f);
		player = GameObject.FindGameObjectWithTag("Player");
		
		Vector3 shake;
		
		while(true) {
			
			if(player.transform.position.y < 1) {
				transform.position = Vector3.Lerp(transform.position, active, Time.deltaTime * 4f);
			} else if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) {
				yield return new WaitForSeconds(.05f);
				
				while(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) {
					transform.position = Vector3.Lerp(transform.position, active, Time.deltaTime * 4f);
					
					GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, activeOrthoSize, Time.deltaTime * 4f);
					
					//camera shake 
					/*
					if(shakeAndBake) {
						shake = transform.position;
						shake.z += Random.Range(-.5f, .5f);
						shake.x += Random.Range(-.5f, .5f);
						
						transform.position = shake;
						
						StartCoroutine( Still() );
					} */
					
					yield return 0;
				}
				
			} else {
				transform.position = Vector3.Lerp(transform.position, start, Time.deltaTime * .1f);
				GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, startOrthoSize, Time.deltaTime * .1f);
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
	
	IEnumerator Still() {
		yield return new WaitForSeconds(.5f);
		
		shakeAndBake = false;
	}
}
