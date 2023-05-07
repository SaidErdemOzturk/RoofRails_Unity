using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour,IPlane
{
    public void Stay(Action action)
    {
        action?.Invoke();
    }

    public void Enter(Action action)
    {
        action?.Invoke();
    }

    public void Exit(Action action)
    {
        action?.Invoke();
    }
}
