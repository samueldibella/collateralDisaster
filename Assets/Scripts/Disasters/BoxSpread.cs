using UnityEngine;
using System.Collections;

public class BoxSpread : MonoBehaviour {
	private Bounds dimensions;
	private float lastSpread;
	private GameObject newCube = null;
	public float spreadDelay;
	public int gen;
	public int genMax;

	// Use this for initialization
	void Start () {
		dimensions = gameObject.GetComponent<BoxCollider> ().bounds;
		lastSpread = Time.time;
	
	}

	// Update is called once per frame
	void Update () {
		if (Time.time > lastSpread + spreadDelay && gen<genMax) {

			lastSpread = Time.time;
			switch(Random.Range(0,4)){
				case 0:{
				if(Physics.OverlapSphere(transform.position + new Vector3(dimensions.size.x,0,0),0.01f).Length == 0)
						newCube = Instantiate(gameObject,transform.position + new Vector3(dimensions.size.x,0,0), Quaternion.identity) as GameObject;
					break;
				}
				case 1:{
				if(Physics.OverlapSphere(transform.position - new Vector3(dimensions.size.x,0,0),0.01f).Length == 0)
						newCube = Instantiate(gameObject,transform.position - new Vector3(dimensions.size.x,0,0), Quaternion.identity) as GameObject;
					break;
				}
				case 2:{
				if(Physics.OverlapSphere(transform.position + new Vector3(0,0,dimensions.size.z),0.01f).Length == 0)
						newCube = Instantiate(gameObject,transform.position + new Vector3(0,0,dimensions.size.z), Quaternion.identity) as GameObject;
					break;
				}
				case 3:{
				if(Physics.OverlapSphere(transform.position - new Vector3(0,0,dimensions.size.z),0.01f).Length == 0)
						newCube = Instantiate(gameObject,transform.position - new Vector3(0,0,dimensions.size.z), Quaternion.identity) as GameObject;
					break;
				}

			
			}
			if (newCube!=null){
				newCube.GetComponent<BoxSpread>().gen = gen+1;
			}
			
		}

	
	}
}
