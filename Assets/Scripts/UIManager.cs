using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour {
    public static UIManager Instance;
    float speed = 5f;

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
        nextLevelText.text = (level + 1).ToString();
    }

    // Update is called once per frame
    public void UpdateLevelProgress() {
        float val = 1f - ((float)Level.Instance.objectsInScene / Level.Instance.totalObjects);
        progressFillImage.DOFillAmount(val, 0.4f);
    }   

    public void ShowLevelCompletedUI() {
        levelCompletedText.DOFade(1f, 0.6f).From(0f);
    }

    public void FadeAtStart() {
        fadePanel.DOFade(0f, 1.3f).From(1f);
    }
}
