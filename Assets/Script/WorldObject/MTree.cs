using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class MTree : WorldObject {
     Animator animator;

    public int RainValue = 0;

    public int RainMaxValue = 100;

    public int RainMinValue = 0;


    void Start()
    {

        initialization();
    }

    public new void initialization() {
        animator = this.GetComponent<Animator>();

        base.initialization();
    }

    void Update()
    {

       float growValue= UtilityFun.Mapping(RainValue, RainMinValue, RainMaxValue, 0, 1);
        
        animator.Play("Tree01", 0, growValue);

    }

    private void ResetObj()
    {
        
    }

    private void ResetGrow() {


    }

    private void OnParticleCollision(GameObject other)
    {
        //Debug.Log(other.name);
        WorldObject WO = other.GetComponent<WorldObjectParticle>().worldObject;

        if (WO.objectType == WorldObject.ObjectType.RainCloud)
        {
            //hunderValue= Mathf.Clamp(ThunderValue -1, 0, 5);
         //   Debug.Log("Rain");
            RainValue = Mathf.Clamp(RainValue + 1, RainMinValue, RainMaxValue);
        }
    }
}
