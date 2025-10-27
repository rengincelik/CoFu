using UnityEngine;

public class AdManager :MonoBehaviour
{
    [SerializeField] private AdEventSO adEventSO;

    void OnEnable()
    {
        adEventSO?.AddListener(OnEventRaised);
    }

    void OnDisable()
    {
        adEventSO?.RemoveListener(OnEventRaised);
    }

    void OnEventRaised(AdType adType)
    {
        Debug.Log(" show ad and give reward");
    }
    //add başarılıysa ödül verilecek o kısım eklenecek.

    //muhtemelen reklam gösterme işlemlerini service e taşıyabiliriz veya ödül verme işlemlerini de
    
}