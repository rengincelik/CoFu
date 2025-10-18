using UnityEngine;

public class PopupButtons: MonoBehaviour
{
    [SerializeField] GameObject popUp;

    public void ShowPopUp()
    {
        popUp.SetActive(true);
    }
}