using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public PanelObject PanelObject { get; private set; }

    [SerializeField]
    PanelTextureTable textureTable;

    // 移動中であるか
    private bool isMoving = false;


    private void Awake()
    {
        PanelObject = GetComponent<PanelObject>();
    }

    private void Start()
    {
        Sprite sprite = textureTable.GetSprite(PanelObject.WayPattern);
        PanelObject.SetSprite(sprite);
    }

    /// <summary>
    /// パネルを移動させる
    /// </summary>
    /// <param name="way">移動する方向</param>
    /// <param name="callback">移動終了の通知</param>
    public void Move(WayPattern way, System.Action callback = null)
    {
        Vector3 direction = Vector3.zero;
        if ((way & WayPattern.Right) != 0) direction += Vector3.right;
        if ((way & WayPattern.Down) != 0) direction += Vector3.down;
        if ((way & WayPattern.Left) != 0) direction += Vector3.left;
        if ((way & WayPattern.Up) != 0) direction += Vector3.up;

        if (direction == Vector3.zero) return;
        if (isMoving) return;
        isMoving = true;
        callback += () => { isMoving = false; };
        StartCoroutine(PanelObject.TryMoving(direction, callback));
    }



}
