using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  

public class Recorder : MonoBehaviour {  
	private int _score = 0;  
	public void AddScore(string name)//根据碰撞物名称加减分  
	{  
		if (name == "Plane") _score -= 10;  
		else if (name == "bullet(Clone)") _score += 10;  
	}  
	public int getScore()  
	{  
		return _score;  
	}  
	public void reset()  
	{  
		_score = 0;  
	}  
}  