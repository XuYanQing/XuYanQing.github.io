﻿using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  

public class Director : System.Object  
{  
	private static Director _instance;  

	public SceneController _Controller { get; set; }  

	public static Director getinstance()  
	{  
		if (_instance == null)  
		{  
			_instance = new Director();  
		}  
		return _instance;  
	}  

	public int getFPS()  
	{  
		return Application.targetFrameRate;  
	}  

	public void setFPS(int fps)  
	{  
		Application.targetFrameRate = fps;  
	}  
}  