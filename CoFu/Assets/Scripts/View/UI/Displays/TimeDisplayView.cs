using TMPro;
using UnityEngine;

// // public class TimeDisplayView : MonoBehaviour
// // {
// //     // [SerializeField] private TMP_Text timeText;
// //     // [SerializeField] private Image fillImage;

// //     // private void Update()
// //     // {
// //     //     // LevelService'den polling (performans OK, 60fps'de bile)
// //     //     var remaining = LevelService.Instance.GetRemainingTime();
// //     //     timeText.text = remaining.ToString("F1");
// //     //     fillImage.fillAmount = remaining / LevelService.Instance.TimeLimit;

// //     //     // Kritik süre animasyonu
// //     //     if (remaining < 10f)
// //     //         PlayWarningPulse();
// //     // }
// // }

// // ============================================
// // 2. TIMER DISPLAY (Dinamik - Sadece gerekirse göster)
// // ============================================
// public class TimeDisplayView : MonoBehaviour {
//     [SerializeField] private TMPro.TextMeshProUGUI timeText;
//     [SerializeField] private GameObject timerPanel;

//     void Start() {
//         // LevelService.Instance.OnTimeChanged += UpdateTime;

//         // // Level config'e göre timer'ı göster/gizle
//         // LevelConfig level = LevelService.Instance.currentLevel;
//         // bool showTimer = level.winCondition == WinConditionType.TimeBasedSurvival ||
//         //                  level.winCondition == WinConditionType.Hybrid;

//         // timerPanel.SetActive(showTimer);
//     }

//     void UpdateTime(float remainingTime) {
//         // int minutes = Mathf.FloorToInt(remainingTime / 60f);
//         // int seconds = Mathf.FloorToInt(remainingTime % 60f);
//         // timeText.text = $"{minutes:00}:{seconds:00}";

//         // // Son 10 saniye kırmızı yap
//         // if (remainingTime <= 10f) {
//         //     timeText.color = Color.red;
//         // }
//     }
// }

// ============================================
// 3. TIME DISPLAY UI - Managed Version
// ============================================

public class TimeDisplayView : ManagedBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;


    public override void Tick(float deltaTime)
    {
        // Her frame LevelService'ten zaman oku
        // float time = LevelService.Instance.RemainingTime;

        // int minutes = Mathf.FloorToInt(time / 60f);
        // int seconds = Mathf.FloorToInt(time % 60f);
        // timeText.text = $"{minutes:00}:{seconds:00}";

        // // Son 10 saniye kırmızı
        // if (time <= 10f && time > 0f)
        // {
        //     timeText.color = Color.Lerp(Color.white, Color.red, 1f - (time / 10f));
        // }
    }
}
