

using UnityEngine;

public class GameStateService : ServiceBase
{
    protected override void OnEventRaised()
    {
        Debug.Log("state event dinlendi");
    }
}