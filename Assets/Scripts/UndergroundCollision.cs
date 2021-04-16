using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
                        LevelGameplay.Instance.RestartLevel();
                    });
                
            }
        }
    }

    void NextLevel() {
        LevelGameplay.Instance.LoadNextLevel();
    }
}
