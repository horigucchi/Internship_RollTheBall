using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // �^�b�`�������W�v�Z�Ɏg���J����
    private Camera mainCamera;

    /// <summary>
    /// �X���C�v���ɌĂ΂��i�X���C�v���������j
    /// </summary>
    public event System.Action<WayPattern> Swiped;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    // �w�̍��W���Q�[����ԍ��W�Ŏ擾
    private Vector3 getFingerPosition()
    {
        //return Input.GetTouch(0).position;
        Vector3 screenPosition = Input.mousePosition;
        screenPosition.z -= mainCamera.transform.position.z;
        return mainCamera.ScreenToWorldPoint(screenPosition);
    }

    // �p�x����ی����擾
    private int getQuadrant(float angle)
    {
        return (Mathf.FloorToInt((angle + 360 - 45) / 90) + 1) % 4 + 1;
    }

    // �ی�����������擾
    private WayPattern getSwipeWay(int quadrant)
    {
        int n = quadrant - 1;
        return (WayPattern)((0b00010001 >> n) & 0b1111);
    }

    // �X���C�v�����o����
    private IEnumerator trySwaipe()
    {
        // �����ʒu
        Vector3 firstPosition = getFingerPosition();

        yield return null;
        Vector3 vector;
        
        // ���͈͓����܂Ń��[�v
        while (true)
        {
            // �w�𗣂����璆�~
            if (Input.GetMouseButtonUp(0)) yield break;

            Vector3 position = getFingerPosition();
            vector = position - firstPosition;
            
            // ���͈͊O�Ȃ猟�o
            const float RANGE = 0.5f;
            if (vector.sqrMagnitude > RANGE * RANGE) break;

            yield return null;
        }

        // �w�̂��ꂩ��p�x���Z�o
        float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
        WayPattern pattern = getSwipeWay(getQuadrant(angle));
        Swiped?.Invoke(pattern);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(trySwaipe());
        }
    }
}