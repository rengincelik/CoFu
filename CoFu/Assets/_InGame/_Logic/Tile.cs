using System.Collections;
using UnityEngine;
using UnityEngine.U2D.Animation;
using VFavorites.Libs;

public class Tile : MonoBehaviour
{
    public Vector2Int gridPos;
    CandyType candyType;
    CandyState candyState;

    public SpriteDatabaseAnimator spriteDatabaseAnimator;

    private bool isMoving = false;

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

    // GridManager'dan çağrılır
    public void DestroyTile()
    {
        gameObject.Destroy();
    }
    public void MoveToPosition(Vector3 targetPos, float duration)
    {
        if (!isMoving)
            StartCoroutine(MoveTo(targetPos, duration));
    }

    IEnumerator MoveTo(Vector3 targetPos, float duration)
    {
        isMoving = true;
        Vector3 start = transform.position;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;

            // Opsiyonel: Ease out
            float eased = 1f - Mathf.Pow(1f - t, 2f);

            transform.position = Vector3.Lerp(start, targetPos, eased);
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }

    public bool IsMoving() => isMoving;
    public CandyType GetCandyType => candyType;
}


// public enum CandyType { red, yellow, blue, lightblue, lightpink, pink, green, orange }
public enum CandyType { red, yellow, blue,  green, orange }

public enum CandyState { basic, selected }



