// --------------------------------------------------------------------
// Timer class: main functionality of TaskTimer app
// You can have up to three independent counters running
// Data can be transferred in csv format to your system email client

// Author: Juha Liias / WestSloth Games
// --------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{

	public float TimeCounter = 0.0f;
	private float pauseTotalTime = 0.0f;

	private DateTime startTime;
	private DateTime currentTime;
	private DateTime pauseGoTime;
	private DateTime pauseExitTime;

	public GameObject timeText;

	public bool isRunning;

	public Animator runAnimation;
	public Animator stopAnimation;

	public InputField timerNameInputField;

	public string timerName = "Timer";
	private String formattedTime;

	// Variables to save hours, minutes and seconds
	public int hours;
	public int minutes;
	public int seconds;

	// resetLevel will store then number of reset button presses
	// One press does not do anything, second press will reset counter
	private int resetLevel = 0;

	// Use this for initialization
	void Awake ()
	{
		// get start time from system
		startTime = DateTime.Now;

		// Variable for storing the time when timer is paused
		// This enables counter running even if application is 
		// running on background
		pauseGoTime = startTime;

		// State information for a counter
		// animation states etc are tuned respectively
		isRunning = false;
		runAnimation.SetBool ("isRunning", false);

		// Find text component for showing counter time
		Text timetext = timeText.GetComponent<Text> ();
		timetext.text = "Time : " + 0; 

		// Show Admod banner
		//AdmobAds.RequestBanner();
	}

	// Update is called once per frame
	void Update ()
	{
		// If counter is running, update current time
		// Remember to take pause time into account
		if (isRunning) {
			currentTime = DateTime.Now;
			TimeCounter = ((Int32)(currentTime - startTime).TotalSeconds) - pauseTotalTime;
			//Debug.Log("pauseTotalTime : " + pauseTotalTime);
		}
		// Calculate hours, minutes and seconds from system time
		// Create formattedTime string from them and show it in text field
		hours = Mathf.FloorToInt (TimeCounter / 3600F);
		minutes = Mathf.FloorToInt ((TimeCounter % 3600) / 60);
		seconds = Mathf.FloorToInt ((TimeCounter % 3600) % 60);
		formattedTime = string.Format ("{0:00}h{1:00}m{2:00}s", hours, minutes, seconds);
		Text timetext = timeText.GetComponent<Text> ();
		timetext.text = "Time : " + formattedTime;
	}

	// If text field is edited, this method catches the changed data
	// and saves it to corresponding variable
	public void TextFieldEdited ()
	{
		timerName = timerNameInputField.text;
		Debug.Log ("Time name is: " + timerName);
	}

	// ------------------------------------------------------------------//
	// Button behaviors

	// playButtonPressed is called when timer play botton is pressed 
	public void playButtonPressed ()
	{
		if (!isRunning) {
			if (resetLevel == 0) {
				pauseExitTime = DateTime.Now;
				pauseTotalTime += (Int32)(pauseExitTime - pauseGoTime).TotalSeconds;
				isRunning = true;
				runAnimation.SetBool ("isRunning", true);
				//Debug.Log("started " + isRunning);
			} else {
				resetLevel = 0;
				stopAnimation.SetBool ("resetState", false);
			}
		} else {
			pauseGoTime = DateTime.Now;
			isRunning = false;
			runAnimation.SetBool ("isRunning", false);
			//Debug.Log("stopped " + isRunning);
		}
	}

	// resetButtonPressed is called when timer reset button is pressed
	// it will reset counter after 2 presses
	public void resetButtonPressed ()
	{
		if (!isRunning) {
			if (resetLevel < 1) {
				resetLevel++;
				stopAnimation.SetBool ("resetState", true);
			} else {
				startTime = DateTime.Now; // get start time
				pauseGoTime = startTime;
				pauseTotalTime = 0.0f;
				TimeCounter = 0f;
				resetLevel = 0;
				stopAnimation.SetBool ("resetState", false);
			}
			Debug.Log ("resetLevel = " + resetLevel);
		}
	}

}
