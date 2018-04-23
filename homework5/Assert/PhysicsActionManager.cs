using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  

public class PhysicsActionManager : MonoBehaviour {  

	private SceneController _Controller;  
	void Start () {  
		_Controller = (SceneController)FindObjectOfType(typeof(SceneController));  
	}  

	public void ShootBullets(GameObject _bullet, Vector3 _dir)//发射子弹  
	{  
		_bullet.transform.position = new Vector3(0, 2, 0);  
		_bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;                       // 子弹刚体速度重置  
		_bullet.GetComponent<Rigidbody>().AddForce(_dir * 500f, ForceMode.Impulse);  
	}  

	public void ShootDisks(GameObject _disk)//发射飞碟  
	{  
		_disk.transform.position = new Vector3(0, 2, 2);  
		float _dx = Random.Range(-20f, 20f);  
		Vector3 _dir = new Vector3(_dx, 30, 20);  
		_disk.transform.up = _dir;  
		if (_Controller.getRound() == 1)  
		{  
			_disk.GetComponent<Rigidbody>().AddForce(_dir*20f, ForceMode.Force);  
		} else if(_Controller.getRound() == 2)  
		{  
			_disk.GetComponent<Rigidbody>().AddForce(_dir*30f, ForceMode.Force);  
		}  
	}  

	void Update () {  

	}  
}  