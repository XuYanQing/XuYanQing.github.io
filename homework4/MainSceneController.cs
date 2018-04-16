using System;  
using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  

namespace PlayDarts.Com {  

	public interface IUserAction {  
		void launchDarts();  
		void strikeTheDart(Vector3 mousePos);  
	}  

	public interface IGameStatusOp {  
		int getRoundNum();  
		void addScore();  
		void subScore();  
	}  

	public class MainSceneController : System.Object, IUserAction, IGameStatusOp {  
		private static MainSceneController instance;  
		private GameModel myGameModel;  
		private GameStatus myGameStatus;  

		public static MainSceneController getInstance() {  
			if (instance == null)  
				instance = new MainSceneController();  
			return instance;  
		}  

		internal void setGameModel(GameModel _myGameModel) {  
			if (myGameModel == null) {  
				myGameModel = _myGameModel;  
			}  
		}  

		internal void setGameStatus(GameStatus _myGameStatus) {  
			if (myGameStatus == null) {  
				myGameStatus = _myGameStatus;  
			}  
		}  

		/** 
        * 实现IUserAction接口 
        */  
		public void launchDarts() {  
			myGameModel.launchDarts();  
		}  

		public void strikeTheDart(Vector3 mousePos) {  
			myGameModel.strikeTheDart(mousePos);  
		}  


		/** 
        * 实现IGameStatusOp接口 
        */  
		public int getRoundNum() {  
			return myGameStatus.getRoundNum();  
		}  

		public void addScore() {  
			myGameStatus.addScore();  
		}  

		public void subScore() {  
			myGameStatus.subScore();  
		}  
	}  
}  