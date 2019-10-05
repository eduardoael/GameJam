using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    int clipboards = 0;
    public GameObject gameOverScreen;

    private void Start()
    {
        gameOverScreen.SetActive(true);
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
        Debug.LogWarning("Game Over");
    }

    public void ClipboardCollected()
    {
        clipboards++;
    }

    public void TerminalReached()
    {
        Debug.Log("END. Clipboards collected:  " + clipboards);    
    }
}
