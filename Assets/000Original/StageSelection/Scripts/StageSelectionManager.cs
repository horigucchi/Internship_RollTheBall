using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectionManager : MonoBehaviour
{
    public void SelectStage(StageDataObject stageData)
    {
        GameInstance.Instance.StageData = stageData;
        SceneSwitcher.Instance.SwitchScene("GameScene");
    }
}
