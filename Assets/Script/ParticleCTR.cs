using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCTR : MonoBehaviour {
    public float destoryTimeDelay;
    public ParticleSystem ParticleSystem;

    // Use this for 
    private void OnEnable()
    {

    }

    void Start () {
		
	}

    public void initialization(Mesh mesh) {

        ParticleSystem = this.GetComponent<ParticleSystem>();

        setupParticle(mesh);

        Destroy(this.gameObject, destoryTimeDelay);
    }

    // Update is called once per frame
    void Update () {
		
	}
    void setupParticle(Mesh mesh) {
       
        ParticleSystem.ShapeModule shapeModule = ParticleSystem.shape;

        shapeModule.mesh = mesh;
    } 
}
