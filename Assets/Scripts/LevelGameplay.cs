using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelGameplay : MonoBehaviour {
    // Сам уровень
    public static LevelGameplay Instance;

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

    // Разблокирование следующего уровня
    public void LoadNextLevel() {
        // Текущий уровень под 1 индексом, но по сути он 0, значит нужно вызвать для 1 (на самом деле 2 ур разблокирование)
        if (SceneManager.GetActiveScene().buildIndex < LevelBarMenu.Instance.activatedLevels.Length) {
            LevelBarMenu.Instance.activatedLevels[SceneManager.GetActiveScene().buildIndex] = true;
            SaveSystem.SaveLevel(LevelBarMenu.Instance);
        }
        
        SceneManager.LoadScene(0);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
