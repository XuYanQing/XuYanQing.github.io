using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSDirector : MonoBehaviour {

	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public enum State { WIN, LOSE, PAUSE, CONTINUE , START };
	public class SSDirector : System.Object {

		private static SSDirector _instance;
		public int totalSeconds = 60;
		public int leaveSeconds;
		public bool onCountDown = false;
		public string countDownTitle = "Start";

		public ISceneController currentSceneController { get; set; }
		public State state { get; set; }
		public bool running { get; set; }

		// get instance anytime anywhere!
		public static SSDirector getInstance() {
			if (_instance == null) {
				_instance = new SSDirector ();
			}
			return _instance;
		}

		public int getFPS() {
			return Application.targetFrameRate;
		}

		public void setFPS(int fps) {
			Application.targetFrameRate = fps;
		}

		public IEnumerator DoCountDown() {
			while (leaveSeconds > 0) {
				yield return new WaitForSeconds (1f);
				leaveSeconds--;
			}
		}
	}
}
