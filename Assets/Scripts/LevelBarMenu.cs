using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelBarMenu : MonoBehaviour {
    public GameObject levelHolder;
    public GameObject levelActivatedButton;
    public GameObject levelNActivatedButton;
    public GameObject thisCanvas;
    public static int numberOfLevels = 0;
    public bool[] activatedLevels;
    private Rect iconDimensions;

    public static LevelBarMenu Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    void Start() {

        if (numberOfLevels == 0) {
            numberOfLevels = SceneManager.sceneCountInBuildSettings - 1;
            activatedLevels = new bool[numberOfLevels];
            activatedLevels[0] = true;
        }

        if (SaveSystem.LoadLevel() == null) {
            SaveSystem.SaveLevel(this);
        } else {
            LevelData data = SaveSystem.LoadLevel();
            activatedLevels = data.activatedLevels;
        }

        iconDimensions = levelActivatedButton.GetComponent<RectTransform>().rect;
        //levelHolder.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Ceil((float)numberOfLevels / 5) * iconDimensions.width);
        SetUpGrid(levelHolder);
        LoadIcons(numberOfLevels, levelHolder);
    }

    void SetUpGrid(GameObject panel) {
        GridLayoutGroup grid = panel.GetComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(iconDimensions.width, iconDimensions.height);
    }

    void LoadIcons(int numberOfIcons, GameObject parentObject) {
        for (int i = 1; i <= numberOfIcons; i++) {
            // Отображение закрытых и открытых уровней
            // activatedLevels = i - 1 = false так как первый уровень в 0 ячейке
            GameObject icon = new GameObject();
            if (activatedLevels[i - 1] == false) {
                icon = Instantiate(levelNActivatedButton);
            } else {
                icon = Instantiate(levelActivatedButton);
            }
            
            icon.transform.SetParent(thisCanvas.transform, false);
            icon.transform.SetParent(parentObject.transform);
            icon.name = "Level " + i;
            icon.GetComponentInChildren<TextMeshProUGUI>().SetText("Level " + i);

        }
    }

}
