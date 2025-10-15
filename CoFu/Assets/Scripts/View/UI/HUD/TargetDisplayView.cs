
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// ============================================
// 3. TARGET COUNTER (Dinamik - Sadece gerekirse göster)
// ============================================
public class TargetDisplayView : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI targetText;
    [SerializeField] private GameObject targetPanel;
    
    void Start() {
        // LevelService.Instance.OnTargetProgress += UpdateTarget;
        
        // Level config'e göre counter'ı göster/gizle
        // LevelConfig level = LevelService.Instance.currentLevel;
        // bool showTarget = level.winCondition == WinConditionType.CollectTarget ||
        //                   level.winCondition == WinConditionType.Hybrid;
        
        // targetPanel.SetActive(showTarget);
        
        // if (showTarget) {
        //     UpdateTarget(0);
        // }
    }
    
    void UpdateTarget(int collected) {
        // int target = LevelService.Instance.currentLevel.targetWhiteEssence;
        // targetText.text = $"{collected}/{target}";
        
        // // Hedef tamamlandı - yeşil yap
        // if (collected >= target) {
        //     targetText.color = Color.green;
        // }
    }
}

