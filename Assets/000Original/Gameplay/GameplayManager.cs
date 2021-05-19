using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : SingletonMonoBehaviour<GameplayManager>
{
    public PlayerController PlayerController { get; private set; }
    public StageController StageController { get; private set; }

    private void attachComponent()
    {
        PlayerController = FindObjectOfType<PlayerController>();
        StageController = FindObjectOfType<StageController>();
    }

    protected override void Awake()
    {
        base.Awake();

        attachComponent();
    }

    private void Start()
    {
        PlayerController.Swiped += onSwiped;
    }

    private void onSwiped(Vector3 position, WayPattern way)
    {
        StageController.SwipePanel(position, way);
    }
}
