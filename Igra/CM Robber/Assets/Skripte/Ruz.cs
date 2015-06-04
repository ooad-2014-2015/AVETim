using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
public class Ruz : MonoBehaviour {
	public Text tekst;
	public AudioSource muzika;
	// Use this for initialization
	void Start () {
	if (Application.loadedLevel == 2)
			tekst.text = "10";
		else
			tekst.text = "15";
	}
	
	// Update is called once per frame
	void Update () {
		if(tekst.text=="0") { StartCoroutine(pokreni());}

	}
	IEnumerator pokreni(){
		yield return new WaitForSeconds (2);
		Application.LoadLevel("Prijelaz1");
	}
	void OnCollisionEnter2D(Collision2D kolizija)
	{

		if (kolizija.gameObject.name=="Lopov") {
			muzika.Play();
			Destroy (gameObject);
			tekst.text=(int.Parse(tekst.text)-1).ToString();
				}
	}
}
