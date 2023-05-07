using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObstacleController : MonoBehaviour, IObstacleable
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float rotateTime;
    [SerializeField] private float positionX;
    [SerializeField] private bool isMoveX;
    [SerializeField] private bool isMoveY;
    [SerializeField] private float moveYValue;

    private void Start()
    {
        transform.position = new Vector3(positionX,transform.position.y,transform.position.z);
        transform.DORotate(new Vector3(0, 360, 0), rotateTime,RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);

        if (isMoveX)
        {
            transform.DOLocalMove(new Vector3(-positionX, transform.localPosition.y, transform.localPosition.z), 1F).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
        if (isMoveY)
        {
            transform.DOLocalMove(new Vector3(0,moveYValue,0), 1F).SetRelative().SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
    }


    public void Obstacle(Action<ObstacleController> obstacle)
    {
        obstacle?.Invoke(this);
    }
}
