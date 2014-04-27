using UnityEngine;
using System.Collections;

public class BoxSpread : MonoBehaviour {
	private Bounds dimensions;
	private float lastSpread;
	private GameObject newCube = null;
	private int cubesMade=0;
	private GameObject[] childCubes;
	private float startTime;
	public int spreadDelay;
	public int gen;
	public int genMax;
	bool left =false;
	bool right =false;
	bool up =false;
	bool down =false;
	public bool spread=true;
	// Use this for initialization
	void Start () {
		dimensions = gameObject.GetComponent<BoxCollider>().bounds;
		lastSpread = Time.time;
		startTime = Time.time;
	
	}

	// Update is called once per frame
	void Update () {
		StartCoroutine ("Spread");
				



	
	}
	IEnumerator Spread(){
		yield return new WaitForSeconds (Random.Range (0f, 4f));
		while (true) {
							if (Time.time >= startTime+ (genMax/2)) {
								GameObject.Destroy(this.gameObject);
							}
							if (up && down && left && right) {
									spread = false;
							}
		
							if (spread) {
								
				

								switch (Random.Range (0, 4)) {
								case 0:
										{
												if (!right && Physics.Raycast (transform.position, transform.right, dimensions.size.x) == false)
														newCube = Instantiate (gameObject, transform.position + new Vector3 (dimensions.size.x, 0, 0), Quaternion.identity) as GameObject;
												else
														right = true;
												break;
										}
				
								case 1:
										{
												if (!left && Physics.Raycast (transform.position, transform.right * -1, dimensions.size.x) == false)
														newCube = Instantiate (gameObject, transform.position - new Vector3 (dimensions.size.x, 0, 0), Quaternion.identity) as GameObject;
												else
														left = true;
												break;
										}
				
								case 2:
										{
												if (!up && Physics.Raycast (transform.position, transform.forward, dimensions.size.x) == false)
														newCube = Instantiate (gameObject, transform.position + new Vector3 (0, 0, dimensions.size.z), Quaternion.identity) as GameObject;
												else
														up = true;
												break;
										}
				
								case 3:
										{		
												if (!down && Physics.Raycast (transform.position, transform.forward * -1, dimensions.size.x) == false)
														newCube = Instantiate (gameObject, transform.position - new Vector3 (0, 0, dimensions.size.z), Quaternion.identity) as GameObject;
												else
														down = true;
												break;
										}
				
				
								}
			
								if (newCube != null) {
										newCube.GetComponent<BoxSpread> ().gen = gen + 1;
								}
			
						}
						yield return new WaitForSeconds (2f);
				}
		
	}
}
