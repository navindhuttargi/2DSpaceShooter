using UnityEngine;

public class StartState : BaseState
{
    public StartState(GameManager manager, StateController controller) : base(manager, controller)
    {

    }
    public override void Entry()
    {
        Debug.Log("State Name:" + this.ToString());
        ServiceLocator.GetService<IPlayerSpawner>().SpawnPlayer();
        ServiceLocator.GetService<IPowerUpsSpawner>().StartSpawn();
        ServiceLocator.GetService<IEnemySpawner>().StartSpawningEnemies();
        stateController.ChangeState(StateController.GameStates.running);
    }

    public override void Exit()
    {

    }
}
