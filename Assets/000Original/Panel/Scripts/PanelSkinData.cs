using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PanelSkinData
{
    [field: SerializeField]
    public PanelTextureTable TextureTable { get; private set; }

    [field: SerializeField]
    public Color LockedColor { get; private set; } = new Color(0.8f, 0.8f, 0.8f);
}
