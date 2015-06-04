using UnityEngine;
using System.Collections;

public class MenuGlavni : MonoBehaviour {

public	bool isQuit=false;
	
	public AudioSource kreni;
	
	void  OnMouseUp(){
		//is this quit
		if (isQuit==true) {
			//quit the game
			Application.Quit();
		}
		else {
			//load level
			kreni.Play ();
			Application.LoadLevel("Level1start");
		}
	}
	
	void Update(){
		//quit game if escape key is pressed
		if (Input.GetKey(KeyCode.Escape)) { Application.Quit();
		}
	}

}
