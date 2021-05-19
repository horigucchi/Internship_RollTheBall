using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallModel : MonoBehaviour
{
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
