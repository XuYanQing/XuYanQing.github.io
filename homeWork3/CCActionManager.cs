using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager, ISSActionCallback {

	public FirstController scene;
	public CCMoveToAction action1, action2;
	public CCSequenceAction saction;
	float speed = 30f;

	public void moveBoat(GameObject boat) {
		action1 = CCMoveToAction.GetSSAction((boat.transform.position ==  new Vector3(4, 0, 0)? new Vector3(-4, 0, 0) : new Vector3(4, 0, 0)), speed);
		this.RunAction(boat, action1, this);
	}

	public void getOnBoat(GameObject people, int shore, int seat) {
		if (shore == 0 && seat == 0) {
			action1 = CCMoveToAction.GetSSAction(new Vector3(-5f, 2.7f, 0), speed);
			action2 = CCMoveToAction.GetSSAction(new Vector3(-5f, 1.2f, 0), speed);
		}
		else if(shore==0 && seat == 1) {
			action1 = CCMoveToAction.GetSSAction(new Vector3(-3f, 2.7f, 0), speed);
			action2 = CCMoveToAction.GetSSAction(new Vector3(-3f, 1.2f, 0), speed);
		}
		else if (shore == 1 && seat == 0) {
			action1 = CCMoveToAction.GetSSAction(new Vector3(3f, 2.7f, 0), speed);
			action2 = CCMoveToAction.GetSSAction(new Vector3(3f, 1.2f, 0), speed);
		}
		else if (shore == 1 && seat == 1) {
			action1 = CCMoveToAction.GetSSAction(new Vector3(5f, 2.7f, 0), speed);
			action2 = CCMoveToAction.GetSSAction(new Vector3(5f, 1.2f, 0), speed);
		}

		CCSequenceAction saction = CCSequenceAction.GetSSAction(0, 0, new List<SSAction> { action1, action2 });
		this.RunAction(people, saction, this);
	}

	public void getOffBoat(GameObject people, int shoreNum) {
		action1 = CCMoveToAction.GetSSAction(new Vector3(people.transform.position.x, 2.7f, 0), speed);
		if (shoreNum == 0)  action2 = CCMoveToAction.GetSSAction(new Vector3(-16f + 1.5f * System.Convert.ToInt32(people.name), 2.7f, 0), speed);
		else action2 = CCMoveToAction.GetSSAction(new Vector3(16f - 1.5f * System.Convert.ToInt32(people.name), 2.7f, 0), speed);

		CCSequenceAction saction = CCSequenceAction.GetSSAction(0, 0, new List < SSAction >{ action1, action2});
		this.RunAction(people, saction, this);
	}

	protected void Start() {
		scene = (FirstController)SSDirector.getInstance().currentSceneController;
		scene.actionManager = this;
	}

	protected new void Update() {
		base.Update();
	}

	public void SSActionEvent(SSAction source,SSActionEventType events = SSActionEventType.Completed,
		int intParam = 0,
		string strParam = null,
		Object objectParam = null) {
		Debug.Log("Done");
	}
}