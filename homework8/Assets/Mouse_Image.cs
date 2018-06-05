using System.Collections;
using System.Collections.Generic;
using Game_Manager;
using UnityEngine;
using UnityEngine.UI;

public class Mouse_Image : MonoBehaviour {

    private Game_Scene_Manager gsm;
    private Image mouse_image;
    private int mouse_type = 0;
    public Sprite none;
    public Sprite head;
    public Sprite arm;
    public Sprite shoes;
    public Color None;
    public Color NotNone;
    public Camera cam;

    void Awake() {
        gsm = Game_Scene_Manager.GetInstance();
        gsm.SetMouse(this);
        mouse_image = GetComponent<Image>();
    }

    public int GetMouseType() {
        return mouse_type;
    }

    public void SetMouseType(int Mouse_type) {
        mouse_type = Mouse_type;
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Debug.Log(mouse_type);
        if (mouse_type == 0) {
            mouse_image.sprite = none;
            mouse_image.color = None;
        } else {
            mouse_image.color = NotNone;
            if (mouse_type == 1)
                mouse_image.sprite = head;
            else if (mouse_type == 2)
                mouse_image.sprite = arm;
            else if (mouse_type == 3)
                mouse_image.sprite = shoes;
            Vector3 mp = Input.mousePosition;
            Vector3 mmp = cam.ScreenToWorldPoint(mp + new Vector3(0, 0, 350));
            //transform.position = new Vector3(mp.x - 450, mp.y - 200, 0);
            transform.position = new Vector3(mmp.x, mmp.y, 0);

            // Debug.Log("mp: " + mp);
            // Debug.Log("mmp: " + mmp);
        }

    }
}
