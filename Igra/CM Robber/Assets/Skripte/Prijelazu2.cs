using UnityEngine;
using System.Collections;

public class Prijelazu2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	IEnumerator x(){
		yield return new WaitForSeconds (4);
		
		Application.LoadLevel ("Level2Scena");
	}
	
	// Update is called once per frame
	void Update () {
		StartCoroutine(x());
		
		if (Input.GetKey(KeyCode.Escape)) { Application.Quit();
		}
	}
}
