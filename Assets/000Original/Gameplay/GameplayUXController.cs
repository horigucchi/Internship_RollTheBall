using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUXController : MonoBehaviour
{
    [SerializeField]
    private UIControllerBase ClearUI;

    public void PlayClear(System.Action callback = null)
    {
        ClearUI.Show();
        callback?.Invoke();
    }
}
