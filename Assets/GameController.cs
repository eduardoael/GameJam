using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public AudioClip music;
    public AudioClip alerted;
    public AudioClip collectClipboard;

    int clipboards = 0;
    public GameObject gameOverScreen;
    public GameObject gameFinishedScreen;

    private void Start()
    {
        gameOverScreen.SetActive(false);
        SoundManager.Instance.PlayMusic(music);
    }

    public void GameOver()
    {
        SoundManager.Instance.Play(alerted);
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
        Debug.LogWarning("Game Over");
    }

    public void ClipboardCollected()
    {
        SoundManager.Instance.Play(collectClipboard);
        clipboards++;
    }

    public void TerminalReached()
    {
        //gameFinishedScreen.SetActive(true);
        Time.timeScale = 0;
        Debug.Log("END. Clipboards collected:  " + clipboards);
        SceneManager.LoadScene("");
        
    }
}
