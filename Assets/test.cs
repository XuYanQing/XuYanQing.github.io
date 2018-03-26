using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var cubef = GameObject.Find ("/Cube");
		if (null != cubef) {
			Debug.Log ("Find cube");
		}
		var sphere = GameObject.FindWithTag ("cubea");
		if (null != sphere) {
			Debug.Log ("find tag");
		}

		GameObject cubeObject = GameObject.CreatePrimitive (PrimitiveType.Cylinder);
		cubeObject.transform.position = new Vector3 (-2.0f, 0.5f, 4.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
