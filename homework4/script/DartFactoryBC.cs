using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using PlayDarts.Com;  

namespace PlayDarts.Com {  
	public class DartFactory : System.Object {  
		private static DartFactory instance;  
		private List<GameObject> usingDartList = new List<GameObject>();    
		private List<GameObject> unusedDartList = new List<GameObject>(); 

		private GameObject dartItem;  

		public static DartFactory getInstance() {  
			if (instance == null)  
				instance = new DartFactory();  
			return instance;  
		}  

		
		public GameObject getDart() {  
			if (unusedDartList.Count == 0) {    
				GameObject newDart = Camera.Instantiate(dartItem);  
				usingDartList.Add(newDart);  
				return newDart;  
			}  
			else {                      
				GameObject oldDart = unusedDartList[0];  
				unusedDartList.RemoveAt(0);  
				oldDart.SetActive(true);  
				usingDartList.Add(oldDart);  
				return oldDart;  
			}  
		}  

		public void detectReuseDarts() {  
			for (int i = 0; i < usingDartList.Count; i++) {  
				if (usingDartList[i].transform.position.y <= -8) {  
					usingDartList[i].GetComponent<Rigidbody>().velocity = Vector3.zero;  
					usingDartList[i].SetActive(false);  
					unusedDartList.Add(usingDartList[i]);  
					usingDartList.Remove(usingDartList[i]);  
					i--;  

					MainSceneController.getInstance().subScore();  
				}  
			}  
		}  

		
		public void ReuseWhenDartBeingStruck(GameObject StruckDart) {  
			StruckDart.GetComponent<Rigidbody>().velocity = Vector3.zero;  
			StruckDart.SetActive(false);  
			unusedDartList.Add(StruckDart);  
			usingDartList.Remove(StruckDart);  
		}  
  
		public bool isLaunching() {  
			return (usingDartList.Count > 0);  
		}  

		
		public void initItems(GameObject _dartItem) {  
			dartItem = _dartItem;  
		}  
	}  
}  

public class DartFactoryBC : MonoBehaviour {  
	public GameObject dartItem;  

	void Awake() {  
		DartFactory.getInstance().initItems(dartItem);  
	}  
}  
