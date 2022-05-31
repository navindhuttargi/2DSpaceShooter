using UnityEngine;
using UnityEngine.UI;

public class InGameUIHandler : UIHandler
{
    int score = 0;
    int playerHealth = 0;
    int enemyKills = 0;
    Text playerHealthText;
    Text enemiesKilledText;
    Text scoreText;
    public void InitializeInGameUI(Text health,Text killText,Text score,GameObject gameObject)
    {
        playerHealthText = health;
        enemiesKilledText = killText;
        scoreText = score;
        uiParent = gameObject;
        killText.text = "Kills:" + enemyKills;
        playerHealthText.text = "Lives:" + GameManager.Instance.gameConfig.playerHealth;
        //enemyHealth.onValueChanged.AddListener(OnEnemyHealthChanged);
    }

    public InGameUIHandler()
    {
        UIManager.instance.updateKills += UpdateEnemyKills;
        UIManager.instance.updateScore += UpdateScore;
        UIManager.instance.updatelayerHealth += UpdatePlayerHealth;
        GameManager.Instance.resetGame += Reset;
    }
    ~InGameUIHandler()
    {
        UIManager.instance.updateKills -= UpdateEnemyKills;
        UIManager.instance.updateScore -= UpdateScore;
        UIManager.instance.updatelayerHealth -= UpdatePlayerHealth;
    }
    public bool IsAllEnimiesDied()
    {
        if (enemyKills >= ServiceLocator.GetService<IEnemySpawner>().totalEnemies)
            return true;
        return false;
    }
    public int GetTotalKills()
    {
        return enemyKills;
    }
    public int GetScore()
    {
        return score;
    }
    public void Reset()
    {
        enemyKills = 0;
        enemiesKilledText.text = "Kills:" + enemyKills;
        score = 0;
        scoreText.text = "Score:" + score;
        playerHealth = GameManager.Instance.gameConfig.playerHealth;
        playerHealthText.text = "Lives:" + playerHealth;
    }
    void UpdateEnemyKills()
    {
        enemyKills += 1;
        enemiesKilledText.text = "Kills:" + enemyKills;
    }
    void UpdateScore(int value)
    {
        score += value;
        scoreText.text = "Score:" + score;
    }
    void UpdatePlayerHealth(int health)
    {
        playerHealth = health;
        playerHealthText.text = "Health:" + playerHealth;
    }
}
