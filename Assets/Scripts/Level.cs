using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour {
    public static Level Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }


    public int objectsInScene;
    public int totalObjects;

    [SerializeField] ParticleSystem winFx;
    [SerializeField] Transform objectParent;

    [SerializeField] Material tableMaterial;
    [SerializeField] Image progressFillImage;
    [SerializeField] Image progressBackImage;

    [SerializeField] Color tableColor;
    [SerializeField] Color progressFillImageColor;
    [SerializeField] Color progressBackImageColor;

    [SerializeField] Color cameraColor;
    

    // Start is called before the first frame update
    void Start() {
        CountObjects();
        UpdateLevelColors();
    }

    // Update is called once per frame
    void CountObjects() {
        totalObjects = objectParent.childCount;
        objectsInScene = totalObjects;
    }

    public void PlayWinFx() {
        winFx.Play();
    }

    public void LoadNextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void UpdateLevelColors() {
        tableMaterial.color = tableColor;
        progressFillImage.color = progressFillImageColor;
        progressBackImage.color = progressBackImageColor;

        Camera.main.backgroundColor = cameraColor;
    }

    private void OnValidate() {
        UpdateLevelColors();
    }
}
