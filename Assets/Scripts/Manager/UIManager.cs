using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    IEnemySpawner enemySpawner;
    [SerializeField]
    Text enemiesKilledText;
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text playerHealthText, resultText;
    [SerializeField]
    GameObject mainMenuPanel, inGamePanel, gameOverPanel;
    [SerializeField]
    Button startBtn, restartBtn;
    public Slider slider;

    public static UIManager instance;

    public System.Action<int> updateScore;
    public System.Action updateKills;
    public System.Action<int> updatelayerHealth;
    public System.Action showResult;

    public InGameUIHandler inGameUIHandler;
    public MainMenuHandler mainMenuHandler;
    public GameOverUIHandler gameOverUIHandler;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        //UpdateScore(0);

        inGameUIHandler = ServiceLocator.GetGameUI();
        mainMenuHandler = ServiceLocator.GetMenu();
        gameOverUIHandler = ServiceLocator.GetGameOverUI();


        inGameUIHandler.InitializeInGameUI(playerHealthText
            , enemiesKilledText, scoreText, inGamePanel);

        mainMenuHandler.InitializeMainMenu(mainMenuPanel, startBtn/*, inGameUIHandler*/);

        gameOverUIHandler.InitializeGamover(gameOverPanel, resultText, restartBtn);

        updateScore(0);
        ResetUI();

    }

    public void ResetUI()
    {
        mainMenuPanel.SetActive(true);
        inGamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }
}
