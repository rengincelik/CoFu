
using System;
using UnityEngine;

// ========== ENUMS ==========
public enum AudioType { ButtonClick, Victory, Fail, CoinCollect }
public enum AnimationType { ScaleUp, ScaleDown, FadeIn, FadeOut }
public enum PopupType { Pause, Win, Fail, Settings, Menu, Loading }

    // // ✅ Enum'lar
    // public enum AnimationType
    // {
    //     ShowPopup,
    //     HidePopup,
    //     ShowScreen,
    //     HideScreen
    // }



// ========== INTERFACE ==========
public interface ICommand { void Execute(); }
public interface IUICommand : ICommand { }
public interface IGamePlayCommand : ICommand { }
public interface IJokerCommand : ICommand { }

// ========== COMMANDS ==========
public class ShowPopupCommand : IUICommand
{
    PopupType _type;
    AnimationEventSO _animEvent;
    
    public ShowPopupCommand(PopupType type, AnimationEventSO animEvent)
    {
        _type = type;
        _animEvent = animEvent;
    }
    
    public void Execute()
    {
        // Event fırlatır, AnimationManager popup açar
        _animEvent.Raise(AnimationType.ScaleUp, _type);
    }
}

public class HidePopupCommand : IUICommand
{
    PopupType _type;
    AnimationEventSO _animEvent;
    
    public HidePopupCommand(PopupType type, AnimationEventSO animEvent)
    {
        _type = type;
        _animEvent = animEvent;
    }
    
    public void Execute()
    {
        _animEvent.Raise(AnimationType.ScaleDown, _type);
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
public class AudioEventSO : BaseEventSO<AudioType> { } // AudioClip değil, enum

[CreateAssetMenu(menuName = "Events/Animation Event")]
public class AnimationEventSO : BaseEventSO<AnimationType, PopupType> { } // ICommand değil

[CreateAssetMenu(menuName = "Events/Game State Changed")]
public class GameStateChangedEventSO : BaseEventSO { } 

[CreateAssetMenu(menuName = "Events/Resource Changed")]
public class ResourceChangedEventSO : BaseEventSO { }

