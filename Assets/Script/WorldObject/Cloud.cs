using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : WorldObject {
    public Vector3 TargetScale;

    public GameObject TransformObject;

    public ParticleSystem Particle;

    public AudioSource audioSource;

    public AudioClip sound;
	// Use this for initialization
	void Start () {

        base.initialization();

	}
	

	void Update () {
		
	}

    public override void Clickdoing()
    {
        base.Clickdoing();

        Particle.Play();

        PlaySound();

        LeanTween.cancel(TransformObject);
        LeanTween.scale(TransformObject, TargetScale, .1f).setOnComplete(delegate () {
            LeanTween.scale(TransformObject, Vector3.one, .1f);
        });
    }

    public void PlaySound() {
        audioSource.PlayOneShot(sound);
    }
}
