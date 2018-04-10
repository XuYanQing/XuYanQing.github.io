using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour, IUserAction, ISceneController {

	public CCActionManager actionManager;

	List<GameObject> LeftObjList = new List<GameObject>();
	List<GameObject> RightObjList = new List<GameObject>();
	GameObject[] boat = new GameObject[2];

	GameObject boat_obj, leftShore_obj, rightShore_obj;

	Vector3 LeftShorePos = new Vector3(-12, 0, 0);
	Vector3 RightShorePos = new Vector3(12, 0, 0);
	Vector3 BoatLeftPos = new Vector3(-4, 0, 0);
	Vector3 BoatRightPos = new Vector3(4, 0, 0);

	void Awake() {
		SSDirector director = SSDirector.getInstance();
		director.setFPS(60);
		director.currentSceneController = this;
		director.currentSceneController.LoadResources();
		director.leaveSeconds = director.totalSeconds;
	}

	void Start () {
		SSDirector.getInstance().state = State.PAUSE;
		SSDirector.getInstance().countDownTitle = "Start";
		actionManager = GetComponent<CCActionManager>() as CCActionManager;
	}

	void Update() {
		check();
	}

	public void LoadResources() {
		GameObject priest_obj, devil_obj;
		Camera.main.transform.position = new Vector3(0, 0, -20);

		leftShore_obj = Instantiate(Resources.Load("prefabs/Shore"), LeftShorePos, Quaternion.identity) as GameObject;
		rightShore_obj = Instantiate(Resources.Load("prefabs/Shore"), RightShorePos, Quaternion.identity) as GameObject;
		leftShore_obj.name = "left_shore";
		rightShore_obj.name = "right_shore";

		boat_obj = Instantiate(Resources.Load("prefabs/Boat"), BoatLeftPos, Quaternion.identity) as GameObject;
		boat_obj.name = "boat";
		boat_obj.transform.parent = leftShore_obj.transform;

		for (int i = 0; i < 3; ++i) {
			priest_obj = Instantiate(Resources.Load("prefabs/Priest")) as GameObject;
			priest_obj.name = i.ToString();
			priest_obj.transform.position = new Vector3(-16f + 1.5f * Convert.ToInt32(priest_obj.name), 2.7f, 0);
			priest_obj.transform.parent = leftShore_obj.transform;
			LeftObjList.Add(priest_obj);

			devil_obj = Instantiate(Resources.Load("prefabs/Devil")) as GameObject;
			devil_obj.name = (i + 3).ToString();
			devil_obj.transform.position = new Vector3(-16f + 1.5f * Convert.ToInt32(devil_obj.name), 2.7f, 0);
			devil_obj.transform.parent = leftShore_obj.transform;
			LeftObjList.Add(devil_obj);
		}
	}

	public void click() {
		GameObject gameObj = null;

		if (Input.GetMouseButtonDown(0) && 
			(SSDirector.getInstance().state == State.START || SSDirector.getInstance().state == State.CONTINUE)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) gameObj = hit.transform.gameObject;
		}

		if (gameObj == null) return;
		else if (gameObj.name == "0" || gameObj.name == "1" || gameObj.name == "2"
			|| gameObj.name == "3" || gameObj.name == "4" || gameObj.name == "5")
			MovePeople(gameObj);
		else if(gameObj.name == "boat") MoveBoat();
	}

	void MovePeople(GameObject people) {
		int shoreNum, seatNum;

		if (people.transform.parent == boat_obj.transform.parent && (boat[0] == null || boat[1] == null)) {
			seatNum = boat[0] == null ? 0 : 1;
			if (people.transform.parent == leftShore_obj.transform) {
				shoreNum = 0;
				for (int i = 0; i < LeftObjList.Count; i++) {
					if (people.name == LeftObjList[i].name) {
						Debug.Log (actionManager);
						actionManager.getOnBoat(people, shoreNum, seatNum);
						LeftObjList.Remove(LeftObjList[i]);
					}
				}
			} else {
				shoreNum = 1;
				for (int i = 0; i < RightObjList.Count; i++) {
					if (people.name == RightObjList[i].name) {
						actionManager.getOnBoat(people, shoreNum, seatNum);
						RightObjList.Remove(RightObjList[i]);
					}
				}
			}
			boat[seatNum] = people;
			people.transform.parent = boat_obj.transform;
		} else if (people.transform.parent == boat_obj.transform) {
			shoreNum = boat_obj.transform.parent == leftShore_obj.transform ? 0 : 1;
			seatNum = (boat[0] != null && boat[0].name == people.name) ? 0 : 1;

			actionManager.getOffBoat(people, shoreNum);

			boat[seatNum] = null;
			if(shoreNum == 0) {
				people.transform.parent = leftShore_obj.transform;
				LeftObjList.Add(people);
			} else {
				people.transform.parent = rightShore_obj.transform;
				RightObjList.Add(people);
			}
		}
	}

	void MoveBoat() {
		if (!(boat[0]==null && boat[1] == null)) {
			actionManager.moveBoat(boat_obj);
			boat_obj.transform.parent = boat_obj.transform.parent == leftShore_obj.transform ? rightShore_obj.transform : leftShore_obj.transform;
		}
	}

	public void check() {
		int left_d = 0, left_p = 0, right_d = 0, right_p = 0;

		foreach (GameObject element in LeftObjList) {
			if (element.tag == "Priest") left_p++;
			if (element.tag == "Devil") left_d++;
		}

		foreach (GameObject element in RightObjList) {
			if (element.tag == "Priest") right_p++;
			if (element.tag == "Devil") right_d++;
		}

		for (int i = 0; i < 2; i++) {
			if (boat[i] != null && boat_obj.transform.parent == leftShore_obj.transform) {
				if (boat[i].tag == "Priest") left_p++;
				else left_d++;
			}
			if (boat[i] != null && boat_obj.transform.parent == rightShore_obj.transform) {
				if (boat[i].tag == "Priest") right_p++;
				else right_d++;
			}
		}

		if ((left_d > left_p && left_p != 0) || (right_d > right_p && right_p != 0) || SSDirector.getInstance().leaveSeconds == 0)
			SSDirector.getInstance().state = State.LOSE;
		else if (right_d == right_p && right_d == 3) SSDirector.getInstance().state = State.WIN;
	}

	public void Pause() {
		SSDirector.getInstance().state = State.PAUSE;
	}

	public void Resume() {
		SSDirector.getInstance().state = State.CONTINUE;
	}

	public void Restart() {
		Application.LoadLevel(Application.loadedLevelName);
		SSDirector.getInstance().state = State.START;
	}
}
