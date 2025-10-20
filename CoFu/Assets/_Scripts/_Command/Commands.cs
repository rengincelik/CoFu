using System;
using NUnit.Framework;
using UnityEngine;

// ========== ENUMS ==========

public enum MusicType { GamePlay, NonGamePlay }
public enum SFXType {ButtonClick, Victory, Fail, CoinCollect}

public enum AnimationType { Scale, Fade, Slide }




// ========== INTERFACE ==========
public interface ICommand { void Execute(); }
public interface IUICommand : ICommand { }
public interface IGamePlayCommand : ICommand { }
public interface IJokerCommand : ICommand { }
public interface IAdCommand : ICommand { }

// ========== COMMANDS ==========
public class LevelStartCommand : IGamePlayCommand
{
    GameStateService _gameState;
    GameStateChangedEventSO _stateEvent;
    AudioEventSO _audioEvent;
    
    public LevelStartCommand(
        GameStateService gameState,
        GameStateChangedEventSO stateEvent,
        AudioEventSO audioEvent)
    {
        _gameState = gameState;
        _stateEvent = stateEvent;
        _audioEvent = audioEvent;
    }
    
    public void Execute()
    {
        // // 1. Can kontrol
        // if (_gameState.GetLives() <= 0)
        // {
        //     // PopupEvent.Raise(NoLive); // Popup aç
        //     return;
        // }
        
        // // 2. Can harca
        // _gameState.ConsumeLive();
        
        // // 3. State değiştir
        // _gameState.SetState(GameState.Playing);
        // _stateEvent.Raise(); // Timer başlar, display güncellenir
        
        // 4. Müzik başlat
        _audioEvent.Raise(MusicType.GamePlay);
    }
}

public class LevelCompletedCommand : IGamePlayCommand
{
    GameStateService _gameState;
    GameStateChangedEventSO _stateEvent;
    ResourceChangedEventSO _resourceEvent;
    AudioEventSO _audioEvent;
    PopupEventSO _popupEvent;
    
    public void Execute()
    {
        // // 1. State değiştir
        // _gameState.SetState(GameState.LevelComplete);
        // _stateEvent.Raise();
        
        // // 2. Can kazan
        // _gameState.AddLive();
        // _resourceEvent.Raise();
        
        // // 3. Level artır
        // _gameState.IncrementLevel();
        // _resourceEvent.Raise();
        
        // // 4. Ses + Popup
        // _audioEvent.Raise(AudioType.VictoryMusic);
        // _popupEvent.Raise(PopupType.Win);
    }
}

public class GoToScreenCommand : IUICommand
{
    ScreenViewType _type;
    ScreenEventSO _screenEvent; // ✅ Event kullan
    
    public GoToScreenCommand(ScreenViewType type, ScreenEventSO screenEvent)
    {
        _type = type;
        _screenEvent = screenEvent;
    }
    
    public void Execute()
    {
        _screenEvent.Raise(_type); // ScreenManager dinler
    }
}

public class UseJokerCommand : IJokerCommand
{
    JokerType _jokerType;
    JokerEventSO _jokerEvent;
    
    public UseJokerCommand(JokerType jokerType, JokerEventSO jokerEvent)
    {
        _jokerType = jokerType;
        _jokerEvent = jokerEvent;
    }
    
    public void Execute()
    {
        _jokerEvent.Raise(_jokerType); // JokerManager dinler, coin kontrol + kullan
    }
}

// ========== EVENTS (Base) ==========

public class BaseEventSO : ScriptableObject
{
    private event Action _onRaised;
    public void AddListener(Action listener) => _onRaised += listener;
    public void RemoveListener(Action listener) => _onRaised -= listener;
    public void Raise() => _onRaised?.Invoke();
}

public abstract class BaseEventSO<T> : ScriptableObject
{
    private event Action<T> _onRaised;
    public void AddListener(Action<T> listener) => _onRaised += listener;
    public void RemoveListener(Action<T> listener) => _onRaised -= listener;
    public void Raise(T value) => _onRaised?.Invoke(value);
}

public abstract class BaseEventSO<T1, T2> : ScriptableObject
{
    private event Action<T1, T2> _onRaised;
    public void AddListener(Action<T1, T2> listener) => _onRaised += listener;
    public void RemoveListener(Action<T1, T2> listener) => _onRaised -= listener;
    public void Raise(T1 value1, T2 value2) => _onRaised?.Invoke(value1, value2);
}

// ========== EVENTS (Concrete) ==========
[CreateAssetMenu(menuName = "Events/Command Event")]
public class CommandEventSO : BaseEventSO<ICommand> { }

[CreateAssetMenu(menuName = "Events/Audio Event")]
public class AudioEventSO : BaseEventSO<MusicType> { }
[CreateAssetMenu(menuName = "Events/SFX Event")]
public class SFXEventSO : BaseEventSO<SFXType, float> { }

[CreateAssetMenu(menuName = "Events/Animation Event")]
public class AnimationEventSO : BaseEventSO<AnimationType, PopupType> { } // ✅ İki parametre

[CreateAssetMenu(menuName = "Events/Game State Changed")]
public class GameStateChangedEventSO : BaseEventSO { }

