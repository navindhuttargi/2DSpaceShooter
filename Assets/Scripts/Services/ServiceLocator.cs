using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator
{
    private static Dictionary<object, object> _serviceContainer = null;
    public static Dictionary<object, object> serviceContainer => _serviceContainer;
    public static void InitializeContainer()
    {
        _serviceContainer = null;
        _serviceContainer = new Dictionary<object, object>()
        {
            {typeof(IPlayerSpawner),new PlayerSpawner() },
            {typeof(IBulletPool),new BulletPool()},
            {typeof(IPowerUpsSpawner),new PowerUpSpawner() },
            {typeof(IEnemySpawner),new EnemySpawner() }
        };
    }
    public static MainMenuHandler GetMenu()
    {
        return new MainMenuHandler();
    }
    public static InGameUIHandler GetGameUI()
    {
        return new InGameUIHandler();
    }
    public static GameOverUIHandler GetGameOverUI()
    {
        return new GameOverUIHandler();
    }
    public static T GetService<T>()
    {
        try
        {
            return (T)_serviceContainer[typeof(T)];
        }
        catch (System.Exception ex)
        {
            Debug.Log(typeof(T).ToString());
            throw new System.Exception("Service not implemented");
        }
    }
}
