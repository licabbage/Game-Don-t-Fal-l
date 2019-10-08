using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuController : MonoBehaviour {

    // Use this for initialization
  
    public GameObject player;
    public GameObject menu; //菜单UI对象
    public GameObject slidersObj;  //滑块UI对象
    public GameObject buttonsObj;   //buttons对象
    public GameObject path;
    public GameObject scoreboardObj;   //记分板UI对象
    public GameObject max_scoreObj; //最高分记录对象
    public GameObject afterDiePanel;    //死亡面板
    public GameObject SALobj;
    public bool onPause;
    

    private Slider[] sliders;
    private Button[] buttons;
    private viewControler viewControl;
    private Text scorboard;
    
    private int CurrentOption = 0;
    private int buttonOption = 0;
    private bool canResponseV = true;
    private bool canResponseH = true;

    void Start () {
        onPause = false;
        menu.SetActive(false);
        scoreboardObj.SetActive(true);
        max_scoreObj.SetActive(true);
        viewControl = player.GetComponent<viewControler>();
        initSliders();
        initButtons();
        scorboard = scoreboardObj.GetComponent<Text>();

        //显示最高得分
        max_scoreObj.GetComponent<Text>().text = SALobj.GetComponent<SaveAndLoad>().getMaxScore().ToString();
    }
	
    

	// Update is called once per frame
	void Update () {
        //按下菜单键
        pauseControl();

        manageScoreboard();
       
        if (onPause)
        {     
            manageMenu();
            OnchageValue();
            OnClickButton();
            scaleCanvas();
        }
    }


    void setMaxScore()
    {

    }
    private void pauseControl()
    {
        
        if ((Input.GetKeyDown(KeyCode.JoystickButton4) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyUp(KeyCode.JoystickButton1)))
        {
            if (afterDiePanel.activeSelf)
                return;
            menu.SetActive(!menu.activeSelf);
            scoreboardObj.SetActive(!scoreboardObj.activeSelf);
            max_scoreObj.SetActive(!max_scoreObj.activeSelf);
            if (!onPause)
            {
                Time.timeScale = 0;
                onPause = true;
            }
            else
            {
                Time.timeScale = 1;
                onPause = false;

            }
        }

        //if (onPause &&( Input.GetKeyUp(KeyCode.Joystick1Button1) ||Input.GetKeyUp(KeyCode.C)))
        //{
        //    canvas.SetActive(false);
        //    Time.timeScale = 1;
        //    onPause = false;

        //}
    }


    private void initButtons()
    {
        buttons = new Button[buttonsObj.transform.childCount];
        for(int i=0; i<buttons.Length;i++)
        {
            buttons[i] = buttonsObj.transform.GetChild(i).GetComponent<Button>();
        }
    }
    private void initSliders()
    {
        sliders = new Slider[slidersObj.transform.childCount];

        Debug.Log(sliders.Length);
        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i] = slidersObj.transform.GetChild(i).GetComponent<Slider>();
        }      

        //控制视角y
        sliders[0].minValue = 10;
        sliders[0].maxValue = 85;
        sliders[0].value = viewControl.Rotation_y;

        //控制视角x
        sliders[1].minValue = -40;
        sliders[1].maxValue = 40;
        sliders[1].value = viewControl.Rotation_x;

        //控制方块消失间隔
        sliders[2].minValue = 0.7f;
        sliders[2].maxValue = 1.5f;
        sliders[2].value = path.GetComponent<createPath>().deltTime;

        //控制两眼视角角度
        sliders[3].minValue = -5.0f;
        sliders[3].maxValue = 5.0f;
        sliders[3].value =viewControl.viewOffset;

        //控制两眼间距
        sliders[4].maxValue = 1.0f;
        sliders[4].minValue = -1.0f;
        sliders[4].value = viewControl.eyeDistance;

        //控制viewport;
        sliders[5].maxValue = 0.2f;
        sliders[5].minValue = 0.0f;
        sliders[5].value = viewControl.viewportOffset;


    }


    private void manageMenu()
    {
        int v = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
        int h = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));

        
        if (canResponseV && (v != 0))
        {
            
            if (CurrentOption < sliders.Length)
            {
                sliders[CurrentOption].transform.GetChild(0).GetComponent<Image>().color = Color.white;
                sliders[CurrentOption].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = Color.white;
            }
            else
            {
                buttons[buttonOption].GetComponent<Image>().color = Color.white;
            }
            CurrentOption -= v;
            CurrentOption = Mathf.Clamp(CurrentOption, 0, sliders.Length);
            //响应输入后设置一段时间无法响应
            canResponseV = false;
        }
        //手柄摇杆回来时可以再次响应输入
        if (v == 0 && h == 0)
        {
            canResponseV = true;
        }
        else
            canResponseV = false;

        //控制滑块
        if (CurrentOption < sliders.Length)
        {
            if (v != 0)
                return;
            sliders[CurrentOption].value += h * (sliders[CurrentOption].maxValue - sliders[CurrentOption].minValue) / 100f;
            sliders[CurrentOption].transform.GetChild(0).GetComponent<Image>().color = Color.red;
            sliders[CurrentOption].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = Color.red;
            
        }
        //控制button
        else
        {
            if (canResponseH && h != 0)
            {
                buttons[buttonOption].GetComponent<Image>().color = Color.white;
                buttonOption += h;
                buttonOption = Mathf.Clamp(buttonOption, 0, buttons.Length - 1);
                //响应输入后设置一段时间无法响应
                canResponseH = false;
            }
            if (h == 0 && v ==0)
            {
                canResponseH = true;
            }
            buttons[buttonOption].GetComponent<Image>().color = Color.red;

            //点击时显示效果
            if (Input.GetKey(KeyCode.K) || Input.GetKey(KeyCode.JoystickButton0))
            {
                buttons[buttonOption].GetComponent<Image>().color = Color.gray;
            }
        }
    }

    public void OnchageValue()
    {
        switch (CurrentOption)
        {
            case 0:
                viewControl.Rotation_y = sliders[CurrentOption].value;
                break;
            case 1:
                viewControl.Rotation_x = sliders[CurrentOption].value;
                break;
            case 2:
                path.GetComponent<createPath>().deltTime = sliders[CurrentOption].value;
                break;
            case 3:
                viewControl.viewOffset = sliders[CurrentOption].value;
                break;
            case 4:
                viewControl.eyeDistance = sliders[CurrentOption].value;
                break;
            case 5:                
                viewControl.viewportOffset = sliders[CurrentOption].value;
                break;
        }
    }

    public void OnClickButton()
    {
        if (CurrentOption < sliders.Length)
            return;
        else
        {
            if (Input.GetKeyUp(KeyCode.K) || Input.GetKeyUp(KeyCode.JoystickButton0))
            {
                switch (buttonOption)
                {
                    case 0:
                        SALobj.GetComponent<SaveAndLoad>().Save();
                        break;
                    case 1:
                        SALobj.GetComponent<SaveAndLoad>().Load();
                        flushSliders();
                        break;
                    case 2:
                        SALobj.GetComponent<SaveAndLoad>().setDefault();
                        flushSliders();
                        break;
                    case 3:
                        Application.Quit();
                       // Debug.Log("exit");
                        break;
                }
            }
        }
    }
    public void rotationYchanged(float value)
    {
        Debug.Log(value.ToString());
    }

    public void manageScoreboard()
    { 
        scorboard.text = path.GetComponent<createPath>().getCurrentDistance().ToString();
    }
    
    public void flushSliders()
    {
        sliders[0].value = viewControl.Rotation_y;
        sliders[1].value = viewControl.Rotation_x;
        sliders[2].value = path.GetComponent<createPath>().deltTime;
        sliders[3].value = viewControl.viewOffset;
        sliders[4].value = viewControl.eyeDistance;
        sliders[5].value = viewControl.viewportOffset;
    }


    private void scaleCanvas()
    {
        float f = 1- 2*viewControl.viewportOffset;
        Vector3 scale =  new Vector3(f, f, 1);
        menu.transform.localScale = scale;
        max_scoreObj.transform.localScale = scale;
        scoreboardObj.transform.localScale = scale;
        afterDiePanel.transform.localScale = scale;
    }


    //private void OnGUI()
    //{
    //    GUILayout.TextArea(CurrentOption.ToString());
    //}
}