[CreateAssetMenu(menuName = "Events/Resource Changed")]
public class ResourceChangedEventSO : BaseEventSO { }

[CreateAssetMenu(menuName = "Events/Joker Event")] // ✅ Yeni
public class JokerEventSO : BaseEventSO<JokerType> { }

[CreateAssetMenu(menuName = "Events/Screen Event")] // ✅ Yeni
public class ScreenEventSO : BaseEventSO<ScreenViewType> { }

[CreateAssetMenu(menuName = "Events/Popup Event")] // ✅ Yeni
public class PopupEventSO : BaseEventSO<PopupType> { }


// // ========== ENUMS ==========
// public enum AudioType { GamePlay, NonGamePlay }
// public enum SFXType {ButtonClick, Victory, Fail, CoinCollect}
// public enum AnimationType { Scale, Fade, Slide }


// // ========== INTERFACE ==========
// public interface ICommand { void Execute(); }
// public interface IUICommand : ICommand { }
// public interface IGamePlayCommand : ICommand { }
// public interface IJokerCommand : ICommand { }
// public interface IAdCommand:ICommand{}

// // ========== COMMANDS ==========
// public class LevelStartCommand : IGamePlayCommand
// {
//     public void Execute()
//     {

//     }
// }
// public class LevelCompletedCommand : IGamePlayCommand
// {
//     public void Execute()
//     {

//     }
// }
// public class LevelFailedCommand : IGamePlayCommand
// {
//     public void Execute()
//     {

//     }
// }
// public class LevelRestartCommand : IGamePlayCommand
// {
//     public void Execute()
//     {

//     }
// }
// public class LevelContinueWithAdCommand : IGamePlayCommand
// {
//     public void Execute()
//     {

//     }
// }

// public class GoToScreenCommand : IUICommand
// {
//     public ScreenViewType Type;
//     public GoToScreenCommand(ScreenViewType type) { Type = type; }
//     public void Execute()
//     {
//         ScreenManager.Instance.GoToLayer(Type);
//     }
// }

// public class UseJokerCommand : IJokerCommand
// {
//     JokerType _jokerType;
//     public UseJokerCommand(JokerType jokerType){ _jokerType = jokerType; }
//     public void Execute()
//     {
        
//     }
// }

// public class BuyJokerCommand : IJokerCommand
// {
//     JokerType _jokerType;
//     public BuyJokerCommand(JokerType jokerType){ _jokerType = jokerType; }
//     public void Execute()
//     {
        
//     }
// }

// public class WatchAdCommand : IAdCommand
// {
//     public void Execute() { }
// }

// public class ToggleSettingsCommand : ICommand
// {
//     public void Execute(){}
// }

// // ========== EVENTS (Base) ==========

// public class BaseEventSO : ScriptableObject
// {
//     private event Action _onRaised;
//     public void AddListener(Action listener) => _onRaised += listener;
//     public void RemoveListener(Action listener) => _onRaised -= listener;
//     public void Raise() => _onRaised?.Invoke();
// }

// public abstract class BaseEventSO<T> : ScriptableObject
// {
//     private event Action<T> _onRaised;
//     public void AddListener(Action<T> listener) => _onRaised += listener;
//     public void RemoveListener(Action<T> listener) => _onRaised -= listener;
//     public void Raise(T value) => _onRaised?.Invoke(value);
// }

// public abstract class BaseEventSO<T1, T2> : ScriptableObject
// {
//     private event Action<T1, T2> _onRaised;
//     public void AddListener(Action<T1, T2> listener) => _onRaised += listener;
//     public void RemoveListener(Action<T1, T2> listener) => _onRaised -= listener;
//     public void Raise(T1 value1, T2 value2) => _onRaised?.Invoke(value1, value2);
// }

// // ========== EVENTS (Concrete) ==========
// [CreateAssetMenu(menuName = "Events/Command Event")]
// public class CommandEventSO : BaseEventSO<ICommand> { }

// [CreateAssetMenu(menuName = "Events/Audio Event")]
// public class AudioEventSO : BaseEventSO<AudioType> { }
// [CreateAssetMenu(menuName = "Events/SFX Event")]
// public class SFXEventSO : BaseEventSO<SFXType, float> { }
// [CreateAssetMenu(menuName = "Events/Animation Event")]
// public class AnimationEventSO : BaseEventSO { }//parametre eklenecek

// [CreateAssetMenu(menuName = "Events/Game State Changed")]
// public class GameStateChangedEventSO : BaseEventSO { } 

// [CreateAssetMenu(menuName = "Events/Resource Changed")]
// public class ResourceChangedEventSO : BaseEventSO { }

// // Şunlar da lazım:
// [CreateAssetMenu(menuName = "Events/Joker Event")]
// public class JokerEventSO : BaseEventSO<JokerType> { }

// [CreateAssetMenu(menuName = "Events/Screen Event")]
// public class ScreenEventSO : BaseEventSO<ScreenViewType> { }

// [CreateAssetMenu(menuName = "Events/Popup Event")]
// public class PopupEventSO : BaseEventSO<PopupType> { }
