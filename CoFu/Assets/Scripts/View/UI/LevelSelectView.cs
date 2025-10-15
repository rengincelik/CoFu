using UnityEngine;
using UnityEngine.UI;
using TMPro;

// LevelSelectView.cs
public class LevelButtonView : MonoBehaviour
{
    [SerializeField] private int levelNumber;
    [SerializeField] private Button button;
    [SerializeField] private GameObject lockIcon;
    [SerializeField] private TextMeshProUGUI highScoreText;

    // void Start() {
    //     bool unlocked = PlayerData.Instance.IsLevelUnlocked(levelNumber);

    //     button.interactable = unlocked;
    //     lockIcon.SetActive(!unlocked);

    //     if (unlocked) {
    //         int highScore = PlayerData.Instance.GetHighScore(levelNumber);
    //         highScoreText.text = highScore > 0 ? $"Best: {highScore}" : "New";
    //     }
    // }

    public void OnClicked()
    {
        LevelConfig level = Resources.Load<LevelConfig>($"Levels/Level_{levelNumber:00}");
        new StartGameCommand(level).Execute();
    }
}
