using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
public class AndriodMoveController : MonoBehaviour
{

    // Use this for initialization

    // public GameObject me;
    public float jump;
    public float k = 3.0f;
    public GameObject path;
    public bool useJoysticks = false;
    public GameObject menuController;
    public GameObject SALobj;
    public GameObject afterDieObj;
    public GameObject AudioPlayerObj;
    public GameObject bgmObj;
    public bool isEnd;
    public SimpleTouchController touch_controller;

    private int score;
    private Rigidbody m_rigid;
    private float h, v;
    private bool canJump;
    private bool canInput;
    private AudioSource jumpAudio;

    void Start()
    {
        h = 0f;
        v = 0f;
        canInput = false;
        jumpAudio = AudioPlayerObj.GetComponent<AudioSource>();
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {

        //如果阵亡
        if (transform.position.y < -5.0f)
        {
            isEnd = true;
            bgmObj.GetComponent<AudioSource>().Stop();
            if (score > SALobj.GetComponent<SaveAndLoad>().getMaxScore())
            {
                SALobj.GetComponent<SaveAndLoad>().SaveMaxScore();
            }
            SALobj.GetComponent<SaveAndLoad>().Save();
            afterDieObj.GetComponent<afterDie>().beginWork();
            
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        //球落地前不响应输入
        if (!canInput)
            return;
        //暂停时不响应输入
        if (menuController.GetComponent<menuController>().onPause)
            return;
        moveControl();
        //获取当前移动的距离（得分）
        score = path.GetComponent<createPath>().getCurrentDistance();


    }

    //运动控制
    private void moveControl()
    {
        var position = touch_controller.GetTouchPosition;
        v = position.y;
        h = position.x;

        //使用手柄进行取整改善交互
        if (useJoysticks)
        {
            v = Mathf.Round(v);
            h = Mathf.Round(h);
        }

        m_rigid = this.GetComponent<Rigidbody>();

        //通过输入给物体施加水平力和重力
        m_rigid.AddForce(new Vector3(h, 0, v) * k);
        m_rigid.AddForce(new Vector3(0, -1, 0) * k);

        //根据输入对物体施加弹跳力
        if (canJump && (Input.GetKeyDown(KeyCode.K) || CrossPlatformInputManager.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.JoystickButton0)))
        {
            m_rigid.AddForce(new Vector3(0, 1, 0) * k * jump);
            jumpAudio.Play();
        }
    }
    //便于外界获取h，v的值
    public Vector2 getHV()
    {
        return new Vector2(h, v);
    }
    //private void OnGUI()
    //{
    //    GUILayout.TextArea(h.ToString());
    //    GUILayout.TextArea(v.ToString());
    //    GUILayout.TextArea(canJump.ToString());
    //    GUILayout.TextArea(count.ToString());
    //    GUILayout.TextArea(Time.time.ToString());
    //}

    private void OnCollisionEnter(Collision collision)
    {
        canInput = true;
    }
    private void OnCollisionStay(Collision collision)
    {
        canJump = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        canJump = false;

    }


}
