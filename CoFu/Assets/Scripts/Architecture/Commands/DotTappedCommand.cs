// Tetikleme: ColorDotView üzerinde OnPointerClick
// Parametre: ColorDotView dot, SlotView targetSlot

// Akış:
// 1. CanExecute() kontrolü:
//    ├── Slot dolu mu?
//    ├── Oyun duraklamış mı?
//    └── Geçerli kombinasyon var mı?

// 2. Slot'a yerleştirme animasyonu (DOTween):
//    await dot.transform.DOMove(targetSlot.Position, 0.3f)

// 3. ColorCombinations.TryCombine(slot, dot.ColorType)
// 4. Sonuca göre:
//    ├── Yeni renk → SlotView güncelle
//    ├── White Essence → EconomyService.AddWhiteEssence()
//    └── Invalid → Dot'u geri gönder

// 5. Event fırlat: GameEvents.OnDotCombined


using UnityEngine;

public class DotTappedCommand 
{
    // private ColorDotView dot;
    // private SlotView slot;
    
    // public async UniTask Execute()
    // {
    //     // 1. LOGIC (Core)
    //     var newColor = ColorCombinations.TryCombine(
    //         slot.CurrentColor, 
    //         dot.CurrentColor
    //     );
        
    //     if (newColor.IsNull) return; // Invalid combination
        
    //     // 2. STATE UPDATE
    //     slot.UpdateColor(newColor);
    //     dot.gameObject.SetActive(false); // Dot'u gizle
        
    //     // 3. VFX (Opsiyonel - toggle edilebilir!)
    //     if (GameSettings.EnableVFX)
    //     {
    //         await MergeEffectController.Instance.PlayMergeEffect(
    //             dot.transform.position,
    //             slot.transform.position,
    //             dot.CurrentColor,
    //             slot.PreviousColor,
    //             newColor
    //         );
    //     }
        
    //     // 4. GAME LOGIC
    //     if (newColor.IsWhite)
    //     {
    //         EconomyService.Instance.AddWhiteEssence(1);
    //     }
    // }
}

