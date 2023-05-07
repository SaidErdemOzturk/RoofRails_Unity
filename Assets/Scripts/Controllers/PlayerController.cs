using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PlayerController : BaseSingleton<PlayerController>,IPlayer
{

    [SerializeField] private float playerSpeed;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private GameObject myCylinder;
    [SerializeField] ParticleSystem onCylinderParticle;
    [SerializeField] ParticleSystem onAirParticle;
    [SerializeField] private Camera uiCamera;
    [SerializeField] private GameObject character;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject nextRoadCylinders;

    private ParticleSystem tempCylinderParticle;
    private ParticleSystem tempOnAirParticle;


    private Animator animator;

    private CameraController cameraController;
    private PointManager pointManager;
    private DiamondManager diamondManager;
    private CanvasManager canvasManager;
    private AnimationManager animationManager;
    private GameManager gameManager;


    private float tempPlayerSpeed;
    private GameObject tempCylinder;
    private float horizontal;
    private bool isClickedButton;
    private Rigidbody rigidBody;
    private float diffX;
    private Vector3 contactPoint;
    private bool isFinish;


    private MeshRenderer cylinderMesh;
    private Vector3 offset;
    private Vector3 tempLocalScale;
    private bool onObstacle;
    private Vector3 onObstaclePosition;

    private RaycastHit hit;

    private bool onAirControl;
    float tempCylinderX;



    NextRoadCylinderController tempNextCylinder;

    [SerializeField] private SpriteRenderer diamondRenderer;
    private SpriteRenderer image;


    void Start()
    {
        canvasManager = CanvasManager.GetInstance();
        animationManager = AnimationManager.GetInstance();
        cameraController = CameraController.GetInstance();
        pointManager = PointManager.GetInstance();
        diamondManager = DiamondManager.GetInstance();
        tempPlayerSpeed = playerSpeed;
        gameManager = GameManager.GetInstance();
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        cylinderMesh = myCylinder.GetComponent<MeshRenderer>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (gameManager.GetGameState() == GameState.Awake)
            {
                gameManager.StartGame();
            }
            isClickedButton = true;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            isClickedButton=false;
        }
    }

    private void FixedUpdate()
    {



        if (gameManager.GetGameState() == GameState.Started)
        {
            SetForwardMovmentPlayer();
            SetHorizontalMovmentPlayer();
            canvasManager.SliderUpdate(transform.position.z);
            
            if (rigidBody.SweepTest(Vector3.forward, out hit, 0.50F))
            {
                hit.collider.GetComponent<IObstacleable>()?.Obstacle(Obstacle);
                hit.collider.GetComponent<ICollectable>()?.Collect(Collect);
                hit.collider.GetComponent<IDiamond>()?.CollectDiamond(CollectDiamond);

            }

            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1F, layerMask))
            {
                hit.collider.GetComponent<ISpeedCylinder>()?.Enter(OnSpeed);
                hit.collider.GetComponent<IPlane>()?.Enter(EnterPlane);
            }
            else if (rigidBody.SweepTest(Vector3.down, out hit, 1F))
            {
                hit.collider.GetComponent<ICylinder>()?.EnterCylinder(EnterCylinder);
                hit.collider.GetComponent<IFailCubue>()?.Fail(Fail);    

            }
            else
            {
                OnAir();
            }
        }
    }

    private void OnAir()
    {

        animationManager.PlayerState = PlayerState.OnAir;
        playerSpeed = tempPlayerSpeed;
        if (tempOnAirParticle == null)
        {
            tempOnAirParticle = Instantiate(onAirParticle, transform);
        }
    }

    private void OnSpeed(SpeedCylinderController speedCylinder)
    {
        tempCylinderX = speedCylinder.GetX();
    }

    private void SetForwardMovmentPlayer()
    {
        rigidBody.velocity = new Vector3(rigidBody.velocity.z,rigidBody.velocity.y,playerSpeed);
    }

    private void SetHorizontalMovmentPlayer()
    {
        if (isClickedButton)
        {
            horizontal = Input.GetAxis("Mouse X");
            rigidBody.velocity = new Vector3(horizontal * horizontalSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
            float tempX = Mathf.Clamp(transform.position.x, -9, 9);
            transform.position = new Vector3(tempX, transform.position.y, transform.position.z);
        }
        else
        {
            rigidBody.velocity = new Vector3(0, rigidBody.velocity.y, rigidBody.velocity.z);

        }
    }

    private void EnterCylinder(NextRoadCylinderController cylinder)
    {
        if (myCylinder.transform.localScale.y < tempCylinderX)
        {
            Fail();
        }
        animationManager.PlayerState = PlayerState.OnCylinder;
        playerSpeed = tempPlayerSpeed + 10;
        rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }

    private void EnterPlane()
    {
        animationManager.PlayerState = PlayerState.OnRoad;
        playerSpeed = tempPlayerSpeed;
        transform.DORotate(new Vector3(0, 0, 0), 0.5F);
        if (tempOnAirParticle != null)
        {
            Destroy(tempOnAirParticle);
        }
    }

    private void Fail()
    {
        gameManager.SetGameState(GameState.Fail);
        canvasManager.ShowTryAgainButton();
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.AddComponent<Rigidbody>();
            transform.GetChild(i).GetComponent<Rigidbody>().velocity = rigidBody.velocity;
        }
        transform.DetachChildren();
    }

    private void CollectDiamond(DiamondController collectDiamond)
    {
        collectDiamond.GetComponent<MeshCollider>().isTrigger = false;
        canvasManager.GetDiamond(cameraController.GetComponent<Camera>().WorldToScreenPoint(collectDiamond.transform.position),collectDiamond);
        diamondManager.AddDiamond();
    }

    private void Collect(CylinderController cylinder)
    {
        pointManager.AddPoints(1);
        myCylinder.transform.localScale += new Vector3(0, cylinder.transform.localScale.y, 0);
        Destroy(cylinder.gameObject);
    }

    private void Obstacle(ObstacleController obstacle)
    {
        tempLocalScale = myCylinder.transform.localScale;
        if (transform.position.x > obstacle.transform.position.x)
        {
            diffX = obstacle.transform.position.x - cylinderMesh.bounds.min.x;
            onObstaclePosition = new Vector3(diffX / 2, 0, 0);
            myCylinder.transform.position += onObstaclePosition;
            myCylinder.transform.localScale -= new Vector3(0, diffX / 2, 0);
            GameObject cutted = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            cutted.transform.localScale = new Vector3(tempLocalScale.x, diffX, tempLocalScale.z);
            cutted.transform.rotation = myCylinder.transform.rotation;
            cutted.transform.position = new Vector3(obstacle.transform.position.x - cutted.transform.localScale.y, myCylinder.transform.position.y, myCylinder.transform.position.z);
            cutted.AddComponent<Rigidbody>();
        }
        else
        {
            diffX = cylinderMesh.bounds.max.x - obstacle.transform.position.x;
            onObstaclePosition = new Vector3(diffX / 2, 0, 0);
            myCylinder.transform.position -= onObstaclePosition;
            myCylinder.transform.localScale -= new Vector3(0, diffX / 2, 0);
            GameObject cutted = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            cutted.transform.localScale = new Vector3(tempLocalScale.x, diffX, tempLocalScale.z);
            cutted.transform.rotation = myCylinder.transform.rotation;
            cutted.transform.position = new Vector3(obstacle.transform.position.x + cutted.transform.localScale.y, myCylinder.transform.position.y, myCylinder.transform.position.z);
            cutted.AddComponent<Rigidbody>();
        }
        StartCoroutine(DelayPosition());
    }

    IEnumerator DelayPosition()
    {
        yield return new WaitForSeconds(0.75F);
        myCylinder.transform.position = new Vector3(transform.position.x, myCylinder.transform.position.y, myCylinder.transform.position.z);
    }

    public void OnFinishPlane(int xCount)
    {
        if (isFinish)
        {
            return;
        }
        animationManager.PlayerState = PlayerState.OnFinish;
        canvasManager.ShowNextLevelButton();
        transform.Find("MyCylinder").SetParent(null);
        myCylinder.AddComponent<Rigidbody>();
        myCylinder.GetComponent<Rigidbody>().AddForce(new Vector3(0,0,playerSpeed*50));
        pointManager.SetPoints(xCount);
        transform.DORotate(new Vector3(0,0,0),0.2F);
        gameManager.SetGameState(GameState.Finish);
        isFinish = true;
    }

    public Animator GetAnimator()
    {
        return animator;
    }

}