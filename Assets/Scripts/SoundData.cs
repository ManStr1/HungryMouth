using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundData {
    public float musicVolume;

    public SoundData(Music music) {
        musicVolume = music.volume;
    }
}
