using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour {


    public GameObject pathObj;
    public GameObject playerObj;
    public GameObject menuControllerObj;

    private viewControler m_viewCotroller;

    //默认值
    private float dft_rotation_y = 50f;
    private float dft_rotation_x = 0f;
    private float dft_delt_time = 0.8f;
    private float dft_view_offset = -3f;
    private float dft_eye_distance = 0f;
    private float dft_viewport_offset = 0.15f;
    private int max_score;

    private void Awake()
    {
        m_viewCotroller = playerObj.GetComponent<viewControler>();
        Load();
        LoadMaxScore();
    }
    // Use this for initialization
    void Start () {
       // m_viewCotroller = playerObj.GetComponent<viewControler>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Save()
    {
        PlayerPrefs.SetFloat("rotation_y", m_viewCotroller.Rotation_y);
        PlayerPrefs.SetFloat("rotation_x", m_viewCotroller.Rotation_x);
        PlayerPrefs.SetFloat("dispearTime", pathObj.GetComponent<createPath>().deltTime);
        PlayerPrefs.SetFloat("view_angle", m_viewCotroller.viewOffset);
        PlayerPrefs.SetFloat("eye_distance", m_viewCotroller.eyeDistance);
        PlayerPrefs.SetFloat("view_port_offset", m_viewCotroller.viewportOffset);
    }

    public void Load()
    {
        m_viewCotroller.Rotation_y =  PlayerPrefs.GetFloat("rotation_y", dft_rotation_y);
        m_viewCotroller.Rotation_x = PlayerPrefs.GetFloat("rotation_x", dft_rotation_x);
        pathObj.GetComponent<createPath>().deltTime =  PlayerPrefs.GetFloat("dispearTime", dft_delt_time);
        m_viewCotroller.viewOffset = PlayerPrefs.GetFloat("view_angle", dft_view_offset);
        m_viewCotroller.eyeDistance = PlayerPrefs.GetFloat("eye_distance", dft_eye_distance);
        m_viewCotroller.viewportOffset = PlayerPrefs.GetFloat("view_port_offset", dft_viewport_offset);
    }

    public void setDefault()
    {
        m_viewCotroller.Rotation_y = dft_rotation_y;
        m_viewCotroller.Rotation_x = dft_rotation_x;
        pathObj.GetComponent<createPath>().deltTime = dft_delt_time;
        m_viewCotroller.viewOffset = dft_view_offset;
        m_viewCotroller.eyeDistance = dft_eye_distance;
        m_viewCotroller.viewportOffset = dft_viewport_offset;
    }

    public void SaveMaxScore()
    {
        PlayerPrefs.SetInt("max_score", pathObj.GetComponent<createPath>().getCurrentDistance());
    }

    public void LoadMaxScore()
    {
        max_score = PlayerPrefs.GetInt("max_score", 0);
    }
    
    public int getMaxScore()
    {
        return max_score;
    }
}
