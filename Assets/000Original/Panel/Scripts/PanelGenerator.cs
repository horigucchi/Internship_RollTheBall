using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelGenerator : MonoBehaviour
{
    [SerializeField]
    private PanelController prefab;

    public PanelController GetClone()
    {
        return Instantiate(prefab);
    }

    public List<PanelController> Generate(StageDataObject data)
    {
        var list = new List<PanelController>();
        foreach (var item in data.PanelDataTable)
        {
            var obj = GetClone();
            obj.SetParameter(item, data.TextureTable);
            list.Add(obj);
        }
        return list;
    }
}
