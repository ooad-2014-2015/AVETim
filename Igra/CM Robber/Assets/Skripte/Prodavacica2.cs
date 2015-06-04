using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Threading;
public class Prodavacica2 : MonoBehaviour {

	public Transform target;
	public GameObject t;
	public Text teksic;
	public Text dvijesu;
	public AudioSource kraj;

	void Start(){
		 
	}
	// Update is called once per frame
	void Update () 
	{

		float distance = Vector3.Distance(transform.position, target.position);
		if (distance >= 6) {
			teksic.text = "";
			dvijesu.text = "0";
		}
		else 	if (distance < 3 ){//|| dvijesu.text=="1") {
			teksic.text = "Watch out! The seller is really near! ";
			transform.position = Vector3.MoveTowards (transform.position, target.transform.position, .04f);
			dvijesu.text="1";

		}
		else if (distance<6 )//|| dvijesu.text=="1")
		{transform.position = Vector3.MoveTowards (transform.position, target.transform.position, .01f);
			teksic.text = "Watch out! The seller is near! ";
			
			dvijesu.text="1";
		}



			//transform.position = Vector3.MoveTowards (transform.position, target.transform.position, .03f);

		}


	IEnumerator korutina(Collision2D kolizija){
		kolizija.rigidbody.gravityScale = 0.8f;

		kolizija.collider.isTrigger = true;
		kraj.Play ();
		yield return new WaitForSeconds (3);
		Application.LoadLevel ("GameOver");
	}
		//Destroy (kolizija.gameObject);

		
		void OnCollisionEnter2D(Collision2D kolizija)
	{
		
		if (kolizija.gameObject==t) {

			StartCoroutine(korutina(kolizija));
			
		}
	}
}
