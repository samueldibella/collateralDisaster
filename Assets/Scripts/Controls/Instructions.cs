using UnityEngine;
using System.Collections;

public class Instructions : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine( Start1() );
	}
	
	IEnumerator Start1() {
		//AsyncOperation async = Application.LoadLevelAsync("FullBuild");
		
		while(true) {
			if(Input.GetKeyDown(KeyCode.Space)) {
				Application.LoadLevel("FullBuild");
			}
		
			yield return 0;
		}
	}
}
