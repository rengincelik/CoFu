// 1. Game Flow Command
using UnityEngine;

public class ChangeGameStateCommand : ICommand
{
    private GameState _newState;
    
    public ChangeGameStateCommand(GameState state)
    {
        _newState = state;
    }
    
    public void Execute()
    {
        
    }
}

// 2. Level Command
public class LevelCommand : ICommand
{
    private LevelAction _action;
    
    public LevelCommand(LevelAction action)
    {
        _action = action;
    }
    
    public void Execute()
    {
        // switch (_action)
        // {
        //     case LevelAction.Start:
        //         if (GameStateService.Instance.GetLives() <= 0)
        //         {
        //             UIService.Instance.ShowAddLivePopup();
        //             return;
        //         }
        //         GameStateService.Instance.ConsumeLive();
        //         GameStateService.Instance.SetGameState(GameState.LevelStart);
        //         // Level load...
        //         GameStateService.Instance.SetGameState(GameState.Playing);
        //         break;

        //     case LevelAction.Restart:
        //         if (GameStateService.Instance.GetLives() <= 0)
        //         {
        //             UIService.Instance.ShowAddLivePopup();
        //             return;
        //         }
        //         GameStateService.Instance.ConsumeLive();
        //         // Restart logic...
        //         GameStateService.Instance.SetGameState(GameState.Playing);
        //         break;

        //     case LevelAction.Next:
        //         GameStateService.Instance.IncrementLevel();
        //         CommandExecutor.Instance.ExecuteCommand(new LevelCommand(LevelAction.Start));
        //         break;
        // }
    
    }
}


// 3. Joker Purchase Command
public class PurchaseJokerCommand : ICommand
{
    private JokerType _jokerType;
    private int _cost;
    
    public PurchaseJokerCommand(JokerType type, int cost)
    {
        _jokerType = type;
        _cost = cost;
    }
    
    public void Execute()
    {
        // int currentCoins = GameStateService.Instance.GetCoins();
        
        // if (currentCoins >= _cost)
        // {
        //     // Coin var, direkt al
        //     GameStateService.Instance.SpendCoins(_cost);
        //     JokerService.Instance.UseJoker(_jokerType); // Mevcut handler'ı çağır
        // }
        // else
        // {
        //     // Coin yok, reklam button göster
        //     UIService.Instance.ShowJokerAdButton(_jokerType);
        // }
    }
}

// 4. Watch Ad For Joker Command
public class WatchAdForJokerCommand : ICommand
{
    private JokerType _jokerType;
    
    public WatchAdForJokerCommand(JokerType type)
    {
        _jokerType = type;
    }
    
    public void Execute()
    {
        // GameStateService.Instance.SetGameState(GameState.WaitingForAd);
        
        // AdService.Instance.ShowRewardedAd(() =>
        // {
        //     // Reklam bitti
        //     JokerService.Instance.UseJoker(_jokerType);
        //     GameStateService.Instance.SetGameState(GameState.Playing);
        // });
    }
}

// 5. Continue Level Command (Fail'den sonra reklam)
public class ContinueLevelCommand : ICommand
{
    private ContinueType _continueType;
    
    public ContinueLevelCommand(ContinueType type)
    {
        _continueType = type;
    }
    
    public void Execute()
    {
        // GameStateService.Instance.SetGameState(GameState.WaitingForAd);
        
        // AdService.Instance.ShowRewardedAd(() =>
        // {
        //     // Reklam bitti
        //     switch(_continueType)
        //     {
        //         case ContinueType.AddTime:
        //             GameStateService.Instance.AddTime(30f);
        //             break;
        //         case ContinueType.AddMove:
        //             GameStateService.Instance.AddMoves(5);
        //             break;
        //     }
            
        //     UIService.Instance.HideFailPopup();
        //     GameStateService.Instance.SetGameState(GameState.Playing);
        // });
    }
}


// 6. Add Live Command
public class AddLiveCommand : ICommand
{
    public void Execute()
    {
        // GameStateService.Instance.SetGameState(GameState.WaitingForAd);
        
        // AdService.Instance.ShowRewardedAd(() =>
        // {
        //     // Reklam bitti
        //     GameStateService.Instance.AddLive();
        //     UIService.Instance.HideAddLivePopup();
            
        //     // Level başlatmayı dene
        //     CommandExecutor.Instance.ExecuteCommand(new LevelCommand(LevelAction.Start));
        // });
    }
}

// 7. Resource Command
public class ModifyResourceCommand : ICommand
{
    private ResourceType _type;
    private int _amount;
    
    public ModifyResourceCommand(ResourceType type, int amount)
    {
        _type = type;
        _amount = amount;
    }
    
    public void Execute()
    {
        // switch(_type)
        // {
        //     case ResourceType.Coin:
        //         GameStateService.Instance.AddCoins(_amount);
        //         break;
        //     case ResourceType.Live:
        //         GameStateService.Instance.AddLive();
        //         break;
        // }
    }
}


// 8. Show Popup Command
public class ShowPopupCommand : ICommand
{
    private PopupType _popupType;
    
    public ShowPopupCommand(PopupType type)
    {
        _popupType = type;
    }
    
    public void Execute()
    {
        // switch(_popupType)
        // {
        //     // case PopupType.Settings:
        //     //     UIService.Instance.ShowSettingsPopup();
        //     //     break;
        //     // case PopupType.Menu:
        //     //     UIService.Instance.ShowMenuPopup();
        //     //     break;
        // }
    }
}

// 9. Toggle Settings Command
public class ToggleSettingCommand : ICommand
{
    private SettingType _settingType;
    private bool _value;
    
    public ToggleSettingCommand(SettingType type, bool value)
    {
        _settingType = type;
        _value = value;
    }
    
    public void Execute()
    {
        // switch(_settingType)
        // {
        //     case SettingType.Sound:
        //         AudioService.Instance.SetSoundEnabled(_value);
        //         PlayerPrefs.SetInt("Sound", _value ? 1 : 0);
        //         break;
        //     case SettingType.Music:
        //         AudioService.Instance.SetMusicEnabled(_value);
        //         PlayerPrefs.SetInt("Music", _value ? 1 : 0);
        //         break;
        //     case SettingType.Vibration:
        //         HapticService.Instance.SetEnabled(_value);
        //         PlayerPrefs.SetInt("Vibration", _value ? 1 : 0);
        //         break;
        // }
        // PlayerPrefs.Save();
    }
}


// 10. Save Game Command
public class SaveGameCommand : ICommand
{
    public void Execute()
    {
        // GameStateData data = GameStateService.Instance.GetData();
        // PlayerPrefs.SetInt("Level", data.currentLevel);
        // PlayerPrefs.SetInt("Coins", data.coins);
        // PlayerPrefs.SetInt("Lives", data.lives);
        // PlayerPrefs.Save();
    }
}

// 11. Load Game Command
public class LoadGameCommand : ICommand
{
    public void Execute()
    {
        int level = PlayerPrefs.GetInt("Level", 1);
        int coins = PlayerPrefs.GetInt("Coins", 0);
        int lives = PlayerPrefs.GetInt("Lives", 3);
        
        // GameStateService.Instance.LoadGame(level, coins, lives);
    }
}

// 12. Audio Command
public class PlayAudioCommand : ICommand
{
    private AudioType _audioType;
    
    public PlayAudioCommand(AudioType type)
    {
        _audioType = type;
    }
    
    public void Execute()
    {
        // AudioService.Instance.Play(_audioType);
    }
}

