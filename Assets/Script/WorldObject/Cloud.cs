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

        StartCoroutine(PlaySound(Particle.main.duration));
    }

    IEnumerator PlaySound(float StopInSec) {
        FadeInSound();
        yield return new WaitForSeconds(StopInSec-1);
        FadeOutSound();
    }

    void FadeInSound() {
        PlaySound();
        audioSource.volume = 0;
        LeanTween.value(0, 1, 1f).setOnUpdate(delegate (float val)
        {
            audioSource.volume = val;
        });
    }

    void FadeOutSound() {
        LeanTween.value(1, 0, 1f).setOnUpdate(delegate (float val)
        {
            audioSource.volume = val;
        }).setOnComplete(delegate () {
            StopSound();
            audioSource.volume = 1;
        });
    }

    public void PlaySound() {
        audioSource.PlayOneShot(sound);
    }

    public void StopSound() {
        audioSource.Stop();
    }
}
