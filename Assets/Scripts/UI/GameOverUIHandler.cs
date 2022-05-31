using UnityEngine;
using UnityEngine.UI;

public class GameOverUIHandler : UIHandler
{
    Text resultText;
    Button restartBtn;
    bool gameWon = false;
    public void InitializeGamover(GameObject parent, Text result, Button restart)
    {
        uiParent = parent;
        resultText = result;
        restartBtn = restart;
        restartBtn.onClick.AddListener(RestartGame);
    }
    public void RestartGame()
    {
        UIManager.instance.gameOverUIHandler.uiParent.SetActive(false);
        UIManager.instance.inGameUIHandler.uiParent.SetActive(true);
        GameManager.Instance.controller.ChangeState(StateController.GameStates.start);
        gameWon = false;
    }
    public GameOverUIHandler()
    {
        UIManager.instance.showResult += ShowResult;
        GameManager.Instance.gameStatus += GameStatus;
    }
    ~GameOverUIHandler()
    {
        UIManager.instance.showResult -= ShowResult;
        GameManager.Instance.gameStatus -= GameStatus;
    }
    public void GameStatus(bool status)
    {
        gameWon = status;
    }
    void ShowResult()
    {
        UIManager.instance.inGameUIHandler.DisableUI();
        uiParent.SetActive(true);
        resultText.text = string.Empty;
        resultText.text = gameWon ? "Game Won" : "Game Lost";
        resultText.text += "\nScore:" + UIManager.instance.inGameUIHandler.GetScore() +
            "\nKills:" + UIManager.instance.inGameUIHandler.GetTotalKills();
    }
}
