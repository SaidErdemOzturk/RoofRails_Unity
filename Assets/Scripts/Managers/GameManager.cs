using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Awake,
    Started,
    Fail,
    Finish
}

public class GameManager : BaseSingleton<GameManager>
{
    [SerializeField] private List<LevelScriptablle> levels;
    private PlayerController player;
    private GameState gameState;
    private CanvasManager canvasManager;
    private AnimationManager animationManager;

    public GameState GetGameState()
    {
        return gameState;
    }

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
    }



    // Start is called before the first frame update
    private void Awake()
    {
        CreateLevel();
        player = PlayerController.GetInstance();
        animationManager = AnimationManager.GetInstance();
        canvasManager = CanvasManager.GetInstance();
        gameState = GameState.Awake;
        SetGameState(gameState);
    }

    public void StartGame()
    {
        player.GetComponent<Rigidbody>().isKinematic = false;
        animationManager.StartGame();
        canvasManager.HideStartSlider();
        gameState = GameState.Started;
        SetGameState(gameState);
    }


    public void NextGame()
    {
        PlayerPrefs.SetInt(Utils.LEVEL,PlayerPrefs.GetInt(Utils.LEVEL) + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void CreateLevel()
    {
        Instantiate(levels[PlayerPrefs.GetInt(Utils.LEVEL) % levels.Count].levelPrefab);
    }

    public Vector3 GetLastPosition()
    {
        return levels[PlayerPrefs.GetInt(Utils.LEVEL,1) % levels.Count].levelPrefab.transform.Find("LastPoints").transform.position;
    }
}
