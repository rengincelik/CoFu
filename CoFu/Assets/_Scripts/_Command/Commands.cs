using System;
using DG.Tweening;
using NUnit.Framework;
using UnityEngine;


public enum MusicType { GamePlay, NonGamePlay }
public enum SFXType {ButtonClick, Victory, Fail, CoinCollect}





// ========== INTERFACE ==========
public interface ICommand { void Execute(); }
// public interface IUICommand : ICommand { }
// public interface IGamePlayCommand : ICommand { }
// public interface IJokerCommand : ICommand { }
// public interface IAdCommand : ICommand { }

// ========== COMMANDS ==========
public class LevelStartCommand : ICommand
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
        // _audioEvent.Raise(MusicType.GamePlay);
    }
}

public class LevelCompletedCommand : ICommand
{
    GameStateService _gameState;
    GameStateChangedEventSO _stateEvent;
    // ResourceChangedEventSO _resourceEvent;
    AudioEventSO _audioEvent;
    // PopupEventSO _popupEvent;
    
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

public class GoToScreenCommand : ICommand
{
    ScreenViewType _type;

    public void Execute()
    {
        throw new NotImplementedException();
    }
    // ScreenEventSO _screenEvent; // ✅ Event kullan

    // public GoToScreenCommand(ScreenViewType type, ScreenEventSO screenEvent)
    // {
    //     _type = type;
    //     _screenEvent = screenEvent;
    // }

    // public void Execute()
    // {
    //     _screenEvent.Raise(_type); // ScreenManager dinler
    // }
}

public class UseJokerCommand : ICommand
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
public class ShowAdCommand : ICommand
{
    AdType _adType;
    AdEventSO _adEvent;

    public ShowAdCommand(AdType adType, AdEventSO adEvent)
    {
        _adType = adType;
        _adEvent = adEvent;
    }

    public void Execute()
    {
        _adEvent.Raise(_adType);
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
public class AudioEventSO : BaseEventSO<MusicType, bool> { }

[CreateAssetMenu(menuName = "Events/Game State Changed")]
public class GameStateChangedEventSO : BaseEventSO { }
[CreateAssetMenu(menuName = "Events/Settings Changed")]
public class SettingsChangedEventSO : BaseEventSO { }
[CreateAssetMenu(menuName = "Events/Joker Event")] 
public class JokerEventSO : BaseEventSO<JokerType> { }

[CreateAssetMenu(menuName ="Events/Sequence event")]
public class PlaySequenceEventSO : BaseEventSO<Sequence> { }
[CreateAssetMenu(menuName = "Events/Ad Event")]
public class AdEventSO : BaseEventSO<AdType> { }

//resource yerine Live, coin changed ayrı yaz

// [CreateAssetMenu(menuName = "Events/Resource Changed")]
// public class ResourceChangedEventSO : BaseEventSO { }

// [CreateAssetMenu(menuName = "Events/Animation Event")]
// public class AnimationEventSO : BaseEventSO { } // ✅ İki parametre



// [CreateAssetMenu(menuName = "Events/Screen Event")] // ✅ Yeni
// public class ScreenEventSO : BaseEventSO<ScreenViewType> { }
// [CreateAssetMenu(menuName = "Events/Popup Event")] // ✅ Yeni
// public class PopupEventSO : BaseEventSO<PopupType> { }



[CreateAssetMenu(menuName = "Events/Error Event")]
public class ErrorEventSO : BaseEventSO<string> { }


public enum AdType { Rewarded, Interstitial, Banner }



