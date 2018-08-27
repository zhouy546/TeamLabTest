using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public class ObjectPool : MonoBehaviour
    {
        public GameObject PPL;
        public Queue<Platformer2DUserControl> PPLSPOOL = new Queue<Platformer2DUserControl>();
        public Transform pplTrans;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            CreatePPL();
        }
    }

    void CreatePPL()
    {
        //Platformer2DUserControl platFormCTR = pplTrans.GetComponentInChildren<Platformer2DUserControl>();

        if (PPLSPOOL.Count == 0)
        {
            NewPPL();
        }
        else {
            EnQueuePool();
        }
    }


    void NewPPL() {
        GameObject p = Instantiate(PPL);

        p.transform.position = ValueSheet.PPLSpawnPos;

       // PPLSPOOL.Add(p.GetComponent<Platformer2DUserControl>());
    }

    void EnQueuePool() {
        Platformer2DUserControl platformer2DUserControl = PPLSPOOL.Dequeue();

        platformer2DUserControl.gameObject.SetActive(true);

        platformer2DUserControl.transform.SetParent(null);

        platformer2DUserControl.transform.position = ValueSheet.PPLSpawnPos;

    }
}