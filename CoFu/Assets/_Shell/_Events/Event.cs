// ========== EVENTS (Base) ==========

using System;
using DG.Tweening;
using UnityEngine;

public abstract class BaseEventSO : ScriptableObject
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
public class CommandEventSO : BaseEventSO<IUseCase> { }

[CreateAssetMenu(menuName = "Events/Music Event")]
public class MusicEventSO : BaseEventSO<AudioClip, bool> { }
[CreateAssetMenu(menuName = "Events/SFX Event")]
public class SFXEventSO : BaseEventSO<AudioClip> { }

[CreateAssetMenu(menuName = "Events/Game State Changed")]
public class GameStateChangedEventSO : BaseEventSO { }
[CreateAssetMenu(menuName = "Events/Settings Changed")]
public class SettingsChangedEventSO : BaseEventSO { }
[CreateAssetMenu(menuName = "Events/Joker Event")] 
public class JokerEventSO : BaseEventSO { }

[CreateAssetMenu(menuName ="Events/Sequence event")]
public class PlaySequenceEventSO : BaseEventSO { }
[CreateAssetMenu(menuName = "Events/Ad Event")]
public class AdEventSO : BaseEventSO<AdType> { }
[CreateAssetMenu(menuName = "Events/Coin Changed Event")]
public class CurrencyChangedEventSO : BaseEventSO<Currency> { }
[CreateAssetMenu(menuName = "Events/Live Changed Event")]
public class LifeChangedEventSO : BaseEventSO<Life> { }





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


