using UnityEngine;

public class TimeDisplayView : MonoBehaviour
{
    // [SerializeField] private TMP_Text timeText;
    // [SerializeField] private Image fillImage;

    // private void Update()
    // {
    //     // LevelService'den polling (performans OK, 60fps'de bile)
    //     var remaining = LevelService.Instance.GetRemainingTime();
    //     timeText.text = remaining.ToString("F1");
    //     fillImage.fillAmount = remaining / LevelService.Instance.TimeLimit;

    //     // Kritik süre animasyonu
    //     if (remaining < 10f)
    //         PlayWarningPulse();
    // }
}
