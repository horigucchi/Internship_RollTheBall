using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public PanelObject PanelObject { get; private set; }

    [field: SerializeField]
    public WayPattern WayPattern { get; private set; }

    public bool IsHavingBall { get; private set; }

    [field: SerializeField]
    public bool IsEndPoint { get; private set; }

    public bool CanMove { get; private set; }

    [SerializeField]
    PanelTextureTable textureTable;

    // 移動中であるか
    private bool isMoving = false;


    private void Awake()
    {
        PanelObject = GetComponent<PanelObject>();
    }

    private void setParameter()
    {
        IsHavingBall = false;
        CanMove = true;
    }

    private void Start()
    {
        initialize();
    }

    private void initialize()
    {
        setParameter();
        Sprite sprite = textureTable.GetSprite(WayPattern);
        PanelObject.SetSprite(sprite);
    }

    /// <summary>
    /// パネルを移動させる
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
        if (isMoving) return;
        isMoving = true;
        callback += () => { isMoving = false; };
        StartCoroutine(PanelObject.TryMoving(direction, callback));
    }



}
