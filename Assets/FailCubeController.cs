using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailCubeController : MonoBehaviour, IFailCubue
{
    public void Fail(Action fail)
    {
        fail?.Invoke();
    }
}
