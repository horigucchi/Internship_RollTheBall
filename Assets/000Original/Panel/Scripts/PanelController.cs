using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    private PanelObject panelObject;
    [SerializeField]
    PanelTextureTable textureTable;

    private void Awake()
    {
        panelObject = GetComponent<PanelObject>();
    }

    private void Start()
    {
        Sprite sprite = textureTable.GetSprite(panelObject.WayPattern);
        panelObject.SetSprite(sprite);
    }


}
