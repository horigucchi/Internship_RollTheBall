using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    /// <summary>
    /// �X�e�[�W�̕�
    /// </summary>
    public const int WIDTH = 4;
    /// <summary>
    /// �X�e�[�W�̍���
    /// </summary>
    public const int HEIGHT = 4;

    [SerializeField]
    private GameObject[] panels = new GameObject[WIDTH * HEIGHT];

    // �Ǘ�����X�e�[�W��̃p�l���R���g���[���[
    private PanelController[,] panelControllers = new PanelController[HEIGHT, WIDTH];

    // �p�l���R���g���[���[�̏�����
    private void initializePanelObjects()
    {
        for (int j = 0; j < HEIGHT; j++)
        {
            for (int i = 0; i < WIDTH; i++)
            {
                GameObject obj = panels[HEIGHT * j + i];
                if (obj == null) continue;
                panelControllers[j, i] = obj?.GetComponent<PanelController>();
            }
        }
    }

    private void Awake()
    {
        initializePanelObjects();
    }

    // �g�͈̔͊O��
    private bool isOutOfRange(int x, int y)
    {
        if (x < 0 || WIDTH <= x) return true;
        if (y < 0 || HEIGHT <= y) return true;
        return false;
    }

    public void SwipePanel(int x, int y, WayPattern way)
    {
        if (isOutOfRange(x, y)) return;
        if (panelControllers[y, x] == null) return;

        // �ړ���
        int dx = 0;
        int dy = 0;
        
        if ((way & WayPattern.Up) != 0) dy -= 1;
        if ((way & WayPattern.Down) != 0) dy += 1;
        if ((way & WayPattern.Right) != 0) dx += 1;
        if ((way & WayPattern.Left) != 0) dx -= 1;

        // �ړ���z����W
        int indexX = x + dx;
        int indexY = y + dy;

        if (isOutOfRange(indexX, indexY)) return;
        // �ړ���ɂ��łɒu���Ă���ꍇ���f
        if (panelControllers[indexY, indexX] != null) return;

        // ����ւ�
        System.Action swap = () =>
        {
            var temp = panelControllers[indexY, indexX];
            panelControllers[indexY, indexX] = panelControllers[y, x];
            panelControllers[y, x] = temp;
        };

        // �ړ��̎��s
        panelControllers[y, x]?.Move(way, swap);
    }

    void Update()
    {
        
    }
}
