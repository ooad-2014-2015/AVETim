using UnityEngine;
using System.Collections;

public class Kretanje : MonoBehaviour {
	public float x = (float)0.05;
	public AudioSource mjuza;
	// Use this for initialization
	void Start () {
		mjuza.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.UpArrow)) {
			transform.Translate (Vector3.up *x);
			
		}
		if (Input.GetKey (KeyCode.T)) {
			transform.Translate (Vector3.down *x);
			
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.Translate (Vector3.left *x);
			
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Translate (Vector3.right *x);


		}
		if (Input.GetKey(KeyCode.Escape)) { Application.Quit();
		}
	}

	void OnCollisionEnter(Collision kolizija)
	{
		if (kolizija.gameObject.name=="Ruz 1") {
			Destroy (kolizija.gameObject);
		}
	}
}
