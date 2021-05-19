using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : SingletonMonoBehaviour<GameplayManager>
{
    /// <summary>
    /// 繋がった時
    /// </summary>
    public System.Action<Vector2Int, List<WayPattern>> Connected;
    public System.Action Completed;

    public PlayerController PlayerController { get; private set; }
    public StageController StageController { get; private set; }

    public BallGenerator BallGenerator { get; private set; }

    private RouteSearcher routeSearcher;

    private void attachComponent()
    {
        PlayerController = FindObjectOfType<PlayerController>();
        StageController = FindObjectOfType<StageController>();
        BallGenerator = FindObjectOfType<BallGenerator>();
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
            var startPoint = StageController.GetStartPoint();
            if (routeSearcher.IsConnecting(startPoint, out List<WayPattern> route))
            {
                Connected?.Invoke(startPoint, route);
            }
        };

        Connected += (index, route) =>
        {
            System.Action callback = Completed;
            StageController.GetPanelController(index.x, index.y).BallController.Move(route, callback);
        };

        var startPoint = StageController.GetStartPoint();
        var ball = BallGenerator.GetClone();
        StageController.GetPanelController(startPoint.x, startPoint.y).SetBall(ball);

    }

    private void onSwiped(Vector3 position, WayPattern way)
    {
        StageController.SwipePanel(position, way);
    }
}
