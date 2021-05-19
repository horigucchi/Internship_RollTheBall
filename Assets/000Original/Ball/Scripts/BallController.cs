using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public BallModel BallModel { get; private set; }

    private void Awake()
    {
        BallModel = GetComponent<BallModel>();
    }

    /// <summary>
    /// ボールを移動させる
    /// </summary>
    /// <param name="way">移動する方向</param>
    /// <param name="callback">移動終了の通知</param>
    public void Move(WayPattern way, System.Action callback = null)
    {
        Vector2Int direction = Vector2Int.zero;
        if ((way & WayPattern.Right) != 0) direction += Vector2Int.right;
        if ((way & WayPattern.Down) != 0) direction += Vector2Int.down;
        if ((way & WayPattern.Left) != 0) direction += Vector2Int.left;
        if ((way & WayPattern.Up) != 0) direction += Vector2Int.up;

        if (direction == Vector2Int.zero) return;
        StartCoroutine(BallModel.TryMoving(direction, callback));
    }

    private IEnumerator move(List<WayPattern> ways, System.Action callback)
    {
        foreach (var way in ways)
        {
            Vector2Int direction = Vector2Int.zero;
            if ((way & WayPattern.Right) != 0) direction += Vector2Int.right;
            if ((way & WayPattern.Down) != 0) direction += Vector2Int.down;
            if ((way & WayPattern.Left) != 0) direction += Vector2Int.left;
            if ((way & WayPattern.Up) != 0) direction += Vector2Int.up;

            if (direction == Vector2Int.zero) continue;
            yield return BallModel.TryMoving(direction);
        }
        callback?.Invoke();
        yield return null;
    }

    public void Move(List<WayPattern> ways, System.Action callback = null)
    {
        StartCoroutine(move(ways, callback));
    }

}
