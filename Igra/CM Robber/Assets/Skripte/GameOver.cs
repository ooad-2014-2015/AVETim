using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}


	void  OnMouseUp(){
		//is this quit

			Application.LoadLevel(1);

	}
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey(KeyCode.Escape)) { Application.Quit();
		}
		else if(Input.GetKey(KeyCode.Space)){Application.LoadLevel(1);}
	}
}
