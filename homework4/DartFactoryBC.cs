using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using PlayDarts.Com;  

namespace PlayDarts.Com {  
	public class DartFactory : System.Object {  
		private static DartFactory instance;  
		private List<GameObject> usingDartList = new List<GameObject>();   //正在使用的飞镖list  
		private List<GameObject> unusedDartList = new List<GameObject>();  //没有使用的飞镖list  

		private GameObject dartItem;  

		public static DartFactory getInstance() {  
			if (instance == null)  
				instance = new DartFactory();  
			return instance;  
		}  

		//提供飞镖  
		public GameObject getDart() {  
			if (unusedDartList.Count == 0) {    //没有存储飞镖  
				GameObject newDart = Camera.Instantiate(dartItem);  
				usingDartList.Add(newDart);  
				return newDart;  
			}  
			else {                      //有存储飞镖  
				GameObject oldDart = unusedDartList[0];  
				unusedDartList.RemoveAt(0);  
				oldDart.SetActive(true);  
				usingDartList.Add(oldDart);  
				return oldDart;  
			}  
		}  

		//update()检测飞镖落地，回收。此方法由GameModel的update()方法触发  
		public void detectReuseDarts() {  
			for (int i = 0; i < usingDartList.Count; i++) {  
				if (usingDartList[i].transform.position.y <= -8) {  
					usingDartList[i].GetComponent<Rigidbody>().velocity = Vector3.zero;  //很重要  
					usingDartList[i].SetActive(false);  
					unusedDartList.Add(usingDartList[i]);  
					usingDartList.Remove(usingDartList[i]);  
					i--;  

					MainSceneController.getInstance().subScore();  //打不中，扣分  
				}  
			}  
		}  

		//飞镖被击中，回收  
		public void ReuseWhenDartBeingStruck(GameObject StruckDart) {  
			StruckDart.GetComponent<Rigidbody>().velocity = Vector3.zero;  //很重要  
			StruckDart.SetActive(false);  
			unusedDartList.Add(StruckDart);  
			usingDartList.Remove(StruckDart);  
		}  

		//告知是否正在发射飞镖：若是则不能重复发射  
		public bool isLaunching() {  
			return (usingDartList.Count > 0);  
		}  

		//初始化dartItem  
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