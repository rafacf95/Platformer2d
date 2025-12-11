using System.Collections;
using System.Collections.Generic;
using Core.Singleton;
using UnityEngine;

public class GameOverManager : Singleton<GameOverManager>
{
    public GameObject gameOverUI;

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        PauseManager.Instance.Pause();
    }
}
