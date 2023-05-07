using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDiamond 
{
    void CollectDiamond(Action<DiamondController> collectDiamond);
}
