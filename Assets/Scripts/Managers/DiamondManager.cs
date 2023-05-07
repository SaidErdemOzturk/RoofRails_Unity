using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DiamondManager : BaseSingleton<DiamondManager>
{
    private CanvasManager canvasManager;
    private int diamondCount;

    private void Start()
    {
        canvasManager = CanvasManager.GetInstance();
        diamondCount = PlayerPrefs.GetInt(Utils.DIAMOND);
    }

    public void AddDiamond()
    {
        diamondCount++;
        PlayerPrefs.SetInt(Utils.DIAMOND, diamondCount);
        canvasManager.UpdateDiamond();
    }
}
