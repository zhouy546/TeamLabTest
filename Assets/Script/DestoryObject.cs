using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryObject : MonoBehaviour { 

    public Mesh EmitMesh;

    public float DestoryTime;

    public delegate void ObjDestory();
    public event ObjDestory onDestory;

    public delegate void ObjectCreate();
    public event ObjectCreate onCreate;

    private GameObject DestoryParticle;
    // Use this for initialization
    private void OnEnable()
    {//-------------onDestory subscribe---------------------
        onDestory += DeleteObject;
        onDestory += playDestorySound;
        onDestory += playDestoryParticle;
        //-----------------------------------------------------

        //-------------onCreate subscribe---------------------
        onCreate += playCreateSound;
        onCreate += CreateSetup;
        //-----------------------------------------------------
    }

    private void OnDisable()
    {
    //-------------onDestory desubscribe---------------------
        onDestory -= DeleteObject;
        onDestory -= playDestorySound;
        onDestory -= playDestoryParticle;
        //-------------------------------------------------------

        //-------------onCreate desubscribe---------------------
        onCreate -= playCreateSound;
        onCreate -= CreateSetup;
        //-----------------------------------------------------
    }

    void Start () {

        mCreate();

        StartCoroutine(DodestoryEvent());
	}
	
	// Update is called once per frame
	void Update () {
		
	}




    IEnumerator DodestoryEvent() {
        yield return new WaitForSeconds(ValueSheet.DrawLineDestoryTime);

        mDestory();
    }

    void mCreate() {
        if (onCreate != null)
        {
            onCreate();
        }
    }

    void mDestory() {
        if (onDestory != null) {
            onDestory();
        }
    }

    void DeleteObject() {
        Destroy(this.gameObject,.1f);
    }

    void playDestorySound() {
        //---generate sound on delete position------
    }

    void playDestoryParticle() {
        //--generate particle on delete position-----
        GameObject g = Instantiate(DestoryParticle);
        g.GetComponent<ParticleCTR>().initialization(EmitMesh);
    }

    void CreateSetup() {
        DestoryParticle = InstanceObjectsList.instance.DrawLineDestoryParticle;
    }

    void playCreateSound() {

    }
}
