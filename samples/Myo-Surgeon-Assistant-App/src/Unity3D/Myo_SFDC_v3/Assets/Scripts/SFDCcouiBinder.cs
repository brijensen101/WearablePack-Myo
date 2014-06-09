using UnityEngine;
using System.Collections;
using Coherent.UI.Binding;
using HutongGames.PlayMaker;

public class SFDCcouiBinder : MonoBehaviour {
	
	private CoherentUIView m_View;

	void Start () {
		m_View = GetComponent<CoherentUIView>();
			
			// Obtain the Coherenet UI COmponenet and wait for the biding to be ready before working with it.
			m_View.Listener.ReadyForBindings += (frameId, path, isMainFrame) => {
				Debug.Log ("===> COUI is Ready to fire JS events");

				// Fire Playmaker event to display quote
			    PlayMakerFSM targetFSM = GameObject.Find("SFDC_COUI_Binder").GetComponent<PlayMakerFSM>();
				targetFSM.Fsm.Event ("couiBinderReady");
			};
		}

	// public void fireSaveAndQuote(string model, string colour, string wheels){
	public void orderPostOpXray(string procedureID){
		m_View.View.TriggerEvent ("orderPostOpXray", procedureID); // calls a VF Page's javascript wrapper method, 
												      // which in turn performs a saleesfroce remote action
		Debug.Log ("===> JS Event Fired: orderPostOpXray with procedureID " + procedureID); 
	}


	/****************************************************************************************************
	 *   This is a template for creating a callback method to call from Javascript in a Visualforce page
	 *   We have included sample code for updating Playmaker Global variables (as a way to drive state 
	 *   behaviour as well as firing an FSM Event
	 ****************************************************************************************************/

	[Coherent.UI.CoherentMethod("callbackMethod",false)] // This simple annotation binds a javascript method to a unity3D method
	public void exampleCallbackMethodFromVF(string postOpXrayOrdered)
	{
		Debug.Log ("Post Op Xray Order Status = " + postOpXrayOrdered);
		// SAMPLE: retrieve an FSM Global Variable's value.
		// FsmString aStrinGVariable = FsmVariables.GlobalVariables.FindFsmString("variableName1");

		// SAMPLE: set a Playmaker global variable to influence FSM direction
		// FsmVariables.GlobalVariables.FindFsmInt("variableName2").Value = 200;

		// SAMPLE: Fire a Playmaker event to display quote
		// PlayMakerFSM targetFSM = GameObject.Find("targetObjectwithFSM").GetComponent<PlayMakerFSM>();
		// targetFSM.Fsm.Event ("eventName");
	}
}