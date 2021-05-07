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
    public TMP_Text moneyText;
    public Sprite[] skins;
    private Rect iconDimensions;
    
    public static LevelBarMenu Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }

    }

    void Start() {
        DataProcessController.Instance.LoadDataOfWorld(skins.Length);
        moneyText.text = DataProcessController.Instance.money + "";

        iconDimensions.width = levelHolder.GetComponent<RectTransform>().rect.width / 5;
        iconDimensions.height = levelHolder.GetComponent<RectTransform>().rect.width / 5;

        SetUpGrid(levelHolder);
        SetUpGrid(skinHolder);
        
        LoadIcons(DataProcessController.Instance.numberOfLevels, levelHolder);
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
            if (DataProcessController.Instance.activatedLevels[i - 1] == false) {
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

        for (int i = 0; i < numberOfIcons; i++) {
            GameObject icon = new GameObject();

            if (DataProcessController.Instance.activatedSkins[i] == false) {
                icon = Instantiate(skinNActivatedButton);
                icon.GetComponentInChildren<Image>().color = Color.grey;
            } else {
                icon = Instantiate(skinActivatedButton);
                
            }

            icon.GetComponentInChildren<Image>().sprite = skins[i];
            icon.GetComponentInChildren<Image>().GetComponent<RectTransform>().rect.Set(0, 0, 70, 70);

            if (i == DataProcessController.Instance.indexOfActiveSkin) {
                icon.GetComponentInChildren<Image>().color = Color.yellow;
            }
            
            icon.transform.SetParent(thisCanvas.transform, false);
            icon.transform.SetParent(parentObject.transform);
            icon.name = "Skin " + (i + 1);
            icon.GetComponentInChildren<TextMeshProUGUI>().SetText("Skin " + (i + 1));
        }
    }

    public void ClearAllSkins(int indexOfActSkin, bool[] activatedSkins) {
        Button[] newSkins = skinHolder.GetComponentsInChildren<Button>();
        for (int i = 0; i < activatedSkins.Length; i++) {

            if (activatedSkins[i] == false && DataProcessController.Instance.money >= 700) {
                newSkins[i].GetComponentInChildren<Image>().color = Color.yellow;
                moneyText.text = (DataProcessController.Instance.money - 700) + "";
            } else
            if (activatedSkins[i]) {
                if (i == indexOfActSkin) {
                    newSkins[i].GetComponentInChildren<Image>().color = Color.yellow;
                } else {
                    newSkins[i].GetComponentInChildren<Image>().color = Color.white;
                }
            }
        }
    } 

    public void UpdateMoney(int money) {
        moneyText.text = money + "";
    }

}
