using UnityEngine;

// ==========================================
// SERVICE BASE TYPES
// ==========================================



public abstract class ServiceBase<T> : MonoBehaviour
{
    [SerializeField] private BaseEventSO<T> baseEventSO;

    protected virtual void OnEnable()
    {
        baseEventSO?.AddListener(OnEventRaised);
    }

    protected virtual void OnDisable()
    {
        baseEventSO?.RemoveListener(OnEventRaised);
    }

    protected abstract void OnEventRaised(T value);
}


