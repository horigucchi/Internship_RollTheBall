using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : SingletonMonoBehaviour<GameplayManager>
{
    public PlayerController PlayerController { get; private set; }
    public StageController StageController { get; private set; }

    private RouteSearcher routeSearcher;

    private void attachComponent()
    {
        PlayerController = FindObjectOfType<PlayerController>();
        StageController = FindObjectOfType<StageController>();
    }

    protected override void Awake()
    {
        base.Awake();

        attachComponent();
        routeSearcher = new RouteSearcher(StageController);
    }

    private void Start()
    {
        PlayerController.Swiped += onSwiped;
        StageController.PanelMoved += () =>
        {
            routeSearcher.IsConnecting(new Vector2Int(0, 0), out List<WayPattern> route);
        };
    }

    private void onSwiped(Vector3 position, WayPattern way)
    {
        StageController.SwipePanel(position, way);
    }
}
