using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelObject : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [field: SerializeField]
    public WayPattern WayPattern { get; private set; }

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    /// <summary>
    /// w’è‚Ì•ûŒü‚ÉˆÚ“®
    /// </summary>
    /// <param name="direction">ˆÚ“®‚·‚é•ûŒü</param>
    /// <param name="callback">ˆÚ“®I—¹‚Ì’Ê’m</param>
    /// <returns></returns>
    public IEnumerator TryMoving(Vector3 direction, System.Action callback = null)
    {
        Vector3 firstPosition = transform.position;
        Vector3 targetPosition = transform.position + direction;

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
