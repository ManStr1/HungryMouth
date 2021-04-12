using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour {
    public GameObject levelHolder;
    public GameObject levelIcon;
    public GameObject thisCanvas;
    public int numberOfLevels = 100;
    private Rect iconDimensions;

    void Start() {
        iconDimensions = levelIcon.GetComponent<RectTransform>().rect;
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
            GameObject icon = Instantiate(levelIcon);
            icon.transform.SetParent(thisCanvas.transform, false);
            icon.transform.SetParent(parentObject.transform);
            icon.name = "Level" + i;
            icon.GetComponentInChildren<TextMeshProUGUI>().SetText("Level " + i);
        }
    }


}
