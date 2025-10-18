public enum GameState
{
    None,
    Loading,
    LevelStart,
    Playing,
    WaitingForAd,
    Paused,
    Win,
    Fail
}

public enum LevelAction 
{ 
    Start, 
    Restart, 
    Next 
}

public enum JokerType 
{ 
    Cleaner, 
    Shuffle, 
    AddTile, 
    CollectWhite, 
    AddTime, 
    AddMove 
}

public enum ContinueType 
{ 
    AddTime, 
    AddMove 
}

public enum ResourceType 
{ 
    Coin, 
    Live 
}

public enum PopupType 
{ 
    Settings, 
    Menu 
}

public enum SettingType 
{ 
    Sound, 
    Music, 
    Vibration 
}

public enum AudioType 
{ 
    ButtonClick, 
    JokerUse, 
    LevelWin, 
    LevelFail, 
    CoinCollect 
}
public enum CurrencyType { Coin, Live }
public enum LevelType { TargetCount, TimeAttack }
public enum DisplayType
{
    Score,
    Coins,
    Jokers
}


// public enum LevelType { TargetCount, TimeAttack }
// // public enum GameState { Playing, Paused, Won, Lost }

// public enum UpdateCategory
// {
//     Gameplay,
//     UI,
//     Effects
// }

// // public enum JokerType
// // {
// //     Cleanup,       // Temizlik Jokeri
// //     ExtraPaint,    // Ek Boya
// //     Shuffle,       // Shuffle
// //     FusionBoost,   // Fusion Boost
// //     Pause          // Pause / Time Stop
// // }
// public enum CurrencyType { Coin, Live }

// // Enum - Inspector'da seç
// public enum ButtonType
// {
//     Pause,
//     Resume,
//     Play,
//     Restart,
//     NextLevel,
//     UseJoker,
//     WatchAd
// }
// public enum DisplayType
// {
//     Score,
//     Coins,
//     Jokers
// }
// public enum GameState
// {
//     MainMenu,      // Ana menü
//     LevelStart,    // Level yükleniyor
//     Playing,       // Oyun oynanıyor
//     Paused,        // Pause popup açık
//     LevelComplete, // Win popup açık
//     LevelFailed,   // Fail popup açık
//     WaitingForAd   // Reklam izleniyor
// }