using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  

public class SceneController : MonoBehaviour {  
	private CCActionManager _CCAM;  
	private PhysicsActionManager _PhyAM;  
	private DiskFactory _Factory;  
	private Recorder _Recorder;  
	private int _Round = 1;  
	public GameObject _bullet;//子弹  
	public ParticleSystem _explosion;//爆炸粒子  
	public bool _mode = false;//标记模式  
	public bool isShooting = false;//判断子弹是否正在飞  
	public bool isFlying = false;//判断飞碟是否正在飞  
	private float _time = 1f;  
	void Start () {  
		_bullet = GameObject.Instantiate(_bullet) as GameObject;  
		_explosion = GameObject.Instantiate(_explosion) as ParticleSystem;  
		Director _director = Director.getinstance();  
		_director._Controller = this;  
		_CCAM = (CCActionManager)FindObjectOfType(typeof(CCActionManager));  
		_PhyAM = (PhysicsActionManager)FindObjectOfType(typeof(PhysicsActionManager));  
		_Recorder = (Recorder)FindObjectOfType(typeof(Recorder));  
		_Factory = (DiskFactory)FindObjectOfType(typeof(DiskFactory));  
		_director.setFPS(60);  
		_director._Controller = this;  
	}  
	public DiskFactory getFactory()  
	{  
		return _Factory;  
	}  
	public int getRound()  
	{  
		return _Round;  
	}  
	public Recorder getRecorder()  
	{  
		return _Recorder;  
	}  

	public CCActionManager getCCActionManager()  
	{  
		return _CCAM;  
	}  
	public PhysicsActionManager getPhysicsActionManager()  
	{  
		return _PhyAM;  
	}  
	public void ShootDisk()  
	{  
		if(!isFlying)  
		{  
			GameObject _disk = _Factory.getDisk(_mode, _Round);//根据不同关卡以及不同模式生产不同的飞碟  
			if (!_mode) _CCAM.ShootDisks(_disk);//根据不同模式调用不同动作管理器的动作函数  
			else _PhyAM.ShootDisks(_disk);  
			isFlying = true;  
		}  
	}  
	public void ShootBullet(Vector3 _dir)//调用动作管理器的射击子弹函数  
	{  
		isShooting = true;  
		_PhyAM.ShootBullets(_bullet, _dir);  
	}  
	private void AddRound()//判断是否通关  
	{  
		if (_Recorder.getScore() >= 50&&_Round == 1)  
		{  
			_Round++;  
		}  
		else if (_Recorder.getScore() > 100&& _Round == 2)  
		{  
			_Round = 1;  
			_Recorder.reset();  
		}  
	}  
	void Update () {  
		AddRound();  
		if(isShooting)  
		{  
			if(_time>0)  
			{  
				_time -= Time.deltaTime;  
				if(_time<=0)  
				{  
					isShooting = false;  
					_time = 1f;  
				}  
			}  
		}  
	}  
}  