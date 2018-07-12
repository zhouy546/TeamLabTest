using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareGenerator : MonoBehaviour {
    public GameObject cubeGameobject;
	// Use this for initialization
	void Start () {
        StartCoroutine(GenerateSquare());
	}



    IEnumerator GenerateSquare() {


      GameObject obj =  Instantiate(cubeGameobject);
        Vector3 pos = new Vector3(Random.Range(-6f, 6f), 5f, 10);
        obj.transform.position = pos;
        yield return new WaitForSeconds(1f);
        StartCoroutine(GenerateSquare());
    }
}
