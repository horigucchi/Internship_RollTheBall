using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    [field: SerializeField]
    public BallController prefab;

    public BallController GetClone()
    {
        return Instantiate(prefab);
    }
}
