using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    protected GameManager gameManager;
    protected StateController stateController;
    public abstract void Entry();
    public virtual void StateUpdate() { }
    public abstract void Exit();
    public BaseState(GameManager manager,StateController controller)
    {
        gameManager = manager;
        stateController = controller;
    }
}
