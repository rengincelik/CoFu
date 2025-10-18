
// ============================================
// RESUME DIALOG - Oyun dönüşünde göster
// ============================================
using UnityEngine;
using UnityEngine.UI;

public class ResumeDialogView : MonoBehaviour
{
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitButton;


    private void Show()
    {
        dialogPanel.SetActive(true);
        Time.timeScale = 0f; // Dialog açıkken oyun dursun
    }


}
