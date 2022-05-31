using UnityEngine;
using UnityEngine.UI;
public class UIHandler
{
    public GameObject uiParent;
    public virtual void DisableUI()
    {
        uiParent.SetActive(false);
    }
}
public class MainMenuHandler : UIHandler
{
    Button startButton;
    Slider enemyHealth;
    public void InitializeMainMenu(GameObject parent, Button btn)
    {
        uiParent = parent;
        startButton = btn;
        startButton.onClick.AddListener(StartGame);
    }
    void StartGame()
    {
        DisableUI();
        UIManager.instance.inGameUIHandler.uiParent.SetActive(true);
        GameManager.Instance.controller.ChangeState(StateController.GameStates.start);
    }
}
