
using UnityEngine;
using UnityEngine.U2D.Animation;

public enum CandyType { red, yellow, blue }
public enum CandyState { basic, selected }

[RequireComponent(typeof(SpriteDatabaseAnimator))]
public class Tile : MonoBehaviour
{
    public Vector2Int gridPos;
    CandyType candyType;
    CandyState candyState;
    public bool isSelected = false;
    public SpriteDatabaseAnimator spriteDatabaseAnimator;
    public Vector3 worldPos;

    public void Init(CandyType type, CandyState state)
    {
        candyType = type;
        candyState = state;
        spriteDatabaseAnimator.SetCategory($"{candyType}_{candyState}");
    }
    public void SetState(CandyState newState)
    {
        candyState = newState;
        spriteDatabaseAnimator.SetCategory($"{candyType}_{candyState}");
    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, worldPos, Time.deltaTime * 10f);
    }

}


