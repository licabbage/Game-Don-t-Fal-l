using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testInput : MonoBehaviour {

	float x;
	float y;
	public GameObject obj;
    public SimpleTouchController m_controller;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //if (Input.GetKeyDown(KeyCode.JoystickButton4))
        //{
        //    Debug.Log("clicked");

        //}
        //x = Input.GetAxis("Horizontal");
        //y = Input.GetAxis("Vertical");
        //obj.transform.localPosition = new Vector3(x, y, obj.transform.localPosition.z);


        //Debug.Log("x is" + x);
        //Debug.Log("y is" + y);
        x = m_controller.GetTouchPosition.x;
        y = m_controller.GetTouchPosition.y;
    }



    void OnGUI()
	{
		GUILayout.TextArea ("x is " +x.ToString ());
		GUILayout.TextArea ("y is " + y.ToString ());
		GUILayout.TextArea ("queren " + Input.GetKey (KeyCode.JoystickButton0).ToString());
		GUILayout.TextArea ("fanhui " + Input.GetKey (KeyCode.JoystickButton1).ToString());
		GUILayout.TextArea ("caidan " + Input.GetKey (KeyCode.JoystickButton4).ToString());
        GUILayout.TextArea("joysticks " + Input.GetJoystickNames());

    }
}
