using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPlaneController : MonoBehaviour
{
    [SerializeField] private int xCount;
    private TMPro.TextMeshProUGUI text;
    private CanvasManager canvasManager;
    // Start is called before the first frame update

    private void Start()
    {
        canvasManager = CanvasManager.GetInstance();
        text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        text.text="x"+xCount.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<IPlayer>()?.OnFinishPlane(xCount);
    }
}
