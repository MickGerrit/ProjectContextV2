using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundChanger : MonoBehaviour {
    public AudioSource audioSourceIn;
    public AudioSource audioSourceOut;
    public AudioClip[] woods;
    public AudioClip[] desert;
    public AudioClip[] ice;
    public AudioClip[] space;
    public int biome;
    public int happyness = 2;
    public float audioTrackTime;
    private int playingAudioSource;
    public float fadeSpeed = 1f;
    public float maxVolume;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        switch (biome) {
            case 1:
            SwitchAudio(woods, happyness);
            break;
            case 2:
            SwitchAudio(desert, happyness);
            break;
            case 3:
            SwitchAudio(ice, happyness);
            break;
            case 0:
            SwitchAudio(space, happyness);
            break;
        }

        audioTrackTime = audioSourceIn.time;

        FadeAudioVolumes();
    }

    void SwitchAudio(AudioClip[] audio, int index) {
        if (audioSourceIn.clip != audio[index]) {
            float audioSourceOutVolume;
            audioSourceOutVolume = audioSourceOut.volume;

            audioSourceOut.clip = audioSourceIn.clip;
            audioSourceOut.volume = audioSourceIn.volume;
            audioSourceOut.Play();
            audioSourceOut.time = audioTrackTime;

            audioSourceIn.volume = audioSourceOutVolume;
            audioSourceIn.clip = audio[index];
            audioSourceIn.Play();
            audioSourceIn.time = audioTrackTime;
        } 
    }

    void FadeAudioVolumes() {
        if (audioSourceIn.volume < maxVolume) {
            audioSourceIn.volume += Time.deltaTime * fadeSpeed;
        } else audioSourceIn.volume = maxVolume;

        if (audioSourceOut.volume > 0) {
            audioSourceOut.volume -= Time.deltaTime * fadeSpeed;
        } else {
            audioSourceOut.volume = 0;
        }
    }

   
}
