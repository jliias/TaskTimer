// --------------------------------------------------------------------
// Class implementing send button functionality
// Author: Juha Liias / WestSloth Games
// --------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class SendButton : MonoBehaviour {

	// Current date
	private string thisDate;
	// email address where data is sent
	private string emailAddress = "emailnot@defined.yet";
	// Email header
	private string emailHeader = "Task Timer data";
	// Variable where email body is build
	private string emailBody;

	// Input field object. Needs to be defined in Unity inspector
	public InputField emailInputField;

	// If send button is pressed, it will call this method
	// (must be defined in Unity inspector)
	public void SendButtonPressed() {
		// Build current date
		thisDate= System.DateTime.Now.ToString("dd.MM.yyyy");
		Debug.Log("Date: " + thisDate);	

		// First line of email body to explain content of each column
		emailBody = "Date,Task,hours,minutes,seconds";

		// Concatenate data from each timer to email body
		foreach (GameObject myTimer in GameObject.FindGameObjectsWithTag("TimerObject")) {
			if (myTimer.name == "Timer") {
				Debug.Log ("Timer found!");
				emailBody = emailBody + "\n" + thisDate + "," + myTimer.GetComponent<Timer> ().timerName + "," + myTimer.GetComponent<Timer> ().hours + "," + myTimer.GetComponent<Timer> ().minutes + "," + myTimer.GetComponent<Timer> ().seconds;
			}
		}
		Debug.Log (emailBody);

		// use SendMailCrossPlatform to send email
		SendMailCrossPlatform.SendEmail(emailAddress, emailHeader, emailBody);
						
	}

	// If email input fieid is edited, this method will catch the data
	// and save it to emailAddress variable
	public void TextFieldEdited ()
	{
		emailAddress = emailInputField.text;
		Debug.Log("email is: " + emailAddress);
	}
}
