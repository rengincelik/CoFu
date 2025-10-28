using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Camera mainCamera;
    [SerializeField] GridManager gridManager;
    [SerializeField] InputActionReference pointerPositionAction;
    [SerializeField] InputActionReference pointerPressAction;

    [Header("Settings")]
    [SerializeField] float swipeThreshold = 0.4f;

    private Tile selectedTile;
    private Vector2 pointerStartPos;
    private bool hasSwipeStarted;

    void OnEnable()
    {
        if (pointerPressAction != null)
        {
            pointerPressAction.action.started += OnPointerDown;
            pointerPressAction.action.canceled += OnPointerUp;
            pointerPressAction.action.Enable();
        }

        if (pointerPositionAction != null)
        {
            pointerPositionAction.action.Enable();
        }
    }

    void OnDisable()
    {
        if (pointerPressAction != null)
        {
            pointerPressAction.action.started -= OnPointerDown;
            pointerPressAction.action.canceled -= OnPointerUp;
            pointerPressAction.action.Disable();
        }

        if (pointerPositionAction != null)
        {
            pointerPositionAction.action.Disable();
        }
    }

    void OnPointerDown(InputAction.CallbackContext ctx)
    {
        if (gridManager.isProcessing) return;

        Vector2 screenPos = pointerPositionAction.action.ReadValue<Vector2>();

        Tile tile = RaycastTile(screenPos);

        if (tile != null)
        {
            selectedTile = tile;
            pointerStartPos = screenPos;
            hasSwipeStarted = false;

            selectedTile.SetState(CandyState.selected);
        }
    }

    void Update()
    {
        // Pointer pressed ve tile seçili ise drag kontrol et
        if (selectedTile != null && !hasSwipeStarted && pointerPressAction.action.IsPressed())
        {
            CheckSwipe();
        }
    }

    void CheckSwipe()
    {
        if (gridManager.isProcessing) return;

        Vector2 currentPos = pointerPositionAction.action.ReadValue<Vector2>();
        Vector2 delta = currentPos - pointerStartPos;

        // Threshold kontrolü (pixel cinsinden)
        if (delta.magnitude < swipeThreshold * 100f) return;

        // Yön hesapla
        Vector2Int direction = CalculateDirection(delta);

        // Swap başlat
        hasSwipeStarted = true;
        gridManager.RequestSwap(selectedTile, direction);

        selectedTile = null; // Seçimi kaldır
    }

    void OnPointerUp(InputAction.CallbackContext ctx)
    {
        // Eğer swap başlamadıysa tile seçimini iptal et
        if (selectedTile != null && !hasSwipeStarted)
        {
            selectedTile.SetState(CandyState.basic);
        }

        selectedTile = null;
        hasSwipeStarted = false;
    }

    Tile RaycastTile(Vector2 screenPos)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPos);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null)
        {
            return hit.collider.GetComponent<Tile>();
        }

        return null;
    }

    Vector2Int CalculateDirection(Vector2 delta)
    {
        // En dominant axis'i bul
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            // Yatay
            return delta.x > 0 ? Vector2Int.right : Vector2Int.left;
        }
        else
        {
            // Dikey
            return delta.y > 0 ? Vector2Int.up : Vector2Int.down;
        }
    }
}

