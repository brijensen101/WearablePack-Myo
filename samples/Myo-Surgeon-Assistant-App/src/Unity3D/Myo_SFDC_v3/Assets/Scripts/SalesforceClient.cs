/*
 * @author Ammar Alammar - based on a template provided vy John Casimiro 
 * @created_date 2014-01-30
 * @last_modified_by Ammar Alammar
 * @last_modified_date 2014-06-06
 * @description Client class of the Salesforce REST API wrapper for Unity 3d.
 * @version 1..03
 * 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Boomlagoon.JSON;
using HutongGames.PlayMaker;
using SFDC;


public class SalesforceClient : MonoBehaviour{
	public Salesforce sf;
	public string username;
	public string password;
	public string securityToken; 
	public string patientID;
	public string procedureID;
	public string surgeryType;
	
	// Use this for initialization
	IEnumerator Start () {
		//init object
		sf = gameObject.AddComponent<Salesforce>();
		
		// login
		sf.login(username, password + securityToken);
		
		// wait for Auth Token
		while(sf.token == null)
			yield return new WaitForSeconds(0.1f);

		// query: Retrieve the next closest scheduled surgery / procedure for the current patient
		sf.query("SELECT Id, Surgery_Type__c FROM Case WHERE ContactId = '" + patientID + "' ORDER BY Surgery_Date__c LIMIT 1");

		// wait for query results
		while(sf.response == null)
			yield return new WaitForSeconds(0.1f);

		Debug.Log ("Retrieve Procedure (Case):" + sf.response);

		// Extract the JSON encoded value for the Store the procedure ID (Case) in a field 
		// We are using the free add-in from the Unity3D Asset STore called BoomLagoon.


		// Using BoomLagoon, create a JSON Object from the Salesforce response.
		JSONObject json = JSONObject.Parse(sf.response);

		// Retrieve the records array (only one record is returned) and traverse that record's 
		// attributes to get the case Id and Surgery Type
		JSONArray records = json.GetArray ("records");
		Debug.Log ("records = " + records);
	
		foreach (JSONValue row in records) {
			JSONObject rec = JSONObject.Parse(row.ToString()); 
			Debug.Log ("Procedure (case) Id = " + rec.GetString("Id") + 
		   			   "Surgery Type = " + rec.GetString("Surgery_Type__c"));

			// Assign the case and surgery type
			procedureID = rec.GetString("Id");
			surgeryType = rec.GetString("Surgery_Type__c");
		}

		// Get the number of X-Rays to load from the Playmaker FSM Global Variables.
		int numXrays = 	FsmVariables.GlobalVariables.FindFsmInt ("numImagesToLoad").Value;

		// query: retrieve 5 images (attachments) from the proecdure (case) record
		sf.query("SELECT Id, Name, Body FROM Attachment WHERE ParentId = '" + procedureID + "' LIMIT " + numXrays);

		// wait for query results
		while(sf.response == null)
			yield return new WaitForSeconds(0.1f);

		Debug.Log("Xray Attachments = " + sf.response);

		// Using BoomLagoon, parse the JSON response .
		json = JSONObject.Parse(sf.response);
		
		// Retrieve the records array (up to five records are returned) 
		// Traverse through each record to obtain the link to the attachment body
		records = json.GetArray ("records");
		Debug.Log ("records = " + records);

		int i = 1; // X-Ray image ObjectName starts at xRay1
		foreach (JSONValue row in records) {
			JSONObject rec = JSONObject.Parse(row.ToString()); 
			Debug.Log ("Body Link = " + rec.GetString("Body"));

			// get the attachment and store in the Texture Array
			sf.getAttachmentBody (rec.GetString("Body"),i);

			i++;
		}

	}

	public void completeOperation(){
		// Update a procedure status in Salesforce
		sf.update (this.procedureID, "Case", "{\"Status\" : \"Completed\"}");

	}

	
	/*********************************************************************************
	//****** Here is some code to usefor create, update, and delete operations *******
	//********************************************************************************

	// create
	sf.insert("Account", "{\"Name\" : \"Express Logistics and Transport\"}");
	
	while(sf.response == null)
		yield return new WaitForSeconds(0.1f);
	
	Debug.Log("create results = " + sf.response);


	// Update a record in Salesforce
	sf.update ("< SALESFORCE ID HERE >", "Account", "{\"Name\" : \"Express Logistics and Transport UPDATED\"}");

	while(sf.response == null)
		yield return new WaitForSeconds(0.1f);
	
	Debug.Log("update results = " + sf.response);


	// Delete a record in Salesforce
	sf.delete ("001i000000bv0dH", "Account");

	while(sf.response == null)
		yield return new WaitForSeconds(0.1f);
	
	Debug.Log("delete results = " + sf.response);
	*********************************************************************************/
	
}
