using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoadCylinderController : MonoBehaviour,ICylinder
{
    [SerializeField] ParticleSystem particle;
    private ParticleSystem tempParticle;
    private bool onCylinder;

    //particle hem next road cylinder 'i ilgilendiriyor hem de stackcylinder i ilgilendiriyor. Particle'ýn prefabýný nerede tanýmlamalýyým?


    /*
     * ICylinder cylinder = collision.gameObject.Getcomponent<ICylinder>();
     * if(cylindder != null) 
     * {
     *      tempParticle = Instantiate(particle);
     * }
     * */

    public float GetX()
    {
        Debug.Log(transform.localPosition.x);
        return transform.localPosition.x;
    }

    private void Start()
    {
    }

    public void StayCylinder(Action action)
    {
        throw new NotImplementedException();
    }

    public void EnterCylinder(Action<NextRoadCylinderController> action)
    {
        action?.Invoke(this);
    }

    public void ExitCylinder(Action action)
    {
        action?.Invoke();
    }
}
