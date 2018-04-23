using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using UnityEngine.UI;  
public class Userface : MonoBehaviour {  
	private SceneController _Controller;  
	public Camera _camera;  
	public Text Score;//显示分数  
	public Text Round;//显示关卡  
	public Text Mode;//显示模式  
	void Start () {  
		_Controller = (SceneController)FindObjectOfType(typeof(SceneController));  
	}  

	// Update is called once per frame  
	void Update () {  
		if(Input.GetKeyDown("1"))//按下1切换到运动学模式  
		{  
			if(!_Controller.isFlying)  
			{  
				Debug.Log("NonPhy mode");  
				_Controller._mode = false;  
			}  
		}  
		if(Input.GetKeyDown("2"))//按下2切换到物理刚体模式  
		{  
			if (!_Controller.isFlying)  
			{  
				Debug.Log("Phy mode");  
				_Controller._mode = true;  
			}  
		}  
		if(Input.GetKeyDown("space"))//按下空格发射飞碟  
		{  
			_Controller.ShootDisk();  
		}  
		if (Input.GetMouseButtonDown(0)&&!_Controller.isShooting)//按下左键发射子弹  
		{  
			Ray mouseRay = _camera.ScreenPointToRay(Input.mousePosition);  
			_Controller.ShootBullet(mouseRay.direction);  
		}  
		Score.text = "Score:" + _Controller.getRecorder().getScore();  
		Round.text = "Round:" + _Controller.getRound();  
		if (_Controller._mode == false) Mode.text = "Mode:CCAction";  
		else Mode.text = "Mode:PhysicsAction";  
	}  
}  
