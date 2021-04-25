using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public SpriteRenderer holeSkin;

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
        holeSkin.sprite = LevelBarMenu.Instance.skins[DataProcessController.Instance.indexOfActiveSkin];
        CountObjects();
        UpdateLevelColors();
    }

    void CountObjects() {
        totalObjects = objectParent.childCount;
        objectsInScene = totalObjects;
    }

    public void PlayWinFx() {
        winFx.Play();
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
