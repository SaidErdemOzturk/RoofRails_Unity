using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderController : MonoBehaviour,ICollectable
{
    public void Collect(Action<CylinderController> collect)
    {
        collect?.Invoke(this);
    }
}
