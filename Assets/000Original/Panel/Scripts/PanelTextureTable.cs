using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PanelTextureTable", menuName = "Ex/ScriptableObject/PanelTextureTable")]
public class PanelTextureTable : ScriptableObject
{
    [field: SerializeField]
    public Sprite BlockTexture { get; private set; }
    [field: SerializeField]
    public Sprite RightTexture { get; private set; }
    [field: SerializeField]
    public Sprite DownTexture { get; private set; }
    [field: SerializeField]
    public Sprite LeftTexture { get; private set; }
    [field: SerializeField]
    public Sprite UpTexture { get; private set; }

    [field: SerializeField]
    public Sprite DownRightTexture { get; private set; }
    [field: SerializeField]
    public Sprite LeftDownTexture { get; private set; }
    [field: SerializeField]
    public Sprite UpLeftTexture { get; private set; }
    [field: SerializeField]
    public Sprite UpRightTexture { get; private set; }

    [field: SerializeField]
    public Sprite LeftRightTexture { get; private set; }
    [field: SerializeField]
    public Sprite UpDownTexture { get; private set; }

    //[field: SerializeField]
    //public Sprite LeftDownRightTexture { get; private set; }


    public Sprite GetSprite(WayPattern pattern)
    {
        Sprite sprite = null;
        switch (pattern)
        {
            case WayPattern.None:
                sprite = BlockTexture;
                break;
            case WayPattern.Right:
                sprite = RightTexture;
                break;
            case WayPattern.Down:
                sprite = DownTexture;
                break;
            case WayPattern.DownRight:
                sprite = DownRightTexture;
                break;
            case WayPattern.Left:
                sprite = LeftTexture;
                break;
            case WayPattern.LeftRight:
                sprite = LeftRightTexture;
                break;
            case WayPattern.LeftDown:
                sprite = LeftDownTexture;
                break;
            case WayPattern.Up:
                sprite = UpTexture;
                break;
            case WayPattern.UpRight:
                sprite = UpRightTexture;
                break;
            case WayPattern.UpDown:
                sprite = UpDownTexture;
                break;
            case WayPattern.UpLeft:
                sprite = UpLeftTexture;
                break;
            default:
                break;
        }
        return sprite;
    }
}
