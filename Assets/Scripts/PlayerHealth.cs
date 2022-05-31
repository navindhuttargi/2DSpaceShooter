using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public class PlayerHealth 
{
    public int maxHealth;
    public int health;

    public PlayerState playerState;

    public void SetPlayerHealth(int _maxHealth)
    {
        maxHealth = health = _maxHealth;
        UIManager.instance.updatelayerHealth?.Invoke(health);
    }
    public void AddHealth()
    {
        health += 1;
        health = Mathf.Clamp(health, 1, maxHealth);
        UIManager.instance.updatelayerHealth?.Invoke(health);
    }
    public void GetDamage()
    {
        if (playerState == PlayerState.alive)
        {
            health -= 1;
            health = Mathf.Clamp(health, 0, maxHealth);
            if (health > 0)
            {
                UIManager.instance.updatelayerHealth?.Invoke(health);
                GameManager.Instance.StartCoroutine(ServiceLocator.GetService<IPlayerSpawner>().RespawnPlayer());
            }
            else
            {
                GameManager.Instance.GameStatus(false);
                GameManager.Instance.controller.ChangeState(StateController.GameStates.end);
            }
        }
    }
}
