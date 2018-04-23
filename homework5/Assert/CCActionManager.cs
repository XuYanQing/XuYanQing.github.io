using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  

public class CCActionManager : MonoBehaviour {  
	private SceneController _Controller;  
	private GameObject temp;  
	void Start () {  
		_Controller = (SceneController)FindObjectOfType(typeof(SceneController));  
	}  
	public void ShootDisks(GameObject _disk)//发射飞碟  
	{  
		_disk.transform.position = new Vector3(Random.Range(-1f, 1f), 2f, 2f);  
		temp = _disk;  
	}  
	void Update () {  
		if(_Controller.isFlying&&!_Controller._mode)  
		{  
			if (_Controller.getRound() == 1)  
			{  
				temp.transform.position = Vector3.MoveTowards(temp.transform.position, new Vector3(0, 2, 100), 0.1f);  
			}  
			else if (_Controller.getRound() == 2)  
			{  
				temp.transform.position = Vector3.MoveTowards(temp.transform.position, new Vector3(0, 2, 100), 0.2f);  
			}  
		}  
	}  
}  