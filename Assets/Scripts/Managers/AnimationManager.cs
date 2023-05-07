using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState
{
    OnStart,
    OnAir,
    OnRoad,
    OnCylinder,
    OnFinish
}


public class AnimationManager : BaseSingleton<AnimationManager>
{
    [SerializeField] private ParticleSystem onAirParticle;
    private ParticleSystem tempParticle;
    private PlayerController player;
    private Animator animator;

    private PlayerState playerState;

    public PlayerState PlayerState
    {
        get
        {
            return playerState;
        }

        set
        {
            playerState = value;
            PlayAnimation(playerState);
        }
    }

    private void Start()
    {
        player = PlayerController.GetInstance();
        playerState = PlayerState.OnStart;
        animator = player.GetAnimator();
    }


    public void StartGame()
    {
        animator.SetBool("isStarted", true);
    }
    private void IsFinish()
    {
        animator.SetBool("isFinish", true);
    }

    private void OnRoad()
    {

        animator.SetBool("OnRoad",true);
        animator.SetBool("OnAir",false);
        animator.SetBool("OnCylinder",false);
        if (tempParticle != null)
        {
            DestroyParticle();
        }
    }

    private void OnAir()
    {

        animator.SetBool("OnRoad", false);
        animator.SetBool("OnAir", true);
        animator.SetBool("OnCylinder", false);

    }

    private void OnCylinder()
    {
        animator.SetBool("OnRoad", false);
        animator.SetBool("OnAir", false);
        animator.SetBool("OnCylinder", true);
        if (tempParticle != null)
        {
            DestroyParticle();
        }
    }


    public void PlayAnimation(PlayerState playerState)
    {
        switch (playerState)
        {
            case PlayerState.OnStart:
                break;
            case PlayerState.OnAir:
                OnAir();
                break;
            case PlayerState.OnRoad:
                OnRoad();
                break;
            case PlayerState.OnCylinder:
                OnCylinder();
                break;
            case PlayerState.OnFinish:
                IsFinish();
                break;
            default:
                break;
        }
    }



    private void DestroyParticle()
    {
        Destroy(tempParticle);
    }

}
