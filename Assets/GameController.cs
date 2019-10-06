using System;
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
    public GameObject clipboard1;
    public GameObject clipboard2;
    public GameObject clipboard3;
    public GameObject instructions;
    bool instructionsActive;

    bool isReadingClipboard = false;

    private void Start()
    {
        instructions.SetActive(true);
        instructionsActive = true;
        Time.timeScale = 0;
        gameOverScreen.SetActive(false);
        SoundManager.Instance.PlayMusic(music);
        clipboard1.SetActive(false);
        clipboard2.SetActive(false);
        clipboard3.SetActive(false);
        clipboards = 0;
    }

    public void GameOver()
    {
        SoundManager.Instance.StopMusic();
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
        Debug.LogWarning("Game Over");
    }

    public void ClipboardCollected()
    {
        clipboards++;
        StartCoroutine(ShowClipboardUI());
    }

    IEnumerator ShowClipboardUI()
    {
        SoundManager.Instance.Play(collectClipboard);
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
        isReadingClipboard = true;
        switch (clipboards)
        {
            case 1:
                clipboard1.SetActive(true);
                break;
           case 2:
                clipboard2.SetActive(true);
                break;
           case 3:
                clipboard3.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && instructionsActive)
        {
            instructions.SetActive(false);
            Time.timeScale = 1f;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SoundManager.Instance.StopMusic();
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Backspace) && isReadingClipboard)
        {
            Time.timeScale = 1;
            isReadingClipboard = false;
            switch (clipboards)
            {
                case 1:
                    clipboard1.SetActive(false);
                    break;
                case 2:
                    clipboard2.SetActive(false);
                    break;
                case 3:
                    clipboard3.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }

    public void TerminalReached()
    {
        //gameFinishedScreen.SetActive(true);
        Debug.Log("END. Clipboards collected:  " + clipboards);
        SoundManager.Instance.StopMusic();
        switch (clipboards)
        {
            case 1:
                SceneManager.LoadScene(3);
                break;
            case 2:
                SceneManager.LoadScene(4);
                break;
            case 3:
                SceneManager.LoadScene(5);
                break;
            default:
                break;
        }
        
    }
}
