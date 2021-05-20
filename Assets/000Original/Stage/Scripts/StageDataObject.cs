using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData000", menuName = "Ex/ScriptableObject/StageDataObject")]
public class StageDataObject : ScriptableObject
{
    [field: SerializeField]
    public PanelSkinData SkinData { get; private set; }

    [field: SerializeField]
    public List<PanelData> PanelDataTable { get; private set; }
}
