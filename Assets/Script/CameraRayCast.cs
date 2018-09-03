using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRayCast : MonoBehaviour {
    public TCreateMesh createMesh;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);



           // Debug.Log("----phy----");
            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);
            foreach (var i in hits)
            {
                if (i.collider != null)
                {
                    if (i.collider.gameObject.tag == "Interactive")
                    {

                        Debug.Log(i.collider.name);
                        WorldObject WO = i.collider.GetComponent<WorldObject>();
                        WO.Clickdoing();

                    }
                    else
                    {
                        ValueSheet.b_CreateMesh = true;//Draw Mesh

                    }
                }
                else
                {
                    ValueSheet.b_CreateMesh = true;//Draw Mesh

                }
            }
 
       }
    }
}
