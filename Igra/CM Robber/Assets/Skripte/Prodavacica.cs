using UnityEngine;
using System.Collections;

public class Prodavacica : MonoBehaviour {

	public Transform target ; //the enemy's target
	int moveSpeed = 1; //move speed
	int rotationSpeed = 0; //speed of turning

	public Transform myTransform; //current transform data of this enemy
/*	function Awake()
	{
		myTransform = transform; //cache transform data for easy access/preformance
	}*/
	
	void Start()
	{
		//target = GameObject.FindWithTag("Lopov").transform; //target the player
		
	}
	
	void Update () {
		var range =5f;
		var range2=10f;
		var stop=0;
		//rotate to look at the player
		float distance = Vector3.Distance(myTransform.position, target.position);
		if (distance<=range2 &&  distance>=range){
			myTransform.rotation = Quaternion.Lerp(myTransform.rotation,
			                                        Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed*Time.deltaTime);
		}
		
		
		else if(distance<=range && distance>stop){
			
			//move towards the player
			myTransform.rotation = Quaternion.Lerp(myTransform.rotation,
			                                   Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed*Time.deltaTime);
			myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
		}
		else if (distance<=stop) {
			myTransform.rotation = Quaternion.Lerp(myTransform.rotation,
			                                        Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed*Time.deltaTime);
		}
		
		
	}
}
