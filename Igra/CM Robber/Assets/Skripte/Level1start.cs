using UnityEngine;
using System.Collections;

public class Level1start : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	IEnumerator x(){
		yield return new WaitForSeconds (4);
		
		Application.LoadLevel ("Level1Scena");
	}

	// Update is called once per frame
	void Update () {
			StartCoroutine(x());
		
		if (Input.GetKey(KeyCode.Escape)) { Application.Quit();
		}
	}
}
