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



// ============================================
// 2. COLOR DOT - Managed Version
// ============================================
using UnityEngine;

// public class ColorDotView : ManagedBehaviour {
//     [Header("Movement")]
//     [SerializeField] private float floatSpeed = 1f;
//     [SerializeField] private float floatAmplitude = 0.2f;
    
//     private Vector3 startPosition;
//     private float timeOffset;
    
//     void Start() {
//         startPosition = transform.position;
//         timeOffset = Random.Range(0f, Mathf.PI * 2f);
        
//         // Category = Gameplay (Inspector'dan ayarlanmış)
//         // Priority = 50 (default)
//     }
    
//     // ❌ ESKI: void Update() { FloatAnimation(); }
//     // ✅ YENİ:
//     public override void Tick(float deltaTime) {
//         FloatAnimation(deltaTime);
//     }
    
//     private void FloatAnimation(float deltaTime) {
//         float yOffset = Mathf.Sin((Time.time + timeOffset) * floatSpeed) * floatAmplitude;
//         transform.position = startPosition + Vector3.up * yOffset;
//     }
    
//     // Input OnPointerDown() değişmez - Input ayrı sistem
// }


// ============================================
// ÖRNEK 2: Dot View - MANAGED
// (Animasyon her frame çalışır)
// ============================================
public class ColorDotView : MonoBehaviour, IUpdatable {
    private float timeOffset;
    
    void OnEnable() {
        UpdateManager.Instance.Register(this);
        timeOffset = Random.value * 10f;
    }
    
    void OnDisable() {
        UpdateManager.Instance.Unregister(this);
    }
    
    public void OnUpdate(float deltaTime) {
        // Float animation
        float yOffset = Mathf.Sin(Time.time + timeOffset) * 0.2f;
        transform.position += Vector3.up * yOffset * deltaTime;
    }
}
