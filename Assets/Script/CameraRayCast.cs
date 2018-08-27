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
            //Get the mouse position on the screen and send a raycast into the game world from that position.
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            //If something was hit, the RaycastHit2D.collider will not be null.
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag== "Interactive")
                {

                    Debug.Log(hit.collider.name);
                    WorldObject WO = hit.collider.GetComponent<WorldObject>();
                    WO.Clickdoing();

                }
                else {
                    ValueSheet.b_CreateMesh = true;//Draw Mesh

                }
            }
            else {
                ValueSheet.b_CreateMesh = true;//Draw Mesh

            }
        }
    }
}
