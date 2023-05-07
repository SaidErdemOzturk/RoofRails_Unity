using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerObject 
{
    void Stay(Action action);
    void Enter(Action action);
    void Exit(Action action);
}
