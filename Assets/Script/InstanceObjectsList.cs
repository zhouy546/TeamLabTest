using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceObjectsList : MonoBehaviour {
    public static InstanceObjectsList instance;
    public GameObject DrawLineDestoryParticle;
    // Use this for initialization
    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
    }

}
