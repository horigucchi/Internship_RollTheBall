using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    /// <summary>
    /// 繋がった時
    /// </summary>
    public System.Action<Vector2Int, List<WayPattern>> Connected;
    public System.Action Completed;

    public PlayerController PlayerController { get; private set; }
    public StageController StageController { get; private set; }
    public PanelGenerator PanelGenerator { get; private set; }

    public BallGenerator BallGenerator { get; private set; }

    public CommandStack SwipingStack { get; private set; }

    public GameplayUXController UXController { get; private set; }

    public bool CanSwipePanel { get; private set; }


    [SerializeField]
    private StageDataObject defaultStageData;

    private RouteSearcher routeSearcher;

    private void attachComponent()
    {
        PlayerController = FindObjectOfType<PlayerController>();
        StageController = FindObjectOfType<StageController>();
        PanelGenerator = FindObjectOfType<PanelGenerator>();
        BallGenerator = FindObjectOfType<BallGenerator>();

        SwipingStack = FindObjectOfType<CommandStack>();

        UXController = FindObjectOfType<GameplayUXController>();
    }

    private void Awake()
    { 
        attachComponent();
        routeSearcher = new RouteSearcher(StageController);
    }

    private void initializeStage()
    {
        List<PanelController> list = PanelGenerator.Generate(GameInstance.Instance.StageData ?? defaultStageData);
        StageController.SetStage(list);
    }

    private void Start()
    {
        initializeStage();

        CanSwipePanel = true;

        PlayerController.Swiped += onSwiped;
        StageController.PanelMoved += () =>
        {
            var startPoint = StageController.GetStartPoint();
            if (routeSearcher.IsConnecting(startPoint, out List<WayPattern> route))
            {
                CanSwipePanel = false;
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

        Completed += () =>
        {
            UXController.PlayClear();
        };

    }

    private void onSwiped(Vector3 position, WayPattern way)
    {
        if (!CanSwipePanel) return;
        StageController.SwipePanel(position, way);
    }

    public void UndoSwiping()
    {
        if (!CanSwipePanel) return;
        SwipingStack.Undo();
    }

    public void RetryStage()
    {
        // FIX: 直代入やめる
        SceneSwitcher.Instance.SwitchScene("GameScene");
    }

    public void ReturnTitle()
    {
        // FIX: 直代入やめる
        SceneSwitcher.Instance.SwitchScene("TitleScene");
    }
    public void ReturnStageSelection()
    {
        // FIX: 直代入やめる
        SceneSwitcher.Instance.SwitchScene("StageSelection");
    }
}
