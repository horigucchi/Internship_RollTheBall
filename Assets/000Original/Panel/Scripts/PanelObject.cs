using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelObject : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    /// <summary>
    /// 指定の方向に移動
    /// </summary>
    /// <param name="direction">移動する方向</param>
    /// <param name="callback">移動終了の通知</param>
    /// <returns></returns>
    public IEnumerator TryMoving(Vector2Int direction, System.Action callback = null)
    {
        Vector3 firstPosition = transform.position;
        Vector3 targetPosition = transform.position + new Vector3(direction.x, direction.y);

        const float SPEED = 2f;
        float t = 0;
        while (t < 1.0f)
        {
            t += Time.deltaTime * SPEED;
            Vector3 position = Vector3.Lerp(firstPosition, targetPosition, t);
            transform.position = position;
            yield return null;
        }

        transform.position = targetPosition;
        callback?.Invoke();
        yield return null;
    }
}
