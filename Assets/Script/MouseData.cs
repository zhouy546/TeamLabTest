using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseData : MonoBehaviour {


    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        Debug.Log(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f)));


    }
}
