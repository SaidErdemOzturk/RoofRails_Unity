using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICylinder 
{
    void StayCylinder(Action action);
    void EnterCylinder(Action<NextRoadCylinderController> action);
    void ExitCylinder(Action action);
}
