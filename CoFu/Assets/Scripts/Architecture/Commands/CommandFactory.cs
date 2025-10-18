
using UnityEngine;

public static class CommandFactory
{
    // 1. Game Flow
    public static ICommand CreateGameStateCommand(GameState state)
        => new ChangeGameStateCommand(state);

    // 2. Level
    public static ICommand CreateLevelCommand(LevelAction action)
        => new LevelCommand(action);

    // 3. Joker Purchase
    public static ICommand CreatePurchaseJokerCommand(JokerType type, int cost)
        => new PurchaseJokerCommand(type, cost);

    // 4. Watch Ad for Joker
    public static ICommand CreateWatchAdForJokerCommand(JokerType type)
        => new WatchAdForJokerCommand(type);

    // 5. Continue Level
    public static ICommand CreateContinueLevelCommand(ContinueType type)
        => new ContinueLevelCommand(type);

    // 6. Add Live
    public static ICommand CreateAddLiveCommand()
        => new AddLiveCommand();

    // 7. Resource Modify
    public static ICommand CreateModifyResourceCommand(ResourceType type, int amount)
        => new ModifyResourceCommand(type, amount);

    // 8. Popup
    public static ICommand CreateShowPopupCommand(PopupType type)
        => new ShowPopupCommand(type);

    // 9. Settings
    public static ICommand CreateToggleSettingCommand(SettingType type, bool value)
        => new ToggleSettingCommand(type, value);

    // 10. Save / Load
    public static ICommand CreateSaveGameCommand()
        => new SaveGameCommand();

    public static ICommand CreateLoadGameCommand()
        => new LoadGameCommand();

    // 11. Audio
    public static ICommand CreatePlayAudioCommand(AudioType type)
        => new PlayAudioCommand(type);
}



