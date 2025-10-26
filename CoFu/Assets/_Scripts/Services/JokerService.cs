using UnityEngine;

public class JokerService : ServiceBase
{
    protected override void OnEventRaised()
    {
        Debug.Log("joker event listen");
    }
}