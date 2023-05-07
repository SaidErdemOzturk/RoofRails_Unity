using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CanvasManager : BaseSingleton<CanvasManager>
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Camera uiCamera;
    [SerializeField] private Image mouseImage;
    [SerializeField] private GameObject startSlider;
    [SerializeField] private TMPro.TextMeshProUGUI pointText;
    [SerializeField] private TMPro.TextMeshProUGUI diamondText;
    [SerializeField] private Transform diamondPivot;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button tryAgainButton;
    [SerializeField] private SpriteRenderer diamondRenderer;
    [SerializeField] private Slider mapSlider;
    [SerializeField] private Vector3 lastPosition;
    [SerializeField] private TMPro.TextMeshProUGUI myLevelText;
    [SerializeField] private TMPro.TextMeshProUGUI nextLevelText;
    private GameManager gameManager;
    private Vector3 imagePosition;
    private SpriteRenderer tempImage;
    private Vector2 targetPosition;

    void Start()
    {
        myLevelText.text = PlayerPrefs.GetInt(Utils.LEVEL).ToString();
        nextLevelText.text = (PlayerPrefs.GetInt(Utils.LEVEL) + 1).ToString();
        gameManager = GameManager.GetInstance();
        nextLevelButton.gameObject.SetActive(false);
        tryAgainButton.gameObject.SetActive(false);
        UpdateDiamond();
        mouseImage.transform.DOMoveX(mouseImage.transform.position.x*(-1),1).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.Linear);
        lastPosition = gameManager.GetLastPosition();
        mapSlider.maxValue = lastPosition.z;
    }

    public void HideStartSlider()
    {
        startSlider.SetActive(false);
    }

    public void UpdatePoint(int pointCount)
    {
        pointText.transform.DOScale(pointText.transform.localScale + new Vector3(1,1,1),0.25F).SetLoops(2,LoopType.Yoyo);
        pointText.text = pointCount.ToString();
    }

    public void UpdateDiamond()
    {
        diamondText.text = PlayerPrefs.GetInt(Utils.DIAMOND).ToString();
    }

    public void ShowNextLevelButton()
    {
        nextLevelButton.gameObject.SetActive(true);
    }

    public void ShowTryAgainButton()
    {
        tryAgainButton.gameObject.SetActive(true);
    }

    public void GetDiamond(Vector3 screenPosition,DiamondController diamond)
    {
        //Debug.Log(diamondImage.transform.position);
        //tempImage = Instantiate(diamondRenderer);
        //tempImage.transform.SetParent(canvas.transform);
        Vector3 newPos = uiCamera.ScreenToViewportPoint(screenPosition);
        diamond.transform.SetParent(canvas.transform);
        diamond.transform.localScale = Vector3.one * 25;
        diamond.gameObject.layer = LayerMask.NameToLayer("UI");
        diamond.transform.localPosition = new Vector3(newPos.x*uiCamera.pixelWidth - uiCamera.pixelWidth / 2, newPos.y*uiCamera.pixelHeight - uiCamera.pixelHeight / 2);
        diamond.transform.DOMove(diamondPivot.position, 1.5F);
        Destroy(diamond.gameObject, 1.5F);

        // Debug.Log(diamond.transform.position);
        //diamond.transform.position = uiCamera.GetComponent<Camera>().ScreenToViewportPoint(diamond.gameObject.transform.position);
        //tempImage.transform.position = uiCamera.GetComponent<Camera>().ScreenToViewportPoint(screenPosition);
        //tempImage.transform.localPosition = new Vector3(screenPosition.x-uiCamera.pixelWidth/2,screenPosition.y-uiCamera.pixelHeight/2);
        //tempImage.transform.DOMove(targetPosition,1.5F);
        //Debug.Log(targetPosition);
        //tempImage.transform.DOMove(diamondImage.transform.position.normalized ,1.5F);
    }
    public void SliderUpdate(float position)
    {
        mapSlider.value = position;
    }
}