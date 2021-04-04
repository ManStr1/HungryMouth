using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UndergroundCollision : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (!Game.isGameOver) {
            string tag = other.tag;

            if (tag.Equals("Food")) {
                Level.Instance.objectsInScene--;
                UIManager.Instance.UpdateLevelProgress();
                Destroy(other.gameObject);

                if (Level.Instance.objectsInScene == 0) {
                    UIManager.Instance.ShowLevelCompletedUI();
                    Level.Instance.PlayWinFx();
                    Invoke("NextLevel", 2f);
                }
            }
            if (tag.Equals("Item")) {
                Game.isGameOver = true;
                Camera.main.transform.DOShakePosition(1f, 0.2f, 20, 90f).
                    OnComplete(() => {
                        Level.Instance.RestartLevel();
                    });
                
            }
        }
    }

    void NextLevel() {
        Level.Instance.LoadNextLevel();
    }
}
