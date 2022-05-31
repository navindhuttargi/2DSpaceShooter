using DG.Tweening;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public Transform interactablesParent, enemyParent;

    public event System.Action resetGame;
    public event System.Action<bool> gameStatus;
    private static GameManager _instance;
    public StateController controller;
    public GameConfig gameConfig;

    public Transform pathParent;
    public UIManager scoreCounter;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<GameManager>();
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
        scoreCounter = FindObjectOfType<UIManager>();
        ServiceLocator.InitializeContainer();
        controller = new StateController(this, StateController.GameStates.initialize);
        DOTween.Init();
    }
    public void ResetGame()
    {
        resetGame?.Invoke();
    }
    public void GameStatus(bool isWon)
    {
        gameStatus?.Invoke(isWon);
    }
}
