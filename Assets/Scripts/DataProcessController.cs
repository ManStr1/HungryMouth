using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataProcessController : MonoBehaviour {

    public bool[] activatedLevels;
    public bool[] activatedSkins;
    public bool[] rewardedLevels;
    public int indexOfActiveSkin;
    public int numberOfLevels = 0;
    public int money = 0;
    

    public static DataProcessController Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }

        //SaveSystem.ClearLevelData();
        
        numberOfLevels = SceneManager.sceneCountInBuildSettings - 1;

        if (SaveSystem.LoadLevel() == null) {
            activatedLevels = new bool[numberOfLevels];
            activatedSkins = new bool[LevelBarMenu.Instance.skins.Length];
            rewardedLevels = new bool[numberOfLevels];
            activatedSkins[0] = true;
            activatedLevels[0] = true;
            indexOfActiveSkin = 0;
            money = 100;
            SaveSystem.SaveLevel(this);
        } else {
            LevelData data = SaveSystem.LoadLevel();
            activatedLevels = data.activatedLevels;
            activatedSkins = data.activatedSkins;
            indexOfActiveSkin = data.indexOfActiveSkin;
            money = data.money;
            rewardedLevels = data.rewardedLevels;
        }
    }

    public int LoadLastOpenLevel() {
        int index = 0;
        for (int i = 0; i < activatedLevels.Length; i++) {
            if (activatedLevels[i]) index = i;
        }
        return index + 1;
    }

    public bool isLevelUnlocked(int level) {
        return activatedLevels[level];
    }

    public void UpdateSkin(int skin) {
        if (activatedSkins[skin] == true) {
            indexOfActiveSkin = skin;
            SaveSystem.SaveLevel(this);
        } else {
            if (money >= 100) {
                activatedSkins[skin] = true;
                indexOfActiveSkin = skin;
                money -= 100;
                SaveSystem.SaveLevel(this);
            }
        }
    }

    public void UnlockNextLevel(int sceneIndex, int reward) {
        // Текущий уровень под 1 индексом, но по сути он 0, значит нужно вызвать для 1 (на самом деле 2 ур разблокирование)
        if (sceneIndex <= activatedLevels.Length) {
            if (sceneIndex < activatedLevels.Length) activatedLevels[sceneIndex] = true;

            if (rewardedLevels[sceneIndex - 1] == false) {
                money += reward;
                LevelBarMenu.Instance.UpdateMoney(money);
                rewardedLevels[sceneIndex - 1] = true;
            }

            SaveSystem.SaveLevel(this);
        }
    }



}
