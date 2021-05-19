using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteSearcher
{
    public const int WAY_PATTERN_COUNT = 4;
    private WayPattern[] entranceWays = new WayPattern[WAY_PATTERN_COUNT]
    {
        WayPattern.Right,
        WayPattern.Down,
        WayPattern.Left,
        WayPattern.Up
    };

    private Vector2Int[] deltaIndex = new Vector2Int[WAY_PATTERN_COUNT]
    {
        Vector2Int.right,
        Vector2Int.up,
        Vector2Int.left,
        Vector2Int.down
    };

    private StageController stageController;

    // 探索済み確認用
    private int[,] searchedTable;

    // 繋がっているルート
    private List<WayPattern> connectionRoute;

    // コンストラクタ
    public RouteSearcher(StageController stageController)
    {
        this.stageController = stageController;
        searchedTable = new int[StageController.HEIGHT, StageController.WIDTH];
        clearSearchedTable();
        connectionRoute = new List<WayPattern>();
    }

    // テーブルの初期化
    private void clearSearchedTable()
    {
        for (int j = 0; j < StageController.HEIGHT; j++)
        {
            for (int i = 0; i < StageController.WIDTH; i++)
            {
                searchedTable[j, i] = -1;
            }
        }
    }

    // 探索開始
    private void runSearching(Vector2Int startIndex)
    {
        PanelController point = stageController.GetPanelController(startIndex.x, startIndex.y);
        clearSearchedTable();
        connectionRoute.Clear();
        searchConnection(startIndex.x, startIndex.y, WayPattern.UpLeftDownRight);
    }

    // 方向の１８０度回転
    private WayPattern getReverseWay(WayPattern way)
    {
        return (WayPattern)((((int)way >> 2 | (int)way << 2)) & 0b1111);
    }

    // 経路の探索
    private WayPattern searchConnection(int indexX, int indexY, WayPattern entranceWay)
    {
        // 範囲外はやめる
        if (stageController.IsOutOfRange(indexX, indexY)) return 0;

        var panel = stageController.GetPanelController(indexX, indexY);

        // なければやめる
        if (panel == null) return 0;

        // 入口に繋がって無ければやめる
        if ((panel.WayPattern & getReverseWay(entranceWay)) == 0) return 0;

        if (panel.IsEndPoint) return entranceWay;

        // 探査ずみはやめる
        if (searchedTable[indexY, indexX] != -1) return 0;
        searchedTable[indexY, indexX] = 1;

        for (int i = 0; i < WAY_PATTERN_COUNT; i++)
        {
            //　伸びていない方向は除く
            if ((panel.WayPattern & entranceWays[i]) == 0) continue;
            // 再帰
            var way = searchConnection(indexX + deltaIndex[i].x, indexY + deltaIndex[i].y, entranceWays[i]);
            if ((way & WayPattern.UpLeftDownRight) == 0) continue;
            connectionRoute.Add(way);
            return entranceWay;
        }
        return 0;
    }

    /// <summary>
    /// 繋がっているか
    /// </summary>
    /// <param name="startIndex">開始地点のインデックス</param>
    /// <param name="route">繋がっているルート</param>
    /// <returns></returns>
    public bool IsConnecting(Vector2Int startIndex, out List<WayPattern> route)
    {
        runSearching(startIndex);
        route = connectionRoute;
        if (route.Count < 1) return false;
        route.Reverse();
        return true;
    }

    /// <summary>
    /// 繋がっているか
    /// </summary>
    /// <param name="startIndex">開始地点のインデックス</param>
    /// <returns></returns>
    public bool IsConnecting(Vector2Int startIndex)
    {
        return IsConnecting(startIndex, out _);
    }
}
