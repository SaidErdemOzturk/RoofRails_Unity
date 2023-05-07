using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IObstacleable 
{
    void Obstacle(Action<ObstacleController> obstacle);
}
