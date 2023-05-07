using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : BaseSingleton<PointManager>
{
    private CanvasManager canvasManager;
    private DiamondManager diamondManager;
    private int pointCount;

    private void Start()
    {
        canvasManager = CanvasManager.GetInstance();
       // pointCount = PlayerPrefs.GetInt(Utils.POINT);
    }
    public void AddPoints(int point)
    {
        pointCount += point;
        canvasManager.UpdatePoint(pointCount);
        //PlayerPrefs.SetInt(Utils.POINT, point);
    }

    public void SetPoints(int xCount)
    {
        PlayerPrefs.SetInt(Utils.DIAMOND, PlayerPrefs.GetInt(Utils.DIAMOND)+pointCount*xCount);
    }
}