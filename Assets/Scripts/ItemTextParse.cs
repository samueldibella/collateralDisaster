using UnityEngine;
using System.Collections;

public class ItemTextParse : MonoBehaviour {

	public TextAsset textFile; //assign in inspector
	public Transform itemPrefab;

	// Use this for initialization
	void Start () {
		StartCoroutine( ParseAndGenerate() );
	}
	
	IEnumerator ParseAndGenerate() {
		Debug.Log(textFile.text);
		
		//clean windows line breaks
		string cleanedTextData = textFile.text.Replace( "\r", "" );
		string[] lines = cleanedTextData.Split( "\n" [0] );
		
		foreach (string line in lines) {
			var newItem = Instantiate(itemPrefab, Random.insideUnitSphere * 10f, Quaternion.identity) as Transform;
			
			string[] data = line.Split( "," [0] );
			newItem.name = data[0];
			newItem.transform.localScale = new Vector3(1, float.Parse( data[1] ), 1);
		}
		
		
		
		yield return 0;
	}
}
