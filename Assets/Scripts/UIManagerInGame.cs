using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManagerInGame : MonoBehaviour {
    public static UIManagerInGame Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    [SerializeField] int sceneOffset;
    [SerializeField] TMP_Text nextLevelText;
    [SerializeField] Image progressFillImage;
    [SerializeField] TMP_Text levelCompletedText;
    [SerializeField] Image fadePanel;

    // Start is called before the first frame update
    void Start() {
        FadeAtStart();
        progressFillImage.fillAmount = 0f;
        SetLevelProgressText();
    }

    void SetLevelProgressText() {
        int level = SceneManager.GetActiveScene().buildIndex + sceneOffset;
        nextLevelText.text = (level).ToString();
    }

    // Update is called once per frame
    public void UpdateLevelProgress() {
        float val = 1f - ((float)LevelGameplay.Instance.objectsInScene / LevelGameplay.Instance.totalObjects);
        progressFillImage.DOFillAmount(val, 0.4f);
    }   

    public void ShowLevelCompletedUI() {
        levelCompletedText.DOFade(1f, 0.6f).From(0f);
    }

    public void FadeAtStart() {
        fadePanel.DOFade(0f, 1.3f).From(1f);
    }
}
