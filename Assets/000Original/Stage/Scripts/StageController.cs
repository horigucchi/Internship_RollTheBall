﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    /// <summary>
    /// ステージの幅
    /// </summary>
    public const int WIDTH = 4;
    /// <summary>
    /// ステージの高さ
    /// </summary>
    public const int HEIGHT = 4;

    [SerializeField]
    private GameObject[] panels = new GameObject[WIDTH * HEIGHT];

    // 管理するステージ上のパネルコントローラー
    private PanelController[,] panelControllers = new PanelController[HEIGHT, WIDTH];

    // パネルコントローラーの初期化
    private void initializePanelObjects()
    {
        for (int j = 0; j < HEIGHT; j++)
        {
            for (int i = 0; i < WIDTH; i++)
            {
                GameObject obj = panels[HEIGHT * j + i];
                if (obj == null) continue;
                panelControllers[j, i] = obj?.GetComponent<PanelController>();
            }
        }
    }

    private void Awake()
    {
        initializePanelObjects();
    }

    // 枠の範囲外か
    private bool isOutOfRange(int x, int y)
    {
        if (x < 0 || WIDTH <= x) return true;
        if (y < 0 || HEIGHT <= y) return true;
        return false;
    }

    /// <summary>
    /// ゲームオブジェクトからパネルのインデックスを取得
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public Vector2Int GetIndex(GameObject obj)
    {
        // FIX: パネルにインデックスを管理させた方がよさそう
        int x = -1;
        int y = -1;
        for (int j = 0; j < HEIGHT; j++)
        {
            for (int i = 0; i < WIDTH; i++)
            {
                var item = panelControllers[j, i];
                if (item == null) continue;
                if (item.gameObject == obj)
                {
                    x = i;
                    y = j;
                    break;
                }
            }
        }
        return new Vector2Int(x, y);
    }

    /// <summary>
    /// 座標からパネルのインデックスを取得
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public Vector2Int GetIndex(Vector3 position)
    {
        Vector2Int index = new Vector2Int(-1, -1);

        RaycastHit2D hit = Physics2D.Raycast(position, position);
        if (hit)
        {
            index = GetIndex(hit.collider.gameObject);
        }

        return index;
    }

    public PanelController GetPanelController(int indexX, int indexY)
    {
        return panelControllers[indexY, indexX];
    }

    /// <summary>
    /// インデックスからパネルを取得し動かす
    /// </summary>
    /// <param name="indexX"></param>
    /// <param name="indexY"></param>
    /// <param name="way"></param>
    /// <returns></returns>
    public bool SwipePanel(int indexX, int indexY, WayPattern way)
    {
        if ((way & WayPattern.UpLeftDownRight) == 0) return false;
        if (isOutOfRange(indexX, indexY)) return false;
        if (panelControllers[indexY, indexX] == null) return false;

        // 移動量
        int dx = 0;
        int dy = 0;
        
        if ((way & WayPattern.Up) != 0) dy -= 1;
        if ((way & WayPattern.Down) != 0) dy += 1;
        if ((way & WayPattern.Right) != 0) dx += 1;
        if ((way & WayPattern.Left) != 0) dx -= 1;

        // 移動先配列座標
        int targetIndexX = indexX + dx;
        int targetIndexY = indexY + dy;

        if (isOutOfRange(targetIndexX, targetIndexY)) return false;
        // 移動先にすでに置いてある場合中断
        if (panelControllers[targetIndexY, targetIndexX] != null) return false;


        // FIX: 非同期処理async/awaitを使った方がよさそう
        // 入れ替え
        System.Action swap = () =>
        {
            var temp = panelControllers[targetIndexY, targetIndexX];
            panelControllers[targetIndexY, targetIndexX] = panelControllers[indexY, indexX];
            panelControllers[indexY, indexX] = temp;
        };

        // 移動の実行
        panelControllers[indexY, indexX]?.Move(way, swap);
        return true;
    }

    /// <summary>
    /// 座標からパネルを取得し動かす
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <param name="way"></param>
    /// <returns></returns>
    public bool SwipePanel(Vector3 worldPosition, WayPattern way)
    {
        Vector2Int index = GetIndex(worldPosition);
        return SwipePanel(index.x, index.y, way);
    }

}
