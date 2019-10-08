using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;
public class androidMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(CrossPlatformInputManager.GetButtonDown("restart"))
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (CrossPlatformInputManager.GetButtonDown("exit"))
        {
            Application.Quit();
            print("exiut");
        }
    }
}
