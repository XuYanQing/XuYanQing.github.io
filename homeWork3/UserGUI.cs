using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserAction {
	void click();
}

public class UserGUI : MonoBehaviour {

	private IUserAction action;
	float width, height;

	void Start() {
		action = SSDirector.getInstance().currentSceneController as IUserAction;
	}

	float castw(float scale) {
		return (Screen.width - width) / scale;
	}

	float casth(float scale) {
		return (Screen.height - height) / scale;
	}

	void OnGUI() {
		width = Screen.width / 12;
		height = Screen.height / 12;

		GUI.Label(new Rect(castw(2f)+20, casth(6f) - 20, 50, 50), SSDirector.getInstance().leaveSeconds.ToString());// 倒计时秒数 //  
		if (SSDirector.getInstance().state != State.WIN && SSDirector.getInstance().state != State.LOSE
			&& GUI.Button(new Rect(10, 10, 80, 30), SSDirector.getInstance().countDownTitle)) {
			if (SSDirector.getInstance().countDownTitle == "Start") {

				SSDirector.getInstance().currentSceneController.Resume();
				SSDirector.getInstance().countDownTitle = "Pause";
				SSDirector.getInstance().onCountDown = true;
				StartCoroutine(SSDirector.getInstance().DoCountDown());
			} else {
				SSDirector.getInstance().currentSceneController.Pause();
				SSDirector.getInstance().countDownTitle = "Start";
				SSDirector.getInstance().onCountDown = false;
				StopAllCoroutines();
			}
		}

		if (SSDirector.getInstance().state == State.WIN) {
			StopAllCoroutines();
			if (GUI.Button(new Rect(castw(2f), casth(6f), Screen.width / 8, height), "Win!")) SSDirector.getInstance().currentSceneController.Restart();
		} else if (SSDirector.getInstance().state == State.LOSE) {
			StopAllCoroutines();
			if (GUI.Button(new Rect(castw(2f), casth(6f), Screen.width / 7, height), "Lose!")) SSDirector.getInstance().currentSceneController.Restart();
		}
	}

	void Update() {
		action.click();
	}

}