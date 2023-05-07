using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ICollectable 
{
    void Collect(Action<CylinderController> collect);
}
