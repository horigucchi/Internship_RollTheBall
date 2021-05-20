using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControllerBase : MonoBehaviour
{
    [SerializeField]
    private bool shouldBeginVisible = true;


    protected virtual void Awake()
    {
        if (!shouldBeginVisible) Hide();
    }

    protected virtual void Start()
    {
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
