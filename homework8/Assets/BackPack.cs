using System.Collections;
using System.Collections.Generic;
using Game_Manager;
using UnityEngine;
using UnityEngine.UI;

public class BackPack : MonoBehaviour {

    private Game_Scene_Manager gsm;
    private Image bag_image;
    public int mouse_type;
    public Sprite head;
    public Sprite arm;
    public Sprite shoes;
    public Sprite UIMask;

    void Awake() {
        gsm = Game_Scene_Manager.GetInstance();
        bag_image = GetComponent<Image>();
    }

    public void On_BackUp_Button() {
        int MouseType = gsm.GetMouse().GetMouseType();
        if (bag_image.sprite != UIMask && MouseType == 0) {
            bag_image.sprite = UIMask;
            gsm.GetMouse().SetMouseType(mouse_type);
            mouse_type = 0;
        } else if (bag_image.sprite == UIMask) {
            if (MouseType == 1)
                bag_image.sprite = head;
            else if (MouseType == 2)
                bag_image.sprite = arm;
            else if (MouseType == 3)
                bag_image.sprite = shoes;
            mouse_type = MouseType;
            gsm.GetMouse().SetMouseType(0);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
