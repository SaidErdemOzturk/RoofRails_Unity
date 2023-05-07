using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpeedCylinder
{
    void Enter(Action<SpeedCylinderController> action);
}
