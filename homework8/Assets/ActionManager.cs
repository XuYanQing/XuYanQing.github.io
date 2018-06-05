using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

namespace Game_Manager {

    public class Game_Scene_Manager : System.Object {
        private static Game_Scene_Manager _instance;
        private static Mouse_Image _Mouse;

        public static Game_Scene_Manager GetInstance() {
            if (_instance == null) {
                _instance = new Game_Scene_Manager();
            }
            return _instance;
        }

        public void SetMouse(Mouse_Image _mouse) {
            if (_Mouse == null) {
                _Mouse = _mouse;
            }
        }

        public Mouse_Image GetMouse() {
            return _Mouse;
        }
    }

}
