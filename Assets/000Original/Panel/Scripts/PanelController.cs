using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public PanelData Data { get; private set; }

    public PanelObject PanelObject { get; private set; }

    public BallController BallController { get; private set; }


    public WayPattern WayPattern { get => Data.Way; }

    public bool IsHavingBall { get => Data.IsHavingBall; }

    public bool IsEndPoint { get => Data.IsEndPoint; }

    public bool CanMove { get => Data.CanMove; }

    [SerializeField]
    PanelTextureTable textureTable;

    // 移動中であるか
    private bool isMoving = false;


    private void Awake()
    {
        PanelObject = GetComponent<PanelObject>();
    }

    public void SetPosition(Vector2Int index)
    {
        Vector3 position = Vector3.zero;
        position.x = index.x - (StageController.WIDTH - 1) / 2f;
        position.y = -index.y + (StageController.HEIGHT - 1) / 2f;
        transform.position = position;
    }

    public void SetParameter(PanelData data, PanelTextureTable textureTable)
    {
        Data = data;
        this.textureTable = textureTable;
        SetPosition(new Vector2Int(data.x, data.y));
        initialize();
    }

    private void Start()
    {
        initialize();
    }

    private void initialize()
    {
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
        if (!CanMove) return;

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

    public void SetBall(BallController ball)
    {
        BallController = ball;
        ball.transform.position = transform.position;
    }

}
