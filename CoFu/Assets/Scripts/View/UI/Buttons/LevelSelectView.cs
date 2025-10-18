using UnityEngine;
using UnityEngine.UI;
using TMPro;

// LevelSelectView.cs
public class LevelButtonView : ButtonHandler
{
    [SerializeField] private int levelNumber;
    [SerializeField] private Button button;
    [SerializeField] private GameObject lockIcon;
    [SerializeField] private TextMeshProUGUI highScoreText;

    public void OnClicked()
    {
        LevelConfig level = Resources.Load<LevelConfig>($"Levels/Level_{levelNumber:00}");
        new StartGameCommand(level).Execute();
    }
}
