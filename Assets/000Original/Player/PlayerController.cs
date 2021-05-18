using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // タッチした座標計算に使うカメラ
    private Camera mainCamera;

    /// <summary>
    /// スワイプ時に呼ばれる（スワイプした方向）
    /// </summary>
    public event System.Action<WayPattern> Swiped;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    // 指の座標をゲーム空間座標で取得
    private Vector3 getFingerPosition()
    {
        //return Input.GetTouch(0).position;
        Vector3 screenPosition = Input.mousePosition;
        screenPosition.z -= mainCamera.transform.position.z;
        return mainCamera.ScreenToWorldPoint(screenPosition);
    }

    // 角度から象限を取得
    private int getQuadrant(float angle)
    {
        return (Mathf.FloorToInt((angle + 360 - 45) / 90) + 1) % 4 + 1;
    }

    // 象限から方向を取得
    private WayPattern getSwipeWay(int quadrant)
    {
        int n = quadrant - 1;
        return (WayPattern)((0b00010001 >> n) & 0b1111);
    }

    // スワイプを検出する
    private IEnumerator trySwiping()
    {
        // 初期位置
        Vector3 firstPosition = getFingerPosition();

        yield return null;
        Vector3 vector;
        
        // 一定範囲動くまでループ
        while (true)
        {
            // 指を離したら中止
            if (Input.GetMouseButtonUp(0)) yield break;

            Vector3 position = getFingerPosition();
            vector = position - firstPosition;
            
            // 一定範囲外なら検出
            const float RANGE = 0.5f;
            if (vector.sqrMagnitude > RANGE * RANGE) break;

            yield return null;
        }

        // 指のずれから角度を算出
        float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
        WayPattern pattern = getSwipeWay(getQuadrant(angle));
        Swiped?.Invoke(pattern);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(trySwiping());
        }
    }
}