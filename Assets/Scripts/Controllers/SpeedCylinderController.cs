using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class SpeedCylinderController : MonoBehaviour,ISpeedCylinder
{
    [SerializeField] private float positionX;
    [SerializeField] private float cylinderX;
    [SerializeField] private bool isMove;



    public void Enter(Action<SpeedCylinderController> action)
    {
        action?.Invoke(this);
    }
    public float GetX()
    {
        return cylinderX;
    }

    private void Start()
    {
        transform.position = new Vector3(positionX, transform.position.y, transform.position.z);
        if (isMove)
        {
            transform.DOLocalMove(new Vector3(-positionX, transform.localPosition.y, transform.localPosition.z), 1F).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
        transform.GetChild(0).transform.position = new Vector3(cylinderX, transform.GetChild(0).transform.position.y, transform.GetChild(0).transform.position.z);
        transform.GetChild(1).transform.position = new Vector3(-cylinderX, transform.GetChild(1).transform.position.y, transform.GetChild(1).transform.position.z);
    }
}
