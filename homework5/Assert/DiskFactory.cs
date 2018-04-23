using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  

public class DiskFactory : MonoBehaviour {  
	public List<GameObject> Using; //储存正在使用的  
	public List<GameObject> Used; //储存空闲的    
	private SceneController _Controller;  
	public GameObject DiskPrefab;  
	void Start () {  
		Using = new List<GameObject>();  
		Used = new List<GameObject>();  
		_Controller = (SceneController)FindObjectOfType(typeof(SceneController));  
	}  
	public GameObject getDisk(bool isPhy,int _Round)  
	{  
		GameObject t;  
		if (Used.Count == 0)  
		{  
			t = GameObject.Instantiate(DiskPrefab) as GameObject;  
		}  
		else  
		{  
			t = Used[0];  
			Used.Remove(t);  
		}  
		t.GetComponent<MeshCollider>().isTrigger = true;  
		t.SetActive(true);  
		if (isPhy)//物理学模式加刚体组件  
		{  
			if (t.GetComponent<Rigidbody>() == null)  
				t.AddComponent<Rigidbody>();  
		}  
		else if (!isPhy)//运动学模式去除刚体组件  
		{  
			if (t.GetComponent<Rigidbody>())  
				Destroy(t.GetComponent<Rigidbody>());  
		}  
		Using.Add(t);  
		if(_Round==1)//第一关的飞碟形式  
		{  
			t.transform.localScale *= 2;  
			t.GetComponent<Renderer>().material.color = Color.green;  
		}//第二关为初始大小以及红色  
		return t;  
	}  
	private void freeDisk(int round)//把场景中inactive的飞碟回收  
	{  
		for (int i = 0; i < Using.Count; i++)  
		{  
			GameObject t = Using[i];  
			if (!t.activeInHierarchy)  
			{  
				Using.RemoveAt(i);  
				Used.Add(t);  
				Destroy(t.GetComponent<Rigidbody>());  
				if (round==1)  
					t.transform.localScale /= 2;  
				t.GetComponent<Renderer>().material.color = Color.red;  
			}  
		}  
	}  
	void Update () {  
		freeDisk(_Controller.getRound());  
	}  
}  