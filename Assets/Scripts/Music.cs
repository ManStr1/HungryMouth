using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour {
    public Slider slider;
    public float volume = 0;
    private AudioSource audioSource;
    

    public static Music Instance;
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }


    // Start is called before the first frame update
    void Start() {
        audioSource = GetComponent<AudioSource>();

        if (SaveSystem.LoadSound() == null) {
            volume = 0.5f;
            SaveSystem.SaveSound(this);
            slider.value = volume;
        } else {
            SoundData data = SaveSystem.LoadSound();
            volume = data.musicVolume;
            slider.value = volume;
        }
    }

    // Update is called once per frame
    void Update() {
        audioSource.volume = volume;
    }

    public void SetVolume(float vol) {
        volume = vol;
    }
}
