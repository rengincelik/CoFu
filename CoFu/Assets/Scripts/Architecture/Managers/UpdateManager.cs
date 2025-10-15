using UnityEngine;
using System.Collections.Generic;

// ============================================
// KARAR AĞACI
// ============================================
/*
Her frame çalışması gerekir mi?
    ├─ EVET → IUpdatable ile Register et
    │   └─ Örnekler: Timer, Animation, Cooldown
    │
    └─ HAYIR → Normal MonoBehaviour + Event
        └─ Örnekler: Button, Score, State değişimleri
*/

// // ============================================
// // UPDATE MANAGER - Tek Update() burada
// // ============================================
// public class UpdateManager : Singleton<UpdateManager> {
//     // Farklı kategorilerdeki objeler
//     private List<ITickable> gameplayObjects = new List<ITickable>();
//     private List<ITickable> uiObjects = new List<ITickable>();
//     private List<ITickable> effectObjects = new List<ITickable>();
    
//     // Optimizasyon için cache
//     private bool needsSorting = false;
//     private float cachedDeltaTime;
    
//     // Stats (Debug için)
//     private int totalTickCount = 0;
//     private float averageTickTime = 0f;
    
//     void Update() {
//         // Oyun duraklatıldıysa gameplay objelerini çalıştırma
//         if (GameService.Instance.CurrentState == GameState.Paused) {
//             TickCategory(uiObjects); // UI çalışmaya devam eder
//             return;
//         }
        
//         cachedDeltaTime = Time.deltaTime;
        
//         // Sıralama gerekiyorsa yap (sadece ekleme/çıkarma olduğunda)
//         if (needsSorting) {
//             SortAllCategories();
//             needsSorting = false;
//         }
        
//         // Profiler için
//         #if UNITY_EDITOR
//         UnityEngine.Profiling.Profiler.BeginSample("UpdateManager.TickAll");
//         #endif
        
//         // Kategorileri sırayla çalıştır
//         TickCategory(gameplayObjects);
//         TickCategory(effectObjects);
//         TickCategory(uiObjects);
        
//         #if UNITY_EDITOR
//         UnityEngine.Profiling.Profiler.EndSample();
//         #endif
//     }
    
//     // ============================================
//     // KAYIT İŞLEMLERİ
//     // ============================================
//     public void RegisterGameplay(ITickable obj) {
//         if (!gameplayObjects.Contains(obj)) {
//             gameplayObjects.Add(obj);
//             needsSorting = true;
//         }
//     }
    
//     public void RegisterUI(ITickable obj) {
//         if (!uiObjects.Contains(obj)) {
//             uiObjects.Add(obj);
//             needsSorting = true;
//         }
//     }
    
//     public void RegisterEffect(ITickable obj) {
//         if (!effectObjects.Contains(obj)) {
//             effectObjects.Add(obj);
//             needsSorting = true;
//         }
//     }
    
//     public void Unregister(ITickable obj) {
//         gameplayObjects.Remove(obj);
//         uiObjects.Remove(obj);
//         effectObjects.Remove(obj);
//     }
    
//     // ============================================
//     // TICK İŞLEMLERİ
//     // ============================================
//     private void TickCategory(List<ITickable> objects) {
//         for (int i = objects.Count - 1; i >= 0; i--) {
//             if (objects[i] == null) {
//                 objects.RemoveAt(i);
//                 continue;
//             }
            
//             // Sadece aktif olanları çalıştır
//             if (objects[i].IsActive) {
//                 #if UNITY_EDITOR
//                 totalTickCount++;
//                 float startTime = Time.realtimeSinceStartup;
//                 #endif
                
//                 objects[i].Tick(cachedDeltaTime);
                
//                 #if UNITY_EDITOR
//                 averageTickTime += (Time.realtimeSinceStartup - startTime);
//                 #endif
//             }
//         }
//     }
    
//     private void SortAllCategories() {
//         gameplayObjects.Sort((a, b) => a.Priority.CompareTo(b.Priority));
//         uiObjects.Sort((a, b) => a.Priority.CompareTo(b.Priority));
//         effectObjects.Sort((a, b) => a.Priority.CompareTo(b.Priority));
//     }
    
//     // ============================================
//     // DEBUG
//     // ============================================
//     public void GetStats(out int objectCount, out float avgTime) {
//         objectCount = gameplayObjects.Count + uiObjects.Count + effectObjects.Count;
//         avgTime = totalTickCount > 0 ? averageTickTime / totalTickCount : 0f;
//     }
    
//     [ContextMenu("Log Stats")]
//     private void LogStats() {
//         Debug.Log($"Gameplay: {gameplayObjects.Count} | UI: {uiObjects.Count} | Effects: {effectObjects.Count}");
//         Debug.Log($"Total Ticks: {totalTickCount} | Avg Time: {averageTickTime * 1000f:F2}ms");
//     }
// }




// ============================================
// BASIT UPDATE MANAGER - Sadece gerekenlere
// ============================================
public class UpdateManager : Singleton<UpdateManager> {
    private System.Collections.Generic.List<IUpdatable> activeObjects = 
        new System.Collections.Generic.List<IUpdatable>();
    
    void Update() {
        // Pause kontrolü
        if (GameService.Instance.CurrentState == GameState.Paused) {
            return;
        }
        
        float dt = Time.deltaTime;
        
        // Sadece kayıtlı objeleri çalıştır
        for (int i = activeObjects.Count - 1; i >= 0; i--) {
            if (activeObjects[i] != null) {
                activeObjects[i].OnUpdate(dt);
            } else {
                activeObjects.RemoveAt(i);
            }
        }
    }
    
    public void Register(IUpdatable obj) {
        if (!activeObjects.Contains(obj)) {
            activeObjects.Add(obj);
        }
    }
    
    public void Unregister(IUpdatable obj) {
        activeObjects.Remove(obj);
    }
}




