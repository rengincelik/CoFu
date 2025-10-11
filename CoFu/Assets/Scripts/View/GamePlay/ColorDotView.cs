// public class ColorDotView : MonoBehaviour, IPointerClickHandler
// {
//     private ColorType colorType;
//     private MaterialPropertyBlock propBlock;
//     private SpriteRenderer spriteRenderer;

//     // Factory tarafından initialize edilir
//     public void Initialize(ColorType color)
//     {
//         this.colorType = color;
//         UpdateVisual();
//     }

//     public void OnPointerClick(PointerEventData eventData)
//     {
//         var targetSlot = SlotManager.Instance.GetNearestEmptySlot();
//         var command = new DotTappedCommand(this, targetSlot);

//         if (command.CanExecute())
//             command.Execute().Forget(); // Fire-and-forget
//     }

//     private void UpdateVisual()
//     {
//         // MaterialPropertyBlock ile GPU Instancing
//         propBlock ??= new MaterialPropertyBlock();
//         spriteRenderer.GetPropertyBlock(propBlock);
//         propBlock.SetColor("_BaseColor", GetColorValue(colorType));
//         spriteRenderer.SetPropertyBlock(propBlock);
//     }
// }
// public class ColorDotView : MonoBehaviour
// {
//     private ColorVector currentColor;
//     private SpriteRenderer spriteRenderer;
    
//     // Core mechanic (VFX yok)
//     public void UpdateColor(ColorVector newColor)
//     {
//         currentColor = newColor;
//         spriteRenderer.color = newColor.ToUnityColor();
//     }
    
//     // Input handler
//     public void OnPointerClick(PointerEventData eventData)
//     {
//         var command = new DotTappedCommand(this, targetSlot);
//         command.Execute().Forget();
//     }
// }
