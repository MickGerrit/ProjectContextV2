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
    public AudioClip[] mumbleClip;

    public LayerMask waterLayerMask;
    public LayerMask starsLayerMask;

    public PlanetRotationControls planetRotationControls;
    public bool clickedButton;

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

        if (!audioSource.isPlaying) {
            clickedButton = false;
        }
    }

    void CheckLayerThenPlayAudio() {
        if (!clickedButton) {

            if (((1 << clickedGameObject.gameObject.layer) & waterLayerMask) != 0) {
                PlayAudio(waterClip);
            } else if (clickedGameObject.gameObject.tag == "Space") {
                PlayAudio(starsClip);
            } else if (clickedGameObject.gameObject.tag == "Inhabitant") {
                PlayAudio(mumbleClip);
            } else {
                PlayAudio(clickClip);
            }
        }
    }

    public void ClickSound() {
        PlayAudio(clickClip);
        audioSource.pitch = Random.Range(0.9f, 1f);
        clickedButton = true;
    }

    public void PlayAudio(AudioClip[] audioClip) {
        if (clickedGameObject != null && clickedGameObject.gameObject.tag == "Inhabitant") {
            audioSource.pitch = Random.Range(1, 1.3f);
        } else {
            audioSource.pitch = Random.Range(lowRandomPitch, highRandomPitch);
        }
        audioSource.clip = audioClip[Random.Range(0, audioClip.Length)];
        audioSource.Play();
    }

}
