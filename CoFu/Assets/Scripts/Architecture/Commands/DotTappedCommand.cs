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

public class DotTappedCommand : ICommand
{
    public bool CanExecute()
    {
        throw new System.NotImplementedException();
    }
    public void Execute()
    {
        
    }
}


