using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fall : MonoBehaviour {

    // Use this for initialization
    Vector3 tragetPosition;
    Vector3 startPosition;

    void Start () {
        tragetPosition =  transform.localPosition;
        
        transform.localPosition += new Vector3(0, 7, 0);
    }
	
	// Update is called once per frame
	void Update () {
        
        transform.localPosition = Vector3.Lerp(transform.localPosition, tragetPosition, 0.05f);

    }


}
