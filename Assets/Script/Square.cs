using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : WorldObject {
    public  int ThunderValue = 0;

    public  int ThunderMaxValue = 1;

    public  int ThunderMinValue = 0;

    public  int RainValue = 0;

    public  int RainMaxValue = 100;

    public  int RainMinValue = 0;

    float BurningFuel = 200f;

    float currentFuel;

    bool isburning;

    public ParticleSystem Particle;


    // Use this for initialization
    void Start () {
        currentFuel = BurningFuel;

    }

    private void ResetObj()
    {
        ResetBurn();
    }

    private void ResetBurn()
    {
        currentFuel = BurningFuel;
        ThunderValue = ThunderMinValue;
        isburning = false;
        spriteRenderer.color = Color.gray;
        Particle.Stop();
        Debug.Log("reset");
    }

    // Update is called once per frame
    void Update () {
       
        if (ThunderValue == ThunderMaxValue) {
            burn(10f);
        }
	}

    void burn(float speed) {
        if (!isburning) {

            isburning = true;

            Particle.Play();

            spriteRenderer.color = new Color(255f, 255f, 255f, 0);

        }

        if (currentFuel > 0)//update burn
        {
            currentFuel = currentFuel - Time.deltaTime * speed;
      
            float size = UtilityFun.Mapping(currentFuel, 0f, BurningFuel, 0f, 0.45f);

            ParticleSystem.MainModule main = Particle.main;

            main.startSize = size;
        }
        else {// stop burn
            ResetBurn();

        }
    }

    private void OnParticleCollision(GameObject other)
    {
        WorldObject WO = other.GetComponent<WorldObjectParticle>().worldObject;

        if (WO.objectType == WorldObject.ObjectType.ThunderCloud) {

           ThunderValue = Mathf.Clamp(ThunderValue + 1, ThunderMinValue, ThunderMaxValue);
            Debug.Log("thunder cloud");

        }
        else if (WO.objectType == WorldObject.ObjectType.RainCloud)
        {
           //hunderValue= Mathf.Clamp(ThunderValue -1, 0, 5);
            Debug.Log("Rain");
            RainValue = Mathf.Clamp(RainValue + 1, RainMinValue, RainMaxValue);

            FireExtinction(isburning);

        }
    }

    void FireExtinction(bool _isBurn) {
        if (_isBurn) {
            cutDownFule(5f);
        }
    }


    void cutDownFule(float value) {
          currentFuel = currentFuel - value;
    }
}
