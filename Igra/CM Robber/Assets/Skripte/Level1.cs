using UnityEngine;
using System.Collections;
using System;
public class Level1 : MonoBehaviour {
	public Transform podloga;
	public Transform[] objekat=new Transform[20];
	// Use this for initialization
	void Start () {


		int velicina = objekat.Length;
		for (int i=0;i<velicina;i++)
		{
			//Vector2 position=new Vector2(UnityEngine.Random.Range (podloga.transform.position.x, podloga.transform.lossyScale.x),UnityEngine.Random.Range(podloga.transform.position.y, podloga.transform.lossyScale.y));
			float x = UnityEngine.Random.Range(-12.10f, 9.86f);
			float y = UnityEngine.Random.Range(-4.83f,6.085f);

			Instantiate(objekat[i],new Vector2(x,y), Quaternion.identity);
		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
