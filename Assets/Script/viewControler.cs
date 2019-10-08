

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class viewControler : MonoBehaviour
{
    public float distance = 5f;
    public float Rotation_x, Rotation_y;
    //public GameObject target;
    public Vector3 offset; //偏移量
    public float eyeDistance;  //两个摄像机间距
    public float viewOffset;    //视线偏移角
    public float viewportOffset;
    public float screen_scale;  //缩放系数
    public GameObject leftCameraObj,rightCameraObj;
    public bool smooth = true;

    [SerializeField]private bool useVR = false;
    private Quaternion leftCameraRotation,rightCameraRotation;
    private Camera leftCamera, rightCamera;
    [SerializeField]private GameObject leftMaskCam, rightMaskCam,topMaskCam,bottomMaskCam;
    private Gyroscope gyroscope;    //
  //  private Camera m_camera = Camera.main;

   
    private void Start()
    {
        leftCamera = leftCameraObj.GetComponent<Camera>();
        rightCamera = rightCameraObj.GetComponent<Camera>();
        //leftMaskCam = leftCameraObj.transform.GetChild(0). GetComponent<Camera>();
        //rightMaskCam = rightCameraObj.transform.GetChild(0).GetComponent<Camera>();
        gyroscope = Input.gyro;
        gyroscope.enabled = true;
        //Screen.sleepTimeout = 30;
        gyroscope.updateInterval = 60f;

        if(!useVR )
        {
            leftCamera.rect = new Rect(0, 0, 1.0f, 1.0f);
        }
    }

    private void LateUpdate()
    {
        if(useVR)
        {
            VRView();
        }
        else
        {
            normalView();
        }
        
    }

    private Vector3 eulerAngelFromGyro()
    {
        if(SystemInfo.supportsGyroscope)
        {
            Quaternion temp = gyroscope.attitude;
            temp = Quaternion.Euler(90, 0, 0) * new Quaternion(-temp.x, -temp.y, temp.z, temp.w);
            return temp.eulerAngles;
        }
        
        else
        {
            return Vector3.zero;
        }
    }

    private void changeMask(float scale_num)
    {
        leftMaskCam.GetComponent<Camera>().rect = new Rect(0f, 0f, scale_num, 1f);
        rightMaskCam.GetComponent<Camera>().rect = new Rect(1f - scale_num, 0, scale_num, 1f);
        topMaskCam.GetComponent<Camera>().rect = new Rect(0, 0, 1, scale_num);
        bottomMaskCam.GetComponent<Camera>().rect = new Rect(0, 1 - scale_num, 1, scale_num);
    }
    private void VRView()
    {
        var eulerAngel = eulerAngelFromGyro();
        leftCameraRotation = Quaternion.Euler(Rotation_y+eulerAngel.x, Rotation_x + viewOffset, eulerAngel.z);
        if (smooth)
        {
            leftCamera.transform.rotation = Quaternion.Lerp(leftCamera.transform.rotation, leftCameraRotation, 0.1f);
        }
        else
        {
            leftCamera.transform.rotation = leftCameraRotation;
        }
        //leftCamera.transform.position = leftCameraRotation * new Vector3(0, 0, -distance) + new Vector3(transform.position.x, 0, transform.position.z)+ offset + new Vector3(-eyeDistance, 0, 0);
        leftCamera.transform.position = new Vector3(transform.position.x, 0, transform.position.z) + offset + new Vector3(-eyeDistance, 0, 0);

        leftCamera.rect = new Rect(viewportOffset, viewportOffset, 0.5f - viewportOffset, 1- 2*viewportOffset);
       
        
        rightCameraRotation = Quaternion.Euler(Rotation_y+ eulerAngel.x, Rotation_x - viewOffset, eulerAngel.z);
        if (smooth)
        {
            rightCamera.transform.rotation = Quaternion.Lerp(rightCamera.transform.rotation, rightCameraRotation, 0.1f);
        }
        else
        {
            rightCamera.transform.rotation = rightCameraRotation;
        }
        rightCamera.transform.position = new Vector3(transform.position.x, 0, transform.position.z) + offset + new Vector3(eyeDistance, 0, 0);
        //rightCamera.transform.position = rightCameraRotation * new Vector3(0, 0, -distance) + new Vector3(transform.position.x, 0, transform.position.z) + offset + new Vector3(eyeDistance, 0, 0);
        //Camera.main.transform.rotation = CameraRotation;
        //Camera.main.transform.position = CameraRotation * new Vector3(0, 0, -distance) + transform.position + offset;
        rightCamera.rect = new Rect(0.5f, viewportOffset, 0.5f - viewportOffset, 1 - 2*viewportOffset);


        changeMask(viewportOffset);
    }

    private void normalView()
    {
        leftCameraRotation = Quaternion.Euler(Rotation_y, Rotation_x + viewOffset, eulerAngelFromGyro().z);
        if (smooth)
        {
            leftCamera.transform.rotation = Quaternion.Lerp(leftCamera.transform.rotation, leftCameraRotation, 0.1f);
        }
        else
        {
            leftCamera.transform.rotation = leftCameraRotation;
        }
        //leftCamera.transform.position = leftCameraRotation * new Vector3(0, 0, -distance) + transform.position + offset + new Vector3(-eyeDistance, 0, 0);
        leftCamera.transform.position = new Vector3(transform.position.x, 0, transform.position.z) + offset ;
        leftCamera.rect = new Rect(0.0f, 0.0f, 1.0f,1.0f);
        rightCameraObj.SetActive(false);
    }

    //private void OnGUI()
    //{
    //    GUILayout.TextArea(eulerAngelFromGyro().ToString());
    //}
}
