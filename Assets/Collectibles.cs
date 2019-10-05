﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{

    public GameObject collectiblesPrompt;
    public bool isCollectible;
    public GameController gameController;

    private void Start()
    {
        collectiblesPrompt.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            collectiblesPrompt.gameObject.SetActive(true);
            isCollectible = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            collectiblesPrompt.gameObject.SetActive(false);
            isCollectible = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) || Input.GetButtonDown("Fire3") && isCollectible)
        {
            gameController.ClipboardCollected();
            collectiblesPrompt.gameObject.SetActive(false);
            Debug.Log("DESTROYED Clipboard");
            Destroy(this.gameObject);
        }
    }
}
