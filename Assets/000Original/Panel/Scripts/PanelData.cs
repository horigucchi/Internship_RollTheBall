using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct PanelData
{
    public int x;
    public int y;
    public WayPattern Way;
    public bool IsHavingBall;
    public bool CanMove;
    public bool IsEndPoint;

}
