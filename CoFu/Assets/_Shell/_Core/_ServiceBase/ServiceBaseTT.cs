// using UnityEngine;

// public abstract class ServiceBase<T1, T2> : MonoBehaviour
// {
//     [SerializeField] private BaseEventSO<T1, T2> baseEventSO;

//     protected virtual void OnEnable()
//     {
//         baseEventSO?.AddListener(OnEventRaised);
//     }

//     protected virtual void OnDisable()
//     {
//         baseEventSO?.RemoveListener(OnEventRaised);
//     }

//     protected abstract void OnEventRaised(T1 value1, T2 value2);
// }
