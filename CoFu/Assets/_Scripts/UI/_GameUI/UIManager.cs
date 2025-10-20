using UnityEngine;
using System.Collections.Generic;
using System;

// ## 🔄 **Akış:**
// ```
// [1] Button Click
//     └─→ CommandEventSO.Raise(ShowPopupCommand)

// [2] CommandExecutor
//     └─→ ShowPopupCommand.Execute()
//         └─→ AnimationEventSO.Raise(ShowPopup, Pause)

// [3] UIManager (Listener)
//     └─→ HandleAnimation(ShowPopup, Pause)
//         └─→ ShowPopupByType(Pause)
//             └─→ ShowPopup<PausePopup>()


namespace GameUI
{
    public class UIManager : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private AnimationEventSO animationEvent;

        [Header("Containers")]
        [SerializeField] private Transform screenContainer;
        [SerializeField] private Transform popupContainer;
        [SerializeField] private CanvasGroup blocker;

        private Dictionary<Type, UIScreen> screenInstances = new Dictionary<Type, UIScreen>();
        private Dictionary<Type, UIPopup> popupInstances = new Dictionary<Type, UIPopup>();

        private Stack<UIPopup> popupHistory = new Stack<UIPopup>();
        private UIScreen currentScreen;
        private List<UIPopup> activePopups = new List<UIPopup>();

        private void OnEnable()
        {
            // animationEvent.AddListener(HandleAnimation);
        }

        private void OnDisable()
        {
            // animationEvent.RemoveListener(HandleAnimation);
        }

        // ✅ Event listener
        private void HandleAnimation(global::AnimationType type1)
        {
            
        }

        

        // ✅ Internal methods (event'ten çağrılır)
        private void ShowPopup<T>(bool animate = true) where T : UIPopup
        {
            T popup = GetOrCreatePopup<T>();

            popupHistory.Push(popup);
            activePopups.Add(popup);
            UpdateBlocker();

            popup.Show(animate);
        }

        private void HidePopup<T>(bool animate = true) where T : UIPopup
        {
            if (popupInstances.TryGetValue(typeof(T), out UIPopup popup))
            {
                HidePopup(popup, animate);
            }
        }

        private void HidePopup(UIPopup popup, bool animate)
        {
            activePopups.Remove(popup);

            if (popupHistory.Count > 0 && popupHistory.Peek() == popup)
            {
                popupHistory.Pop();
            }

            popup.Hide(animate);
            UpdateBlocker();
        }

        private T GetOrCreatePopup<T>() where T : UIPopup
        {
            System.Type type = typeof(T);

            if (!popupInstances.ContainsKey(type))
            {
                T popup = FindFirstObjectByType<T>();
                if (popup == null)
                {
                    Debug.LogError($"Popup of type {type.Name} not found in scene!");
                    return null;
                }
                popupInstances[type] = popup;
            }

            return popupInstances[type] as T;
        }

        private void UpdateBlocker()
        {
            if (blocker == null) return;

            if (activePopups.Count > 0)
            {
                blocker.gameObject.SetActive(true);
                blocker.alpha = 0.5f;
                blocker.blocksRaycasts = true;
            }
            else
            {
                blocker.gameObject.SetActive(false);
                blocker.blocksRaycasts = false;
            }
        }

        // Back button (ESC key)
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (popupHistory.Count > 0)
                {
                    UIPopup popup = popupHistory.Pop();
                    HidePopup(popup, true);
                }
            }
        }
        #region Context Menu (Editor Testing)

        // [ContextMenu("Show Menu Screen")]
        // private void TestShowMenuScreen()
        // {
        //     var menuType = System.Type.GetType("GameUI.MenuScreen");
        //     if (menuType != null)
        //     {
        //         // ShowScreen(menuType, true, true);
        //     }

        // }

        // [ContextMenu("Show Loading Popup")]
        // private void TestShowLoadingPopup()
        // {
        //     var loadingType = System.Type.GetType("GameUI.LoadingPopup");
        //     if (loadingType != null)
        //     {
        //         var method = typeof(UIManager).GetMethod(nameof(ShowPopup)).MakeGenericMethod(loadingType);
        //         method.Invoke(this, new object[] { true, true });
        //     }
        // }
        // Önce metodu implement edelim
        private void HideAllPopups(bool animate = true)
        {
            // popupHistory yığınını temizle ve activePopups listesindeki tüm popup'ları gizle
            while (popupHistory.Count > 0)
            {
                UIPopup popup = popupHistory.Pop();
                activePopups.Remove(popup);
                popup.Hide(animate);
            }
            // Kalan aktif popup'lar için (popupHistory'ye eklenmemiş olanlar)
            foreach (var popup in activePopups.ToArray()) // ToArray() kullanmak, Remove işlemi sırasında koleksiyonun değişmesini engeller
            {
                activePopups.Remove(popup);
                popup.Hide(animate);
            }

            activePopups.Clear();
            UpdateBlocker();
        }


        [ContextMenu("Hide All Popups")]
        private void TestHideAllPopups()
        {
            HideAllPopups(true); // Direkt çağrı
        }

        [ContextMenu("Show Loading Popup")]
        private void TestShowLoadingPopup()
        {
            // 1. Yüklenmesini istediğimiz Popup'ın Tipini (Type) string adından alıyoruz.
            var loadingType = System.Type.GetType("GameUI.LoadingPopup");

            if (loadingType != null)
            {
                // 2. UIManager sınıfındaki "ShowPopup" metodunun genel (generic) tanımını alıyoruz.
                // MakeGenericMethod ile parametre olarak loadingType (örneğimizde LoadingPopup) vererek 
                // ShowPopup<LoadingPopup> metodunu oluşturuyoruz.
                var genericShowPopupMethod = typeof(UIManager)
                    .GetMethod(nameof(ShowPopup), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                if (genericShowPopupMethod != null)
                {
                    var constructedMethod = genericShowPopupMethod.MakeGenericMethod(loadingType);

                    // 3. Metodu çağırıyoruz (Invoke). ShowPopup metodunun tek bir bool parametresi var: animate.
                    // Biz true (animasyonlu) gönderiyoruz.
                    constructedMethod.Invoke(this, new object[] { true });
                }
                else
                {
                    Debug.LogError($"Method 'ShowPopup' not found on UIManager.");
                }
            }
            else
            {
                Debug.LogError("Type 'GameUI.LoadingPopup' not found. Make sure the namespace and class name are correct.");
            }
        }

        // [ContextMenu("Show Menu Screen")]
        // private void TestShowMenuScreen()
        // {
        //     var menuType = System.Type.GetType("GameUI.MenuScreen");
        //     if (menuType != null)
        //     {
        //         // ShowScreen generic metot tanımını al (varsayılan ShowScreen<T>(bool animate, bool hideCurrent) )
        //         var genericShowScreenMethod = typeof(UIManager)
        //             .GetMethod(nameof(ShowScreen), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        //         if (genericShowScreenMethod != null)
        //         {
        //             var constructedMethod = genericShowScreenMethod.MakeGenericMethod(menuType);
        //             // ShowScreen metodunuzun muhtemel parametreleri: animate=true, hideCurrent=true
        //             constructedMethod.Invoke(this, new object[] { true, true });
        //         }
        //         else
        //         {
        //             Debug.LogError($"Method 'ShowScreen' not found on UIManager. Make sure it exists and is public or private non-static.");
        //         }
        //     }
        // }
        
        #endregion
    
    }


}


