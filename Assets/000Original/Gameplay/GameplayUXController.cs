using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUXController : MonoBehaviour
{
    [SerializeField]
    private UIControllerBese ClearUI;

    public void PlayClear(System.Action callback = null)
    {
        ClearUI.Show();
        callback?.Invoke();
    }
}
