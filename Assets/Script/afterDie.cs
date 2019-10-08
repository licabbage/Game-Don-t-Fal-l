using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class afterDie : MonoBehaviour {

    public GameObject buttonRestarObj, buttonExitObj;
    public GameObject finalScoreObj;
    public GameObject path;
    public GameObject playerObj;
    private int currentOption = 1;
    private bool canResponse = true;
	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
        buttonRestarObj.GetComponent<Image>().color = Color.red;
    }
	
	// Update is called once per frame
	void Update () {

        work();
    }

    void work()
    {
        int v = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
        int h = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
        if (canResponse && v != 0)
        {
            currentOption -= v;
            currentOption = Mathf.Clamp(currentOption, 1, 2);



            if (currentOption == 1)
            {
                buttonExitObj.GetComponent<Image>().color = Color.white;
                buttonRestarObj.GetComponent<Image>().color = Color.red;
            }
            else
            {
                buttonExitObj.GetComponent<Image>().color = Color.red;
                buttonRestarObj.GetComponent<Image>().color = Color.white;
            }
            canResponse = false;
        }


        if (v == 0)
            canResponse = true;

        if (Input.GetKey(KeyCode.K) || Input.GetKey(KeyCode.JoystickButton0))
        {
            if (currentOption == 1)
            {//重开
                buttonRestarObj.GetComponent<Image>().color = Color.gray;
                Time.timeScale = 1.0f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {//退出
                buttonExitObj.GetComponent<Image>().color = Color.gray;
                
                Application.Quit();
            }
        }

    }

    public void beginWork()
    {
        gameObject.SetActive(true);

        float f = 1 - 2 * playerObj.GetComponent<viewControler>().viewportOffset;
        Vector3 scale = new Vector3(f, f, 1);
        transform.localScale = scale;
        Time.timeScale = 0;

        finalScoreObj.GetComponent<Text>().text = path.GetComponent<createPath>().getCurrentDistance().ToString();
        
    }
}
