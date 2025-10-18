
// // ============================================
// // BASE CLASS - Tüm managed objeler bundan türer
// // ============================================
// using UnityEngine;

// public abstract class ManagedBehaviour : MonoBehaviour, ITickable {
//     [SerializeField] private int priority = 50;
//     [SerializeField] private UpdateCategory category = UpdateCategory.Gameplay;
    
//     private bool isActive = true;
    
//     public bool IsActive => isActive && enabled;
//     public int Priority => priority;
    
//     protected virtual void OnEnable() {
//         RegisterToManager();
//     }
    
//     protected virtual void OnDisable() {
//         // UpdateManager.Instance.Unregister(this);
//     }
    
//     private void RegisterToManager() {
//         // switch (category) {
//         //     case UpdateCategory.Gameplay:
//         //         UpdateManager.Instance.RegisterGameplay(this);
//         //         break;
//         //     case UpdateCategory.UI:
//         //         UpdateManager.Instance.RegisterUI(this);
//         //         break;
//         //     case UpdateCategory.Effects:
//         //         UpdateManager.Instance.RegisterEffect(this);
//         //         break;
//         // }
//     }
    
//     // Alt class'lar bunu implemente eder
//     public abstract void Tick(float deltaTime);
    
//     // Manuel pause/resume
//     public void SetActive(bool active) {
//         isActive = active;
//     }
// }
