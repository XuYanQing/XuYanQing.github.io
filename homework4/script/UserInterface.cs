using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using PlayDarts.Com;  

public class UserInterface : MonoBehaviour {  
	private IUserAction action;  

	void Start () {  
		action = MainSceneController.getInstance() as IUserAction;  

	}  

	void Update () {  
		detectSpaceKeyAndLaunchDarts();  
		detectMouseDownAndStrikeTheDart();  
	}  

	void detectSpaceKeyAndLaunchDarts() {  
		if (Input.GetKeyDown(KeyCode.Space)) {  
			action.launchDarts();  
		}  
	}  

	void detectMouseDownAndStrikeTheDart() {  
		if (Input.GetMouseButtonDown(0)) {  
			Vector3 mouseWorldPosition = Input.mousePosition;  
			action.strikeTheDart(mouseWorldPosition);  
		}  
	}  
}  