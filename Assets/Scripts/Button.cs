using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour {

    public void LoadLevel() {
        string level = name.Substring(name.IndexOf(' '), name.Length - name.IndexOf(' '));
        int l = int.Parse(level);

        if (DataProcessController.Instance.isLevelUnlocked(l - 1)) {
            SceneManager.LoadScene(name);
        }

    }

    public void SetSkin() {
        string skin = name.Substring(name.IndexOf(' '), name.Length - name.IndexOf(' '));
        int l = int.Parse(skin);

        LevelBarMenu.Instance.ClearAllSkins(l - 1, DataProcessController.Instance.activatedSkins);
        DataProcessController.Instance.UpdateSkin(l - 1);
    }

    public void Menu() {
        SceneManager.LoadScene(0);
    }

    public void Play() {
        SceneManager.LoadScene("Level " + (DataProcessController.Instance.LoadLastOpenLevel()));
    }

    public void QuitGame() {
        Application.Quit();
    }
}
