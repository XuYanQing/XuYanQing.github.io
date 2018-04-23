using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  

public class DiskModule : MonoBehaviour {  
	private string _sth = "";  
	private float _time = 8f;  
	private SceneController _Controller;  
	void Start () {  
		_Controller = (SceneController)FindObjectOfType(typeof(SceneController));  
	}  

	void OnTriggerEnter(Collider other)//触发器事件  
	{  
		if(_sth=="")  
		{  
			_sth = other.gameObject.name;  
			Debug.Log(_sth);  
			if (_sth == "bullet(Clone)")  
			{  
				this._time = 0;//直接回收  
				_Controller._explosion.GetComponent<Renderer>().material.color = this.GetComponent<Renderer>().material.color;  
				_Controller._explosion.transform.position = this.transform.position;  
				_Controller._explosion.Play();//播放爆炸粒子  
			}  

			_Controller.getRecorder().AddScore(_sth);  
			_Controller.isShooting = false;  
		}  
	}  

	void Update () {  
		if (_Controller.isFlying)  
		{  
			if (_time > 0) _time -= Time.deltaTime;  
			else if (_time <= 0)//回收飞碟  
			{  
				GetComponent<MeshCollider>().isTrigger = false;  
				this.gameObject.SetActive(false);  
				_time = 8f;  
				_sth = "";  
				_Controller.isFlying = false;  
			}  
		}  

	}  
}  