using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeState : BaseState
{
    public InitializeState(GameManager manager, StateController controller) : base(manager, controller)
    {

    }
    public override void Entry()
    {
        
        Debug.Log("State Name:" + this.ToString());
        ServiceLocator.GetService<IPlayerSpawner>().InitiliazePlayer
            (gameManager.gameConfig.playerPrefab,
            gameManager.gameConfig.playerHealth,
            gameManager.gameConfig.playerSpawnPosition);
        ServiceLocator.GetService<IBulletPool>().InitializeBulletPool
            (gameManager.gameConfig.bulletPrefab,
                gameManager.gameConfig.poolLength);
        ServiceLocator.GetService<IPowerUpsSpawner>().InitializePowerUpPsawner(gameManager.gameConfig.powerUpConfig);
        ServiceLocator.GetService<IEnemySpawner>().InitializeEnemySpawner(gameManager.gameConfig.enemyConfig);
        ServiceLocator.GetService<IPlayerSpawner>().PlayerInstantiate();
    }

    public override void Exit()
    {

    }
}
