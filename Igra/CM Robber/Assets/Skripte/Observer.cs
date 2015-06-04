using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Observer : MonoBehaviour {

	public float startTime = 0;

	public Text tekst;
	void Update () {
		//The time taken so far
		 int timeLimit = 50;
		float timeTaken = startTime + Time.timeSinceLevelLoad;

		if (timeTaken >= 50){
			//timeLimit)
			Application.LoadLevel (5); //startTime = 0;

	}
		// Format the time nicely
		tekst.text = "Time left: " + ((int)(timeLimit-timeTaken)).ToString ()+" seconds";//FormatTime (timeTaken);

		
	}
	
	
	//Format time like this
	// 17[minutes]:21[seconds]:05[fraction]
	
	string FormatTime (float time)
		
	{
		string timeText;
		int intTime = (int)time;
		int minutes  = intTime / 60;
		var seconds = intTime % 60;
		//var fraction : int = time * 10;
		// fraction = fraction % 10;
		
		//Build string with format
		// 17[minutes]:21[seconds]:05[fraction]
		
		 timeText = minutes.ToString () ;
		timeText = timeText + seconds.ToString ();

		
		// timeText += fraction.ToString ();
		return timeText;
	}
}
