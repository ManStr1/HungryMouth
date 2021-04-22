using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour {

    public void LoadScene() {
        string level = name.Substring(name.IndexOf(' '), name.Length - name.IndexOf(' '));
        int l = int.Parse(level);

        if (LevelBarMenu.Instance.activatedLevels[l - 1] == true) {
            SceneManager.LoadScene(name);
        }

    }

    public void Play() {
        int index = 0;
        for (int i = 0; i < LevelBarMenu.Instance.activatedLevels.Length; i++) {
            if (LevelBarMenu.Instance.activatedLevels[i] == true) index = i;
        }
        SceneManager.LoadScene("Level " + (index + 1));
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void SetSkin() {
        string skin = name.Substring(name.IndexOf(' '), name.Length - name.IndexOf(' '));
        int l = int.Parse(skin);

        if (LevelBarMenu.Instance.activatedSkins[l - 1] == true) {
            LevelBarMenu.Instance.indexOfActiveSkin = l - 1;
            SaveSystem.SaveLevel(LevelBarMenu.Instance);
        }
    }
}
