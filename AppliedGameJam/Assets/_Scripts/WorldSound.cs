using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WorldSound : ObjectSelecter {
    private AudioSource audioSource;

    public float lowRandomPitch = 0.9f;
    public float highRandomPitch = 1.1f;

    public AudioClip[] clickClip;
    public AudioClip[] waterClip;
    public AudioClip[] starsClip;

    public LayerMask waterLayerMask;
    public LayerMask starsLayerMask;
    

    // Use this for initialization
    void Start() {
        audioSource = GetComponent<AudioSource>();

    }

    private void FixedUpdate() {
        clickedGameObject = GetGameObjectOnClick(layerMask, sceneCamera);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            clickedGameObject = GetGameObjectOnClick(layerMask, sceneCamera);
        } else {
            clickedGameObject = null;
        }
        if (clickedGameObject != null) {
            //do sound
            CheckLayerThenPlayAudio();


            clickedGameObject = null;

        }
    }

    void CheckLayerThenPlayAudio() {
        if (((1 << clickedGameObject.gameObject.layer) & waterLayerMask) != 0) {
            PlayAudio(waterClip);
        } else if (clickedGameObject.gameObject.tag == "Space") {
            PlayAudio(starsClip);
        } else {
            PlayAudio(clickClip);
        }
    }

    void PlayAudio(AudioClip[] audioClip) {
        audioSource.pitch = Random.Range(lowRandomPitch, highRandomPitch);
        audioSource.clip = audioClip[Random.Range(0, audioClip.Length)];
        audioSource.Play();
    }

}
