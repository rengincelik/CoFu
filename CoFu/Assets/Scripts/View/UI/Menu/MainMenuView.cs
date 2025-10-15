using UnityEngine;

public class MainMenuView : MonoBehaviour
{
    
    
    // MainMenuView.cs - Level seçimi
    public void OnLevel1Clicked()
    {
        LevelConfig level = Resources.Load<LevelConfig>("Levels/Level_01_Tutorial");
        new StartGameCommand(level).Execute();
    }


}
