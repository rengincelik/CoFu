using UnityEngine;
using UnityEngine.UI;

// ============================================
// PAUSE MENU VIEW
// ============================================
public class PauseMenuView : MonoBehaviour {
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;


    private void Show() {
        pausePanel.SetActive(true);
    }

    private void Hide() {
        pausePanel.SetActive(false);
    }



}
