using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sense : MonoBehaviour {
    public Slider slider;
    public float sense;
    // Start is called before the first frame update

    public static Sense Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    private void Start() {
        sense = DataProcessController.Instance.sense;
    }

    public void LoadSense() {
        sense = DataProcessController.Instance.sense;
    }

    void Update() {
        slider.value = sense;
        
    }

    public void SetSense(float vol) {
        sense = vol;
        //DataProcessController.Instance.sense = vol;
    }
}
