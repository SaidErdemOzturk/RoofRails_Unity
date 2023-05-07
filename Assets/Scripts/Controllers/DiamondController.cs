using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class DiamondController : MonoBehaviour,IDiamond
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DORotate(new Vector3(0, 360, 0), 1f).SetRelative().SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }
    public void CollectDiamond(Action<DiamondController> collectDiamond)
    {
        collectDiamond?.Invoke(this);
    }
}
