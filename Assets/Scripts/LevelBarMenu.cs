using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelBarMenu : MonoBehaviour {
    public GameObject levelHolder;
    public GameObject skinHolder;
    public GameObject levelActivatedButton;
    public GameObject levelNActivatedButton;
    public GameObject skinActivatedButton;
    public GameObject skinNActivatedButton;
    public GameObject thisCanvas;
    public static int numberOfLevels = 0;
    public Sprite[] skins;
    public bool[] activatedLevels;
    public bool[] activatedSkins;
    public int indexOfActiveSkin;
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
            activatedSkins = new bool[skins.Length];
            activatedSkins[0] = true;
            activatedSkins[1] = true;
            activatedLevels[0] = true;
            indexOfActiveSkin = 0;
            //SaveSystem.SaveLevel(this);
        }

        if (SaveSystem.LoadLevel() == null) {
            SaveSystem.SaveLevel(this);
        } else {
            LevelData data = SaveSystem.LoadLevel();
            activatedLevels = data.activatedLevels;
            activatedSkins = data.activatedSkins;
            indexOfActiveSkin = data.indexOfActiveSkin;
        }

        iconDimensions = levelActivatedButton.GetComponent<RectTransform>().rect;
        //levelHolder.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Ceil((float)numberOfLevels / 5) * iconDimensions.width);
        SetUpGrid(levelHolder);
        SetUpGrid(skinHolder);
        
        LoadIcons(numberOfLevels, levelHolder);
        LoadSkinIcons(skins.Length, skinHolder);
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

    void LoadSkinIcons(int numberOfIcons, GameObject parentObject) {
        Debug.Log(numberOfIcons);
        for (int i = 0; i < numberOfIcons; i++) {
            GameObject icon = new GameObject();

            if (activatedSkins[i] == false) {
                icon = Instantiate(skinNActivatedButton);
            } else {
                icon = Instantiate(skinActivatedButton);
            }

            icon.GetComponentInChildren<Image>().sprite = skins[i];
            icon.GetComponentInChildren<Image>().GetComponent<RectTransform>().rect.Set(0, 0, 70, 70);
            icon.transform.SetParent(thisCanvas.transform, false);
            icon.transform.SetParent(parentObject.transform);
            icon.name = "Skin " + (i + 1);
            icon.GetComponentInChildren<TextMeshProUGUI>().SetText("Skin " + (i + 1));
        }
    }

}
