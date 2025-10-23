using UnityEngine;

public class AdService : ServiceBase<AdType>
{
    protected override void OnEventRaised(AdType adType)
    {
        Debug.Log("ad event listen");
    }
}