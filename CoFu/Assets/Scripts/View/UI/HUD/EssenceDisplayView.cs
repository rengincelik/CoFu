
// ============================================
// ÖRNEK 5: UI Display - NORMAL + Event
// (Sadece değer değişince günceller)
// ============================================
using UnityEngine;

public class EssenceDisplayView : MonoBehaviour {
    [SerializeField] private TMPro.TextMeshProUGUI text;
    
    // ❌ Update() yok - Event kullan
    
    void OnEnable() {
        EconomyService.Instance.OnEssenceChanged += UpdateDisplay;
        UpdateDisplay(EconomyService.Instance.WhiteEssence);
    }
    
    void OnDisable() {
        EconomyService.Instance.OnEssenceChanged -= UpdateDisplay;
    }
    
    void UpdateDisplay(int amount) {
        text.text = amount.ToString();
    }
}

