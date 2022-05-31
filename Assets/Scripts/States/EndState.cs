using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndState : BaseState
{
    public EndState(GameManager manager,StateController controller) : base(manager,controller)
    {

    }
    public override void Entry()
    {
        Debug.Log("State Name:" + this.ToString());
        UIManager.instance.showResult?.Invoke();
        gameManager.ResetGame();
    }

    public override void Exit()
    {

    }
}
