using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : BaseSingleton<CameraController>
{
    [SerializeField] private PlayerController player;
    private GameManager gameManager;
    private Vector3 offset;
    private Vector3 tempOffset;

    void Start()
    {
        gameManager = GameManager.GetInstance();
        offset = transform.position - player.transform.position;
        tempOffset = offset;
    }

    private void FixedUpdate()
    {
        if (gameManager.GetGameState() == GameState.Started)
        {
            if (player.transform.Find("MyCylinder").localScale.y <= 6)
            {
                tempOffset = offset;
            }
            else
            {
                tempOffset = offset + new Vector3(0,5F,-5F);
            }
            transform.position = Vector3.Lerp(transform.position, player.transform.position + tempOffset, 0.5F);
        }
    }




}
