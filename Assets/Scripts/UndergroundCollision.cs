using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UndergroundCollision : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (!Game.isGameOver) {
            string tag = other.tag;

            if (tag.Equals("Food")) {
                LevelGameplay.Instance.objectsInScene--;
                UIManagerInGame.Instance.UpdateLevelProgress();
                Destroy(other.gameObject);

                if (LevelGameplay.Instance.objectsInScene == 0) {
                    UIManagerInGame.Instance.ShowLevelCompletedUI();
                    LevelGameplay.Instance.PlayWinFx();
                    Invoke("NextLevel", 2f);
                }
            }
            if (tag.Equals("Item")) {
                Game.isGameOver = true;
                Camera.main.transform.DOShakePosition(1f, 0.2f, 20, 90f).
                    OnComplete(() => {
                        RestartLevel();
                    });
                
            }
        }
    }

    void NextLevel() {
        int reward = LevelGameplay.Instance.totalObjects * 15;
        DataProcessController.Instance.UnlockNextLevel(SceneManager.GetActiveScene().buildIndex, reward);
        //SceneManager.LoadScene(0);
        if (SceneManager.sceneCountInBuildSettings == SceneManager.GetActiveScene().buildIndex + 1) {
            SceneManager.LoadScene(0);
        } else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
    }

    void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
